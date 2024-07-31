using AppBusiness.Model.Pagination;
using AutoMapper;
using Azure;
using MicroBase.Entity.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
            var pageCount = Math.Ceiling(_context.Products.Where(p => !p.Deleted && p.IsActive).Count() / pageResults);
            var accounts = await _context.Accounts
                  .Where(a => !a.Deleted)
                  .OrderByDescending(a => a.ModifiedAt)
                  .Skip((page - 1) * (int)pageResults)
                  .Take((int)pageResults)
                  .Include(a => a.Role)
                  .ToListAsync();

            var result = _mapper.Map<List<AccountListResponseDto>>(accounts);

            var pagingData = new Pagination<List<AccountListResponseDto>>
            {
                Result = result,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return new ApiResponse<Pagination<List<AccountListResponseDto>>>
            {
                Data = pagingData,
            };
        }

        public async Task<ApiResponse<AccountDetailResponseDto>> GetAdminSingleAccount(Guid id)
        {
            var account = await _context.Accounts
                                   .Where(a => !a.Deleted)
                                   .Include(a => a.Role)
                                   .FirstOrDefaultAsync(a => a.Id == id);

            if (account == null)
            {
                return new ApiResponse<AccountDetailResponseDto>
                {
                    Success = false,
                    Message = "Cannot find account"
                };
            }

            var result = _mapper.Map<AccountDetailResponseDto>(account); // Mapping Account Entity => result(DTO)

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
            //check account name exist
            if (await AccountExists(newAccount.Username))
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Tài khoản đã tồn tại"
                };
            }

            var account = _mapper.Map<AccountEntity>(newAccount);

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
        public async Task<ApiResponse<bool>> UpdateAccount(Guid accountId, UpdateAccountDto updateAccount)
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

            _mapper.Map(updateAccount, dbAccount);

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
                                     .FirstOrDefaultAsync(a => a.Id == accountId);
            if (account == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy tài khoản"
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
