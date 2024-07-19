using AppBusiness.Model.Pagination;
using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs.BlogDto;
using shop.Application.ViewModels.ResponseDTOs.CustomerResponseDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IBlogService
    {
        Task<ApiResponse<Pagination<List<BlogEntity>>>> GetAdminBlogs(int currentPage, int pageSize);
        Task<ApiResponse<Pagination<List<CustomerBlogResponse>>>> GetBlogsAsync(int currentPage, int pageSize);
        Task<ApiResponse<BlogEntity>> GetAdminSingleBlog(Guid id);
        Task<ApiResponse<CustomerBlogResponse>> GetSingleBlog(string slug);
        Task<ApiResponse<bool>> CreateBlog(AddBlogDto newBlog);
        Task<ApiResponse<bool>> UpdateBlog(Guid blogId, UpdateBlogDto updateBlog);
        Task<ApiResponse<bool>> SoftDeleteBlog(Guid blogId);
    }
}
