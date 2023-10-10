using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels;
using shop.Application.ViewModels.Colors;
using shop.Application.ViewModels.Colors.Queries;
using shop.Application.ViewModels.Colors.Requests;
using shop.Application.ViewModels.Roles;
using shop.Application.ViewModels.Roles.Queries;
using shop.Application.ViewModels.Roles.Requests;
using shop.Application.ViewModels.Sizes;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System.Linq;

namespace shop.Infrastructure.Implements
{
    public class RoleServices : IRoleServices
    {
        private readonly AppDbContext _dbContext;

        public RoleServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse<bool>> CreateRole(RoleCreateRequest request)
        {
            var newRole = new Role
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
            };

            await _dbContext.Roles.AddAsync(newRole);
            await _dbContext.SaveChangesAsync();

            return new ApiSuccessResponse<bool>("create new Role success", true);
        }

        public async Task<ApiResponse<bool>> DeleteRole(RoleDeleteRequest request)
        {
            var query = await _dbContext.Roles.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (query == null)
            {
                return new ApiSuccessResponse<bool>("Role does not exist", false);
            }

            query.DeletedDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResponse<bool>("Delete Role success", true);
        }



        public async Task<ApiResponse<List<RoleDto>>> GetAllRoles()
        {
            var query = from c in _dbContext.Roles
                        select new RoleDto
                        {
                            Id = c.Id,
                            Name = c.Name
                        };

            var result = await query.ToListAsync();

            return new ApiSuccessResponse<List<RoleDto>>("Get all colors successfully", result);
        }

        public async Task<ApiResponse<RoleDto>> GetRoleById(RoleGetByIdRequest request)
        {
            var roleData = await GetAllRoles();
            var result = roleData.ResultObject.FirstOrDefault(c => c.Id == request.Id);

            if (result == null)
            {
                return new ApiResponse<RoleDto>()
                {
                    IsSuccessed = false,
                    Message = "role do not exist",
                };
            }

            return new ApiResponse<RoleDto>()
            {
                IsSuccessed = true,
                Message = "Create role sucessfully",
                ResultObject = result
            };
        }

        public async Task<ApiResponse<bool>> UpdateRole(RoleUpdateRequest request)
        {
            var queryResult = await _dbContext.Roles.FirstOrDefaultAsync(c => c.Name == request.Name);

            if (queryResult == null)
            {
                return new ApiSuccessResponse<bool>("Role does not exist", false);
            }

            queryResult.Name = request.Name;
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResponse<bool>("Update Role success", true);
        }
    }
}
