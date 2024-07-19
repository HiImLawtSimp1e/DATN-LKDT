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
    public interface IApplicationUserService
    {
        Task<ApiResponse<Pagination<List<ApplicationUser>>>> GetUsers(int currentPage, int pageSize);
        Task<ApiResponse<bool>> CreateUser(AddApplicationUserDto newUser);
        Task<ApiResponse<bool>> UpdateUser(string userId, UpdateApplicationUserDto updateUser);
        Task<ApiResponse<bool>> SoftDeleteUser(string userId);
    }
}
