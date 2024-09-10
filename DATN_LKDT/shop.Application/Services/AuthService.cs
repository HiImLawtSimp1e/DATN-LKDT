using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.AuthDTOs;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(AppDbContext context, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        #region GetTokenClaimsService
        public Guid GetUserId() => new Guid(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public string GetUserName() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        #endregion GetTokenClaimsService

        #region AuthService
        public async Task<ApiResponse<bool>> ChangePassword(Guid accountId, string newPassword)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(account => account.Id == accountId);
            if (account == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy tài khoản"
                };
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Thay đổi mật khẩu thành công"
            };
        }

        public async Task<ApiResponse<string>> Login(LoginDto loginDTO)
        {
            var username = loginDTO.Username;
            var password = loginDTO.Password;
            var account = await _context.Accounts
                                        .Include(a => a.Role)
                                        .FirstOrDefaultAsync(a => a.Username.ToLower().Equals(username.ToLower()));
            if (account == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Không tim thấy tài khoản"
                };
            }
            else if (!VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Mật khẩu không chính xác"
                };
            }
            else if (!account.IsActive)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Tài khoản đã bị khóa"
                };
            }
            else if (account.Role.RoleName != "Customer")
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Bạn không có quyền truy cập"
                };
            }
            else
            {
                var token = CreateToken(account);
                return new ApiResponse<string>
                {
                    Data = token
                };
            }
        }

        public async Task<ApiResponse<string>> AdminLogin(LoginDto loginDTO)
        {
            var username = loginDTO.Username;
            var password = loginDTO.Password;
            var account = await _context.Accounts
                                        .Include(a => a.Role)
                                        .FirstOrDefaultAsync(a => a.Username.ToLower().Equals(username.ToLower()));
            if (account == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Không tìm thấy tài khoản"
                };
            }
            else if (!VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Mật khẩu không chính xác"
                };
            }
            else if (!account.IsActive)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Tài khoản đã bị khóa"
                };
            }
            else if (account.Role.RoleName != "Admin" && account.Role.RoleName != "Employee")
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Bạn không có quyền đăng nhập"
                };
            }
            else
            {
                var token = CreateToken(account);
                return new ApiResponse<string>
                {
                    Data = token
                };
            }
        }

        public async Task<ApiResponse<string>> Register(RegisterDto registerDTO)
        {
            try
            {
                //check account name exist
                if (await AccountExists(registerDTO.Username))
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        Message = string.Format("Tài khoản \"{0}\" đã tồn tại", registerDTO.Username)
                    };
                }

                // set role for register
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Customer");
                if (role == null)
                {
                    return new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Something error!!!"
                    };
                }

                var address = new AddressEntity
                {
                    Name = registerDTO.Name,
                    Email = registerDTO.Email,
                    PhoneNumber = registerDTO.PhoneNumber,
                    Address = registerDTO.Address,
                    IsMain = true
                };

                var account = new AccountEntity
                {
                    Username = registerDTO.Username,
                    Addresses = new List<AddressEntity>(),
                    RoleId = role.Id,
                    Role = role
                };
                account.Addresses.Add(address);

                var password = registerDTO.Password;
                //hash password
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                account.PasswordHash = passwordHash;
                account.PasswordSalt = passwordSalt;

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                var token = CreateToken(account);

                return new ApiResponse<string>
                {
                    Data = token,
                    Message = "Đăng ký tài khoản thành công"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<ApiResponse<string>> VerifyToken(string token)
        {
            var response = new ApiResponse<string>();

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:Token").Value);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, // Set to true if you want to validate the issuer
                    ValidateAudience = false, // Set to true if you want to validate the audience
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    // Extract the role claim
                    var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                    if (roleClaim != null)
                    {
                        response.Data = roleClaim;
                        response.Success = true;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Role claim not found";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Invalid token";
                }
            }
            catch (SecurityTokenExpiredException)
            {
                response.Success = false;
                response.Message = "Đã hết phiên đăng nhập";
            }
            catch (SecurityTokenException)
            {
                response.Success = false;
                response.Message = "Invalid token";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }


        #endregion AuthService

        #region PrivateService

        // generate jwt
        private string CreateToken(AccountEntity account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, account.Role.RoleName),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                     claims: claims,
                     expires: DateTime.Now.AddDays(7),
                     signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        // verify hash password
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        //check account name exist
        public async Task<bool> AccountExists(string username)
        {
            if (await _context.Accounts
                .AnyAsync(account => account.Username.ToLower()
                .Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }

        //hash password
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        #endregion PrivateService
    }
}
