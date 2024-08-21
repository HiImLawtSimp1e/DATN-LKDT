using AppBusiness.Model.Pagination;
using AutoMapper;
using MicroBase.Entity.Repositories;
using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.BlogDto;
using shop.Application.ViewModels.ResponseDTOs.CustomerResponseDto;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<BlogEntity> _repo;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;

        public BlogService(IRepository<BlogEntity> repo, IMapper mapper, AppDbContext context, IAuthService authService)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
            _authService = authService;
        }
        public async Task<ApiResponse<bool>> CreateBlog(AddBlogDto newBlog)
        {
            var username = _authService.GetUserName();

            var blog = _mapper.Map<BlogEntity>(newBlog);
            blog.CreatedBy = username;

            await _repo.InsertAsync(blog);
            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Tạo blog thành công"
            };
        }

        public async Task<ApiResponse<Pagination<List<BlogEntity>>>> GetAdminBlogs(int page, double pageResults)
        {
            var pageCount = Math.Ceiling(_context.Blogs.Where(b => !b.Deleted).Count() / pageResults);

            var blogs = await _context.Blogs
                               .Where(b => !b.Deleted) // Filter blog that status has deleted 
                               .OrderByDescending(b => b.ModifiedAt)
                               .Skip((page - 1) * (int)pageResults)
                               .Take((int)pageResults)
                               .ToListAsync();
            var pagingData = new Pagination<List<BlogEntity>>
            {
                Result = blogs,
                CurrentPage = page,
                PageResults = (int)pageResults,
                Pages = (int)pageCount
            };

            return new ApiResponse<Pagination<List<BlogEntity>>>
            {
                Data = pagingData
            };

        }

        public async Task<ApiResponse<BlogEntity>> GetAdminSingleBlog(Guid id)
        {
            var blog = await _repo.GetByIdAsync(id);
            if(blog == null)
            {
                return new ApiResponse<BlogEntity>
                {
                    Success = false,
                    Message = "Không tìm thấy blog"
                };
            }
            return new ApiResponse<BlogEntity>
            {
                Data = blog
            };
        }

        public async Task<ApiResponse<Pagination<List<CustomerBlogResponse>>>> GetBlogsAsync(int page, double pageResults)
        {
            var pageCount = Math.Ceiling(_context.Blogs.Where(b => b.IsActive && !b.Deleted).Count() / pageResults);

            var blogs = await _context.Blogs
                               .Where(b => b.IsActive && !b.Deleted) // Filter blog that status has deleted & status is unactive
                               .OrderByDescending(b => b.CreatedAt)
                               .Skip((page - 1) * (int)pageResults)
                               .Take((int)pageResults)
                               .ToListAsync();

            var result = _mapper.Map<List<CustomerBlogResponse>>(blogs);

            var pagingData = new Pagination<List<CustomerBlogResponse>>
            {
                Result = result,
                CurrentPage = page,
                PageResults = (int)pageResults,
                Pages = (int)pageCount
            };

            return new ApiResponse<Pagination<List<CustomerBlogResponse>>>
            {
                Data = pagingData
            };
        }

        public async Task<ApiResponse<CustomerBlogResponse>> GetSingleBlog(string slug)
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Slug == slug);
            var result = _mapper.Map<CustomerBlogResponse>(blog);
            if (blog == null)
            {
                return new ApiResponse<CustomerBlogResponse>
                {
                    Success = false,
                    Message = "Không tìm thấy blog"
                };
            }
            return new ApiResponse<CustomerBlogResponse>
            {
                Data = result
            };
        }

        public async Task<ApiResponse<bool>> SoftDeleteBlog(Guid blogId)
        {
            var dbBlog = await _repo.GetByIdAsync(blogId);
            if (dbBlog == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy blog"
                };
            }

            var username = _authService.GetUserName();

            dbBlog.Deleted = true;
            dbBlog.ModifiedAt = DateTime.Now;
            dbBlog.ModifiedBy = username;

            await _repo.UpdateAsync(dbBlog);

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Xóa blog thành công"
            };
        }

        public async Task<ApiResponse<bool>> UpdateBlog(Guid blogId, UpdateBlogDto updateBlog)
        {
            var dbBlog = await _repo.GetByIdAsync(blogId);
            if (dbBlog == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy blog"
                };
            }

            var username = _authService.GetUserName();

            _mapper.Map(updateBlog, dbBlog);
            dbBlog.ModifiedAt = DateTime.Now;
            dbBlog.ModifiedBy = username;

            await _repo.UpdateAsync(dbBlog);

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Update blog thành công"
            };
        }
    }
}
