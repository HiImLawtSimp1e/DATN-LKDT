using AppBusiness.Model.Pagination;
using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IAccountService
    {
        Task<ApiResponse<Pagination<List<AccountEntity>>>> GetAdminAccounts(int currentPage, int pageSize);
        Task<ApiResponse<AccountEntity>> GetAdminSingleAccount(Guid id);
        Task<ApiResponse<bool>> CreateAccount(AddAccountDto newAccount);
        Task<ApiResponse<bool>> UpdateAccount(Guid accountId, UpdateAccountDto updateAccount);
        Task<ApiResponse<bool>> SoftDeleteAccount(Guid accountId);
    }
}
