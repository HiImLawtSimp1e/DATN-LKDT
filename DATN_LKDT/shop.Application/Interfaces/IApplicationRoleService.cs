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
    public interface IApplicationRoleService
    {
        Task<ApiResponse<Pagination<List<ApplicationRole>>>> GetRoles(int currentPage, int pageSize);
        Task<ApiResponse<bool>> CreateRole(AddApplicationRoleDto newRole);
        Task<ApiResponse<bool>> UpdateRole(string roleId, UpdateApplicationRoleDto updateRole);
        Task<ApiResponse<bool>> DeleteRole(string roleId);
    }
}
