using AppBusiness.Model.Pagination;
using AutoMapper;
using MicroBase.Entity.Repositories;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs;
using shop.Application.ViewModels.ResponseDTOs;
using shop.Domain.Entities;
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

        public BlogService(IRepository<BlogEntity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ApiResponse<bool>> CreateBlog(AddBlogDto newBlog)
        {
            var blog = _mapper.Map<BlogEntity>(newBlog);

            await _repo.InsertAsync(blog);
            return new ApiResponse<bool>
            {
                ResultObject = true,
                IsSuccessed = true,
                Message = "Tạo blog thành công"
            };
        }

        public Task<ApiResponse<Pagination<List<BlogEntity>>>> GetAdminBlogs(int currentPage, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<BlogEntity>> GetAdminSingleBlog(Guid id)
        {
            var blog = await _repo.GetByIdAsync(id);
            if(blog == null)
            {
                return new ApiResponse<BlogEntity>
                {
                    IsSuccessed = false,
                    Message = "Không tìm thấy blog"
                };
            }
            return new ApiResponse<BlogEntity>
            {
                ResultObject = blog,
                IsSuccessed = true,
            };
        }

        public Task<ApiResponse<Pagination<List<CustomerBlogResponse>>>> GetBlogsAsync(int currentPage, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<CustomerBlogResponse>> GetSingleBlog(Guid id)
        {
            var blog = await _repo.GetByIdAsync(id);
            var result = _mapper.Map<CustomerBlogResponse>(blog);
            if (blog == null)
            {
                return new ApiResponse<CustomerBlogResponse>
                {
                    IsSuccessed = false,
                    Message = "Không tìm thấy blog"
                };
            }
            return new ApiResponse<CustomerBlogResponse>
            {
                ResultObject = result,
                IsSuccessed = true,
            };
        }

        public Task<ApiResponse<bool>> SoftDeleteBlog(Guid blogId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<bool>> UpdateBlog(Guid blogId, UpdateBlogDto updateBlog)
        {
            var dbBlog = await _repo.GetByIdAsync(blogId);
            if (dbBlog == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccessed = false,
                    Message = "Không tìm thấy blog"
                };
            }

            _mapper.Map(updateBlog, dbBlog);
            dbBlog.LastModifiedOnDate = DateTime.Now;

            return new ApiResponse<bool>
            {
                ResultObject = true,
                IsSuccessed = true,
            };
        }
    }
}
