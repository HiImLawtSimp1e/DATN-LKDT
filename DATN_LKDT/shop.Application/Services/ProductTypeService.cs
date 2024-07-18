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

        public ProductTypeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> CreateProductType(AddUpdateProductTypeDto newProductType)
        {
            var productType = _mapper.Map<ProductType>(newProductType);
            _context.ProductTypes.Add(productType);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccessed = true,
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
                    IsSuccessed = false,
                    Message = "Không tìm thấy loại sản phẩm."
                };
            }

            _mapper.Map(updateProductType, dbProductType);
            dbProductType.LastModifiedOnDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccessed = true,
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
                    IsSuccessed = false,
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
                    IsSuccessed = false,
                    Message = "Không thể xóa loại sản phẩm vì có sản phẩm liên quan."
                };
            }

            _context.ProductTypes.Remove(dbProductType);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                IsSuccessed = true,
                Message = "Đã xóa loại sản phẩm!"
            };
        }

        public async Task<ApiResponse<Pagination<List<ProductType>>>> GetProductTypes(int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling(_context.ProductTypes.Where(p => !p.Deleted).Count() / pageResults);

            var productTypes = await _context.ProductTypes
                                             .OrderByDescending(p => p.LastModifiedOnDate)
                                             .Skip((page - 1) * (int)pageResults)
                                             .Take((int)pageResults)
                                             .ToListAsync();

            var pagingData = new Pagination<List<ProductType>>
            {
                Content = productTypes,
                CurrentPage = page,
                TotalPages = (int)pageCount,
                PageSize = (int)pageResults
            };

            return new ApiResponse<Pagination<List<ProductType>>>
            {
                ResultObject = pagingData,
            };
        }

        public async Task<ApiResponse<ProductType>> GetProductType(Guid productTypeId)
        {
            var productType = await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == productTypeId);

            if (productType == null)
            {
                return new ApiResponse<ProductType>
                {
                    IsSuccessed = false,
                    Message = "Không tìm thấy loại sản phẩm"
                };
            }

            return new ApiResponse<ProductType>
            {
                ResultObject = productType,
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
                    IsSuccessed = false,
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
                ResultObject = missingProductTypes
            };
        }

        public async Task<ApiResponse<List<ProductType>>> GetProductTypesSelect()
        {
            var types = await _context.ProductTypes
                                 .Where(pt => !pt.Deleted)
                                 .ToListAsync();
            return new ApiResponse<List<ProductType>>
            {
                ResultObject = types
            };
        }
    }
}
