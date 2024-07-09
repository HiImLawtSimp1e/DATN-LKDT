using AppBusiness.Model.Pagination;
using AutoMapper;
using MicroBase.Entity.Repositories;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<AccountEntity> _repo;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public AccountService(IRepository<AccountEntity> repo,IMapper mapper ,AppDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
        }
        public async Task<ApiResponse<bool>> CreateAccount(AddAccountDto newAccount)
        {
            var existUsername = await _context.Accounts.SingleOrDefaultAsync(a => a.Username == newAccount.Username);

            if(existUsername != null) 
            {
                return new ApiResponse<bool>
                {
                    ResultObject = false,
                    IsSuccessed = false,
                    Message = "Tài khoản đã tồn tại"
                };
            }

            var account = _mapper.Map<AccountEntity>(newAccount);
            account.Status = 1;

            await _repo.InsertAsync(account);
            return new ApiResponse<bool>
            {
                ResultObject = true,
                IsSuccessed = true,
                Message = "Tạo tài khoản thành công"
            };
        }

        public async Task<ApiResponse<Pagination<List<AccountEntity>>>> GetAdminAccounts(int currentPage, int pageSize)
        {
            var pageCount = Math.Ceiling((double)(_context.Accounts.Where(b => b.Status != 0).Count() / pageSize));

            var accounts = await _context.Accounts
                               .Where(b => b.Status != 0) // Filter blog that status has deleted 
                               .Skip((currentPage - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();

            var pagingData = new Pagination<List<AccountEntity>>
            {
                Content = accounts,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalPages = (int)pageCount
            };

            return new ApiResponse<Pagination<List<AccountEntity>>>
            {
                ResultObject = pagingData,
                IsSuccessed = true,
            };
        }

        public async Task<ApiResponse<AccountEntity>> GetAdminSingleAccount(Guid id)
        {
            var account = await _repo.GetByIdAsync(id);
            if (account == null)
            {
                return new ApiResponse<AccountEntity>
                {
                    IsSuccessed = false,
                    Message = "Tài khoản không tồn tại"
                };
            }
            return new ApiResponse<AccountEntity>
            {
                ResultObject = account,
                IsSuccessed = true,
            };
        }

        public async Task<ApiResponse<bool>> SoftDeleteAccount(Guid accountId)
        {
            var dbAccount = await _repo.GetByIdAsync(accountId);
            if (dbAccount == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccessed = false,
                    Message = "Tài khoản không tồn tại"
                };
            }

            dbAccount.Status = 0;

            await _repo.UpdateAsync(dbAccount);

            return new ApiResponse<bool>
            {
                ResultObject = true,
                IsSuccessed = true,
                Message = "Đã xóa tài khoản"
            };
        }

        public async Task<ApiResponse<bool>> UpdateAccount(Guid accountId, UpdateAccountDto updateAccount)
        {
            var dbAccount = await _repo.GetByIdAsync(accountId);
            if (dbAccount == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccessed = false,
                    Message = "Tài khoản không tồn tại"
                };
            }

            _mapper.Map(updateAccount, dbAccount);
            dbAccount.LastModifiedOnDate = DateTime.Now;
            await _repo.UpdateAsync(dbAccount);

            return new ApiResponse<bool>
            {
                ResultObject = true,
                IsSuccessed = true,
                Message = "Cập nhật thông tin tài khoản thành công"
            };
        }
    }
}
