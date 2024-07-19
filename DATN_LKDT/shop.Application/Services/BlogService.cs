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

        public BlogService(IRepository<BlogEntity> repo, IMapper mapper, AppDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
        }
        public async Task<ApiResponse<bool>> CreateBlog(AddBlogDto newBlog)
        {
            var blog = _mapper.Map<BlogEntity>(newBlog);

            await _repo.InsertAsync(blog);
            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Tạo blog thành công"
            };
        }

        public async Task<ApiResponse<Pagination<List<BlogEntity>>>> GetAdminBlogs(int currentPage, int pageResults)
        {
            var pageCount = Math.Ceiling((double)(_context.Blogs.Where(b => !b.Deleted).Count() / pageResults));

            var blogs = await _context.Blogs
                               .Where(b => !b.Deleted) // Filter blog that status has deleted 
                               .OrderByDescending(b => b.ModifiedAt)
                               .Skip((currentPage - 1) * pageResults)
                               .Take(pageResults)
                               .ToListAsync();
            var pagingData = new Pagination<List<BlogEntity>>
            {
                Result = blogs,
                CurrentPage = currentPage,
                PageResults = pageResults,
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

        public async Task<ApiResponse<Pagination<List<CustomerBlogResponse>>>> GetBlogsAsync(int currentPage, int pageResults)
        {
            var pageCount = Math.Ceiling((double)(_context.Blogs.Where(b => b.IsActive && !b.Deleted).Count() / pageResults));

            var blogs = await _context.Blogs
                               .Where(b => b.IsActive && !b.Deleted) // Filter blog that status has deleted & status is unactive
                               .OrderByDescending(b => b.CreatedAt)
                               .Skip((currentPage - 1) * pageResults)
                               .Take(pageResults)
                               .ToListAsync();

            var result = _mapper.Map<List<CustomerBlogResponse>>(blogs);

            var pagingData = new Pagination<List<CustomerBlogResponse>>
            {
                Result = result,
                CurrentPage = currentPage,
                PageResults = pageResults,
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

            dbBlog.Deleted = true;

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

            _mapper.Map(updateBlog, dbBlog);
            dbBlog.ModifiedAt = DateTime.Now;

            await _repo.UpdateAsync(dbBlog);

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Update blog thành công"
            };
        }
    }
}
