using AppBusiness.Model.Pagination;
using AutoMapper;
using MicroBase.Entity.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IRepository<ApplicationUser> _repo;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ApplicationUserService(IRepository<ApplicationUser> repo, IMapper mapper, AppDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
        }
        public async Task<ApiResponse<bool>> CreateUser(AddApplicationUserDto newUser)
        {
            var user = _mapper.Map<ApplicationUser>(newUser);
            user.Status = 1;

            await _repo.InsertAsync(user);
            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Thêm thành công thông tin người dùng"
            };
        }

        public async Task<ApiResponse<Pagination<List<ApplicationUser>>>> GetUsers(int currentPage, int pageResults)
        {
            var pageCount = Math.Ceiling((double)(_context.AspNetUsers.Where(b => b.Status != 0).Count() / pageResults));

            var users = await _context.AspNetUsers
                               .Where(b => b.Status != 0) // Filter blog that status has deleted 
                               .Skip((currentPage - 1) * pageResults)
                               .Take(pageResults)
                               .ToListAsync();

            var pagingData = new Pagination<List<ApplicationUser>>
            {
                Result = users,
                CurrentPage = currentPage,
                PageResults = pageResults,
                Pages = (int)pageCount
            };

            return new ApiResponse<Pagination<List<ApplicationUser>>>
            {
                Data = pagingData
            };
        }

        public async Task<ApiResponse<bool>> SoftDeleteUser(string userId)
        {
            var dbUser = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Id == userId);
            if (dbUser == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy thông tin người dùng"
                };
            }

            dbUser.Status = 0;

            await _repo.UpdateAsync(dbUser);

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Đã xóa thông tin người dùng"
            };
        }

        public async Task<ApiResponse<bool>> UpdateUser(string userId, UpdateApplicationUserDto updateUser)
        {
            var dbUser = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.Id == userId);
            if (dbUser == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy thông tin người dùng"
                };
            }

            _mapper.Map(updateUser, dbUser);
            await _repo.UpdateAsync(dbUser);

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Cập nhật thông tin người dùng thành công"
            };
        }
    }
}
