using AppBusiness.Model.Pagination;
using AutoMapper;
using Azure;
using MicroBase.Entity.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.AccountDto;
using shop.Application.ViewModels.ResponseDTOs.AccountResponseDto;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public AccountService(IMapper mapper ,AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ApiResponse<Pagination<List<AccountListResponseDto>>>> GetAdminAccounts(int page, double pageResults)
        {
            var pageCount = Math.Ceiling(_context.Accounts.Where(p => !p.Deleted).Count() / pageResults);
            var accounts = await _context.Accounts
                  .Where(a => !a.Deleted)
                  .OrderByDescending(a => a.Role.RoleName == "Admin")
                  .ThenByDescending(a => a.ModifiedAt)
                  .Skip((page - 1) * (int)pageResults)
                  .Take((int)pageResults)
                  .Include(a => a.Role)
                  .ToListAsync();

            var result = _mapper.Map<List<AccountListResponseDto>>(accounts);

            var pagingData = new Pagination<List<AccountListResponseDto>>
            {
                Result = result,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<AccountListResponseDto>>>
            {
                Data = pagingData,
            };
        }

        public async Task<ApiResponse<AccountDetailResponseDto>> GetAdminSingleAccount(Guid accountId)
        {
            var account = await _context.Accounts
                                   .Where(a => !a.Deleted)
                                   .Include(a => a.Role)
                                   .FirstOrDefaultAsync(a => a.Id == accountId);

            if (account == null)
            {
                return new ApiResponse<AccountDetailResponseDto>
                {
                    Success = false,
                    Message = "Không tìm thấy tài khoản"
                };
            }

            var address = await _context.Address
                                      .Where(add => add.IsMain)
                                      .FirstOrDefaultAsync(add => add.AccountId == accountId);

            if (address == null)
            {
                return new ApiResponse<AccountDetailResponseDto>
                {
                    Success = false,
                    Message = "Không tìm thấy thông tin tài khoản"
                };
            }

            var result = _mapper.Map<AccountDetailResponseDto>(account); // Mapping Account Entity => result(DTO)
            _mapper.Map(address, result);                                // Mapping Address Entity => result(DTO)

            return new ApiResponse<AccountDetailResponseDto>
            {
                Data = result
            };
        }

        public async Task<ApiResponse<List<RoleEntity>>> GetAdminRoles()
        {
            var roles = await _context.Roles
                                    .ToListAsync();
            return new ApiResponse<List<RoleEntity>>()
            {
                Data = roles
            };
        }

        public async Task<ApiResponse<bool>> CreateAccount(AddAccountDto newAccount)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == newAccount.RoleId);
            if (role == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy role"
                };
            }

            //check account name exist
            if (await AccountExists(newAccount.Username))
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Tài khoản đã tồn tại"
                };
            }

            var address = new AddressEntity
            {
                Name = newAccount.Name,
                Email = newAccount.Email,
                PhoneNumber = newAccount.PhoneNumber,
                Address = newAccount.Address,
                IsMain = true
            };

            var account = new AccountEntity
            {
                Username = newAccount.Username,
                Addresses = new List<AddressEntity>(),
                RoleId = role.Id,
                Role = role
            };
            account.Addresses.Add(address);

            var password = newAccount.Password;
            //hash password
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Message = "Tạo mới tài khoản thành công"
            };
        }
        public async Task<ApiResponse<bool>> UpdateAccount(Guid accountId, UpdateAccountDto updateInfoAccount)
        {
            var dbAccount = await _context.Accounts
                                      .Where(a => !a.Deleted)
                                      .Include(a => a.Role)
                                      .FirstOrDefaultAsync(a => a.Id == accountId);

            if (dbAccount == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy tài khoản"
                };
            }

            if (dbAccount.Role.RoleName == "Admin" && !updateInfoAccount.IsActive)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không thể ngưng hoạt động tài khoản admin"
                };
            }

            var dbAddress = await _context.Address
                                      .Where(add => add.IsMain)
                                      .FirstOrDefaultAsync(add => add.AccountId == accountId);

            if (dbAddress == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy thông tin tài khoản"
                };
            }

            // set account's information
            dbAddress.Name = updateInfoAccount.Name;
            dbAddress.Email = updateInfoAccount.Email;
            dbAddress.PhoneNumber = updateInfoAccount.PhoneNumber;
            dbAddress.Address = updateInfoAccount.Address;

            dbAccount.IsActive = updateInfoAccount.IsActive;

            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Message = "Đã cập nhật thông tin tài khoản"
            };
        }
        public async Task<ApiResponse<bool>> SoftDeleteAccount(Guid accountId)
        {
            var account = await _context.Accounts
                                     .Where(a => !a.Deleted)
                                     .Include(a => a.Role)
                                     .FirstOrDefaultAsync(a => a.Id == accountId);
            if (account == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy tài khoản"
                };
            }

            if (account.Role.RoleName == "Admin")
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không thể xóa tài khoản admin"
                };
            }

            account.Deleted = true;
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Message = "Đã xóa tài khoản"
            };
        }

        private async Task<bool> AccountExists(string username)
        {
            if (await _context.Accounts
                .AnyAsync(account => account.Username.ToLower()
                .Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
