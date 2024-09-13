using AppBusiness.Model.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.CategoryDto;
using shop.Application.ViewModels.ResponseDTOs;
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
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public CategoryService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }
        public async Task<ApiResponse<bool>> CreateCategory(AddCategoryDto newCategory)
        {
            var username = _authService.GetUserName();

            var category = _mapper.Map<Category>(newCategory);
            category.CreatedBy = username;

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Message = "Thêm danh mục thành công"
            };
        }

        public async Task<ApiResponse<Pagination<List<Category>>>> GetAdminCategories(int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling(_context.Categories.Where(p => !p.Deleted).Count() / pageResults);

            var categories = await _context.Categories
                  .Where(c => !c.Deleted)
                  .OrderByDescending(p => p.ModifiedAt)
                  .Skip((page - 1) * (int)pageResults)
                  .Take((int)pageResults)
                  .ToListAsync();

            var pagingData = new Pagination<List<Category>>
            {
                Result = categories,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<Category>>>
            {
                Data = pagingData,
            };
        }

        public async Task<ApiResponse<Category>> GetAdminCategory(Guid categoryId)
        {
            var category = await _context.Categories
                .Where(c => !c.Deleted)
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            return new ApiResponse<Category>
            {
                Data = category
            };
        }

        public async Task<ApiResponse<List<CustomerCategoryResponseDto>>> GetCategoriesAsync()
        {
            var categories = await _context.Categories
                   .Where(c => !c.Deleted && c.IsActive)
                   .ToListAsync();

            var result = _mapper.Map<List<CustomerCategoryResponseDto>>(categories);

            return new ApiResponse<List<CustomerCategoryResponseDto>>
            {
                Data = result
            };
        }

        public async Task<ApiResponse<bool>> UpdateCategory(Guid categoryId, UpdateCategoryDto updateCategory)
        {
            var dbCategory = await _context.Categories
                                         .Where(c => !c.Deleted)
                                         .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (dbCategory == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy danh mục"
                };
            }

            var username = _authService.GetUserName();

            _mapper.Map(updateCategory, dbCategory);
            dbCategory.ModifiedAt = DateTime.Now;
            dbCategory.ModifiedBy = username;

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Message = "Danh mục đã được cập nhật"
            };
        }


        public async Task<ApiResponse<bool>> SoftDeleteCategory(Guid categoryId)
        {
            var category = await _context.Categories
                                      .Where(c => !c.Deleted)
                                      .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy danh mục"
                };
            }

            var username = _authService.GetUserName();

            category.Deleted = true;
            category.ModifiedAt = DateTime.Now;
            category.ModifiedBy = username;

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Message = "Đã xóa danh mục"
            };
        }

        public async Task<ApiResponse<List<CategorySelectResponseDto>>> GetCategoriesSelect()
        {
            var categories = await _context.Categories
                                   .Where(c => !c.Deleted)
                                   .ToListAsync();

            var result = _mapper.Map<List<CategorySelectResponseDto>>(categories);

            return new ApiResponse<List<CategorySelectResponseDto>>
            {
                Data = result
            };
        }

        public async Task<ApiResponse<Pagination<List<Category>>>> SearchAdminCategories(string searchText, int page, double pageResults)
        {
            var pageCount = Math.Ceiling((await FindAdminCategoriesBySearchText(searchText)).Count / pageResults);

            var categories = await _context.Categories
                .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                && !p.Deleted)
                .OrderByDescending(p => p.ModifiedAt)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            if (categories == null)
            {
                return new ApiResponse<Pagination<List<Category>>>
                {
                    Success = false,
                    Message = "Không tìm thấy danh mục sản phẩm"
                };
            }

            var pagingData = new Pagination<List<Category>>
            {
                Result = categories,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<Category>>>
            {
                Data = pagingData,
            };
        }

        private async Task<List<Category>> FindAdminCategoriesBySearchText(string searchText)
        {
            return await _context.Categories
                                .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                                    && !p.Deleted)
                                .ToListAsync();
        }
    }
}
