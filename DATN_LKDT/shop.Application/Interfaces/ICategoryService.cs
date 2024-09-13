using AppBusiness.Model.Pagination;
using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs.CategoryDto;
using shop.Application.ViewModels.ResponseDTOs;
using shop.Application.ViewModels.ResponseDTOs.CustomerResponseDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse<List<CustomerCategoryResponseDto>>> GetCategoriesAsync();
        Task<ApiResponse<Pagination<List<Category>>>> GetAdminCategories(int page);
        Task<ApiResponse<Category>> GetAdminCategory(Guid categoryId);
        Task<ApiResponse<bool>> CreateCategory(AddCategoryDto newCategory);
        Task<ApiResponse<bool>> UpdateCategory(Guid categoryId, UpdateCategoryDto updateCategory);
        Task<ApiResponse<bool>> SoftDeleteCategory(Guid categoryId);
        Task<ApiResponse<List<CategorySelectResponseDto>>> GetCategoriesSelect();
        Task<ApiResponse<Pagination<List<Category>>>> SearchAdminCategories(string searchText, int page, double pageResults);
    }
}
