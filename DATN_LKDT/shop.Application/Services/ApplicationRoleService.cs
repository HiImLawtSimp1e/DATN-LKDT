using AppBusiness.Model.Pagination;
using AutoMapper;
using MicroBase.Entity.Repositories;
using Microsoft.EntityFrameworkCore;
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
    public class ApplicationRoleService : IApplicationRoleService
    {
        private readonly IRepository<ApplicationRole> _repo;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ApplicationRoleService(IRepository<ApplicationRole> repo, IMapper mapper, AppDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
        }
        public async Task<ApiResponse<bool>> CreateRole(AddApplicationRoleDto newRole)
        {
            var role = _mapper.Map<ApplicationRole>(newRole);

            await _repo.InsertAsync(role);
            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Thêm thành công chức vụ"
            };
        }

        public async Task<ApiResponse<Pagination<List<ApplicationRole>>>> GetRoles(int currentPage, int pageResults)
        {
            var pageCount = Math.Ceiling((double)(_context.AspNetRoles.Count() / pageResults));

            var roles = await _context.AspNetRoles
                               .Skip((currentPage - 1) * pageResults)
                               .Take(pageResults)
                               .ToListAsync();

            var pagingData = new Pagination<List<ApplicationRole>>
            {
                Result = roles,
                CurrentPage = currentPage,
                PageResults = pageResults,
                Pages = (int)pageCount
            };

            return new ApiResponse<Pagination<List<ApplicationRole>>>
            {
                Data = pagingData
            };
        }

        public async Task<ApiResponse<bool>> DeleteRole(string roleId)
        {
            var dbRole = await _context.AspNetRoles.FirstOrDefaultAsync(u => u.Id == roleId);
            if (dbRole == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy chức vụ"
                };
            }

            await _repo.DeleteAsync(dbRole);

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Đã xóa chức vụ"
            };
        }

        public async Task<ApiResponse<bool>> UpdateRole(string roleId, UpdateApplicationRoleDto updateRole)
        {
            var dbRole = await _context.AspNetRoles.FirstOrDefaultAsync(u => u.Id == roleId);
            if (dbRole == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy chức vụ"
                };
            }

            _mapper.Map(updateRole, dbRole);
            await _repo.UpdateAsync(dbRole);

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Cập nhật chức vụ thành công"
            };
        }
    }
}
