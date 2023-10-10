using shop.Application.Common;
using shop.Application.ViewModels.Roles;
using shop.Application.ViewModels.Roles.Queries;
using shop.Application.ViewModels.Roles.Requests;


namespace shop.Application.Interfaces
{
    public interface IRoleServices
    {
        Task<ApiResponse<List<RoleDto>>> GetAllRoles();
        Task<ApiResponse<RoleDto>> GetRoleById(RoleGetByIdRequest request);
        Task<ApiResponse<bool>> CreateRole(RoleCreateRequest request);
        Task<ApiResponse<bool>> UpdateRole(Guid ID,RoleUpdateRequest request);
        Task<ApiResponse<bool>> DeleteRole(RoleDeleteRequest request);
    }
}
