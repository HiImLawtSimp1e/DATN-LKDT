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

        public CategoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<bool>> CreateCategory(AddCategoryDto newCategory)
        {
            var category = _mapper.Map<Category>(newCategory);

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
            category.Deleted = true;
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Message = "Đã xóa danh mục"
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

            _mapper.Map(updateCategory, dbCategory);
            dbCategory.ModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Message = "Danh mục đã được cập nhật"
            };
        }
    }
}
