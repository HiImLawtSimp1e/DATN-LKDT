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

        public ProductAttributeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<bool>> CreateProductAttribute(AddUpdateProductAttributeDto newProductAttribute)
        {
            var attribute = _mapper.Map<ProductAttribute>(newProductAttribute);

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
                    IsSuccessed = false,
                    Message = "Không tìm thấy thuộc tính"
                };
            }

            _mapper.Map(updateProductAttribute, dbAttribute);
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
                    IsSuccessed = false,
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
                    IsSuccessed = false,
                    Message = "Không thể xóa thuộc tính vì nó có các giá trị sản phẩm liên quan."
                };
            }

            _context.ProductAttributes.Remove(dbAttribute);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                ResultObject = true,
                Message = "Xóa thuộc tính thành công"
            };
        }

        public async Task<ApiResponse<Pagination<List<ProductAttribute>>>> GetProductAttributes(int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling(_context.ProductAttributes.Where(p => !p.Deleted).Count() / pageResults);

            var attributes = await _context.ProductAttributes
                                             .OrderByDescending(p => p.LastModifiedOnDate)
                                             .Skip((page - 1) * (int)pageResults)
                                             .Take((int)pageResults)
                                             .ToListAsync();
            var pagingData = new Pagination<List<ProductAttribute>>
            {
                Content = attributes,
                CurrentPage = page,
                TotalPages = (int)pageCount,
                PageSize = (int)pageResults
            };

            return new ApiResponse<Pagination<List<ProductAttribute>>>
            {
                ResultObject = pagingData,
            };
        }

        public async Task<ApiResponse<ProductAttribute>> GetProductAttribute(Guid productAttributeId)
        {
            var attribute = await _context.ProductAttributes.FirstOrDefaultAsync(pa => pa.Id == productAttributeId);

            if (attribute == null)
            {
                return new ApiResponse<ProductAttribute>
                {
                    IsSuccessed = false,
                    Message = "Không tìm thấy thuộc tính"
                };
            }

            return new ApiResponse<ProductAttribute>
            {
                ResultObject = attribute,
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
                    IsSuccessed = false,
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
                ResultObject = missingAttribute
            };
        }
    }
}
