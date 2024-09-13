using AppBusiness.Model.Pagination;
using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs.AccountDto;
using shop.Application.ViewModels.ResponseDTOs.AccountResponseDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IAccountService
    {
        Task<ApiResponse<Pagination<List<AccountListResponseDto>>>> GetAdminAccounts(int page, double pageResults);
        Task<ApiResponse<AccountDetailResponseDto>> GetAdminSingleAccount(Guid accountId);
        Task<ApiResponse<List<RoleEntity>>> GetAdminRoles();
        Task<ApiResponse<bool>> CreateAccount(AddAccountDto newAccount);
        Task<ApiResponse<bool>> UpdateAccount(Guid accountId, UpdateAccountDto updateInfoAccount);
        Task<ApiResponse<bool>> SoftDeleteAccount(Guid accountId);
        Task<ApiResponse<Pagination<List<AccountEntity>>>> SearchAccounts(string searchText, int page, double pageResults);
    }
}
