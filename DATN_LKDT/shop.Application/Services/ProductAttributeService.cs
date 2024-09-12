using AppBusiness.Model.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.ProductAttributeDto;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Services
{
    public class ProductAttributeService : IProductAttributeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public ProductAttributeService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }
        public async Task<ApiResponse<bool>> CreateProductAttribute(AddUpdateProductAttributeDto newProductAttribute)
        {
            var username = _authService.GetUserName();

            var attribute = _mapper.Map<ProductAttribute>(newProductAttribute);
            attribute.CreatedBy = username;

            _context.ProductAttributes.Add(attribute);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Message = "Thêm mới thuộc tính thành công"
            };
        }

        public async Task<ApiResponse<bool>> UpdateProductAttribute(Guid productAttributeId, AddUpdateProductAttributeDto updateProductAttribute)
        {
            var dbAttribute = await _context.ProductAttributes.FirstOrDefaultAsync(pa => pa.Id == productAttributeId);
            if (dbAttribute == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy thuộc tính"
                };
            }

            var username = _authService.GetUserName();

            _mapper.Map(updateProductAttribute, dbAttribute);
            dbAttribute.ModifiedAt = DateTime.Now;
            dbAttribute.ModifiedBy = username;

            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Message = "Đã cập nhật thuộc tính"
            };
        }

        public async Task<ApiResponse<bool>> DeleteProductAttribute(Guid productAttributeId)
        {
            var dbAttribute = await _context.ProductAttributes.FirstOrDefaultAsync(pa => pa.Id == productAttributeId);
            if (dbAttribute == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy thuộc tính"
                };
            }

            var dbAttributeValue = await _context.ProductValues
                                          .Where(pav => pav.ProductAttributeId == productAttributeId)
                                          .ToListAsync();

            if (dbAttributeValue.Any())
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không thể xóa thuộc tính vì nó có các giá trị sản phẩm liên quan."
                };
            }

            _context.ProductAttributes.Remove(dbAttribute);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Xóa thuộc tính thành công"
            };
        }

        public async Task<ApiResponse<Pagination<List<ProductAttribute>>>> GetProductAttributes(int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling(_context.ProductAttributes.Count() / pageResults);

            var attributes = await _context.ProductAttributes
                                             .OrderByDescending(p => p.ModifiedAt)
                                             .Skip((page - 1) * (int)pageResults)
                                             .Take((int)pageResults)
                                             .ToListAsync();
            var pagingData = new Pagination<List<ProductAttribute>>
            {
                Result = attributes,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<ProductAttribute>>>
            {
                Data = pagingData,
            };
        }

        public async Task<ApiResponse<ProductAttribute>> GetProductAttribute(Guid productAttributeId)
        {
            var attribute = await _context.ProductAttributes.FirstOrDefaultAsync(pa => pa.Id == productAttributeId);

            if (attribute == null)
            {
                return new ApiResponse<ProductAttribute>
                {
                    Success = false,
                    Message = "Không tìm thấy thuộc tính"
                };
            }

            return new ApiResponse<ProductAttribute>
            {
                Data = attribute,
            };
        }

        public async Task<ApiResponse<List<ProductAttribute>>> GetProductAttributeSelect(Guid productId)
        {
            var dbProduct = await _context.Products
                                       .Include(p => p.ProductValues)
                                       .ThenInclude(pav => pav.ProductAttribute)
                                       .Where(p => !p.Deleted)
                                       .FirstOrDefaultAsync(p => p.Id == productId);

            if (dbProduct == null)
            {
                return new ApiResponse<List<ProductAttribute>>
                {
                    Success = false,
                    Message = "Không tìm thấy sản phẩm"
                };
            }

            var allAttribute = await _context.ProductAttributes.ToListAsync();

            var existingAttributeIds = dbProduct.ProductValues
                                              .Where(pav => !pav.Deleted && pav.ProductAttribute != null)
                                              .Select(pav => pav.ProductAttribute.Id)
                                              .ToList();

            var missingAttribute = allAttribute.Where(pa => !existingAttributeIds
                                                 .Contains(pa.Id))
                                                 .ToList();

            return new ApiResponse<List<ProductAttribute>>
            {
                Data = missingAttribute
            };
        }

        public async Task<ApiResponse<Pagination<List<ProductAttribute>>>> SearchAdminProductAttributes(string searchText, int page, double pageResults)
        {
            var pageCount = Math.Ceiling((await FindProductsBySearchText(searchText)).Count / pageResults);

            var attributes = await _context.ProductAttributes
                                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()) && !p.Deleted)
                                .OrderByDescending(p => p.ModifiedAt)
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();

            if (attributes == null)
            {
                return new ApiResponse<Pagination<List<ProductAttribute>>>
                {
                    Success = false,
                    Message = "Không tìm thấy thuộc tính sản phẩm"
                };
            }

            var pagingData = new Pagination<List<ProductAttribute>>
            {
                Result = attributes,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<ProductAttribute>>>
            {
                Data = pagingData,
            };
        }

        private async Task<List<ProductAttribute>> FindProductsBySearchText(string searchText)
        {
            return await _context.ProductAttributes
                                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()) && !p.Deleted)
                                .ToListAsync();
        }
    }
}
