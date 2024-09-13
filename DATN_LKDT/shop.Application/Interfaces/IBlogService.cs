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
        #region CustomerBlogService
        Task<ApiResponse<Pagination<List<CustomerBlogResponse>>>> GetBlogsAsync(int page, double pageResults);
        Task<ApiResponse<CustomerBlogResponse>> GetSingleBlog(string slug);
        #endregion CustomerBlogService

        #region AdminBlogService
        Task<ApiResponse<Pagination<List<BlogEntity>>>> GetAdminBlogs(int page, double pageResults);
        Task<ApiResponse<BlogEntity>> GetAdminSingleBlog(Guid id);
        Task<ApiResponse<bool>> CreateBlog(AddBlogDto newBlog);
        Task<ApiResponse<bool>> UpdateBlog(Guid blogId, UpdateBlogDto updateBlog);
        Task<ApiResponse<bool>> SoftDeleteBlog(Guid blogId);
        Task<ApiResponse<Pagination<List<BlogEntity>>>> SearchAdminBlogs(string searchText, int page, double pageResults);
        #endregion AdminBlogService
    }
}
