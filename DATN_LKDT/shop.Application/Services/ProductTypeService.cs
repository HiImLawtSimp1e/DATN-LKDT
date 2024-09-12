using AppBusiness.Model.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.ProductDto;
using shop.Application.ViewModels.RequestDTOs.ProductTypeDto;
using shop.Application.ViewModels.ResponseDTOs.CustomerProductResponseDto;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public ProductTypeService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<ApiResponse<bool>> CreateProductType(AddUpdateProductTypeDto newProductType)
        {
            var username = _authService.GetUserName();

            var productType = _mapper.Map<ProductType>(newProductType);
            productType.CreatedBy = username;

            _context.ProductTypes.Add(productType);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Loại sản phẩm đã được tạo!"
            };
        }

        public async Task<ApiResponse<bool>> UpdateProductType(Guid productTypeId, AddUpdateProductTypeDto updateProductType)
        {
            var dbProductType = await _context.ProductTypes
                                             .FirstOrDefaultAsync(pt => pt.Id == productTypeId);
            if (dbProductType == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy loại sản phẩm."
                };
            }

            var username = _authService.GetUserName();

            _mapper.Map(updateProductType, dbProductType);
            dbProductType.ModifiedAt = DateTime.Now;
            dbProductType.ModifiedBy = username;

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Loại sản phẩm đã được cập nhật!"
            };
        }

        public async Task<ApiResponse<bool>> DeleteProductType(Guid productTypeId)
        {
            var dbProductType = await _context.ProductTypes
                                             .FirstOrDefaultAsync(pt => pt.Id == productTypeId);

            if (dbProductType == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy loại sản phẩm."
                };
            }

            var dbVariants = await _context.ProductVariants
                                           .Where(v => v.ProductTypeId == productTypeId)
                                           .ToListAsync();

            if (dbVariants.Any())
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không thể xóa loại sản phẩm vì có sản phẩm liên quan."
                };
            }

            _context.ProductTypes.Remove(dbProductType);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Đã xóa loại sản phẩm!"
            };
        }

        public async Task<ApiResponse<Pagination<List<ProductType>>>> GetProductTypes(int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling(_context.ProductTypes.Count() / pageResults);

            var productTypes = await _context.ProductTypes
                                             .OrderByDescending(p => p.ModifiedAt)
                                             .Skip((page - 1) * (int)pageResults)
                                             .Take((int)pageResults)
                                             .ToListAsync();

            var pagingData = new Pagination<List<ProductType>>
            {
                Result = productTypes,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<ProductType>>>
            {
                Data = pagingData,
            };
        }

        public async Task<ApiResponse<ProductType>> GetProductType(Guid productTypeId)
        {
            var productType = await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == productTypeId);

            if (productType == null)
            {
                return new ApiResponse<ProductType>
                {
                    Success = false,
                    Message = "Không tìm thấy loại sản phẩm"
                };
            }

            return new ApiResponse<ProductType>
            {
                Data = productType,
            };
        }

        public async Task<ApiResponse<List<ProductType>>> GetProductTypesSelectByProduct(Guid productId)
        {
            var dbProduct = await _context.Products
                                       .Include(p => p.ProductVariants)
                                       .ThenInclude(pv => pv.ProductType)
                                       .Where(p => !p.Deleted)
                                       .FirstOrDefaultAsync(p => p.Id == productId);

            if (dbProduct == null)
            {
                return new ApiResponse<List<ProductType>>
                {
                    Success = false,
                    Message = "Không tìm thấy sản phẩm"
                };
            }

            var allProductTypes = await _context.ProductTypes.ToListAsync();

            var existingProductTypeIds = dbProduct.ProductVariants
                                                  .Where(pv => !pv.Deleted && pv.ProductType != null)
                                                  .Select(pv => pv.ProductType.Id)
                                                  .ToList();

            var missingProductTypes = allProductTypes.Where(pt => !existingProductTypeIds
                                                         .Contains(pt.Id))
                                                         .ToList();

            return new ApiResponse<List<ProductType>>
            {
                Data = missingProductTypes
            };
        }

        public async Task<ApiResponse<List<ProductType>>> GetProductTypesSelect()
        {
            var types = await _context.ProductTypes
                                 .Where(pt => !pt.Deleted)
                                 .ToListAsync();
            return new ApiResponse<List<ProductType>>
            {
                Data = types
            };
        }

        public async Task<ApiResponse<Pagination<List<ProductType>>>> SearchAdminProductTypes(string searchText, int page, double pageResults)
        {
            var pageCount = Math.Ceiling((await FindProductTypesBySearchText(searchText)).Count / pageResults);

            var types = await _context.ProductTypes
                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()) && !p.Deleted)
                .OrderByDescending(p => p.ModifiedAt)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            if (types == null)
            {
                return new ApiResponse<Pagination<List<ProductType>>>
                {
                    Success = false,
                    Message = "Không tìm thấy loại sản phẩm"
                };
            }

            var pagingData = new Pagination<List<ProductType>>
            {
                Result = types,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<ProductType>>>
            {
                Data = pagingData,
            };
        }

        private async Task<List<ProductType>> FindProductTypesBySearchText(string searchText)
        {
            return await _context.ProductTypes
                                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()) && !p.Deleted)
                                .ToListAsync();
        }
    }
}
