using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.ProductValueDto;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Services
{
    public class ProductValueService : IProductValueService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductValueService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<bool>> AddAttributeValue(Guid productId, AddProductValueDto newAttributeValue)
        {
            // Kiểm tra sản phẩm tồn tại
            var dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId && !p.Deleted);
            if (dbProduct == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy sản phẩm"
                };
            }

            // Kiểm tra thuộc tính sản phẩm tồn tại
            var dbProductAttribute = await _context.ProductAttributes.FirstOrDefaultAsync(pa => pa.Id == newAttributeValue.ProductAttributeId);
            if (dbProductAttribute == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy thuộc tính sản phẩm"
                };
            }

            // Kiểm tra giá trị thuộc tính sản phẩm tồn tại
            var existingAttributeValue = await _context.ProductValues
                                                    .Where(pv => pv.ProductId == productId && !pv.Deleted)
                                                    .FirstOrDefaultAsync(pv => pv.ProductAttributeId == newAttributeValue.ProductAttributeId);

            // Nếu giá trị thuộc tính tồn tại và chưa bị xóa, từ chối tạo giá trị thuộc tính
            if (existingAttributeValue != null && !existingAttributeValue.Deleted)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Giá trị thuộc tính sản phẩm đã tồn tại!"
                };
            }

            // Nếu giá trị thuộc tính đã bị xóa, phục hồi giá trị thuộc tính
            if (existingAttributeValue != null && existingAttributeValue.Deleted)
            {
                existingAttributeValue.Deleted = false;
                existingAttributeValue.Value = newAttributeValue.Value;

                await _context.SaveChangesAsync();
                return new ApiResponse<bool>
                {
                    Data = true,
                    Message = "Đã tạo giá trị thuộc tính sản phẩm thành công!"
                };
            }

            try
            {
                var attributeValue = _mapper.Map<ProductValue>(newAttributeValue);
                attributeValue.ProductId = productId;
                attributeValue.IsActive = true;

                _context.ProductValues.Add(attributeValue);
                dbProduct.ModifiedAt = DateTime.Now;

                await _context.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    Data = true,
                    Message = "Đã tạo giá trị thuộc tính sản phẩm thành công!"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = $"Lỗi: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<ProductValue>> GetAttributeValue(Guid productId, Guid productAttributeId)
        {
            var attributeValue = await _context.ProductValues
                                        .Where(pv => !pv.Deleted && pv.ProductId == productId)
                                        .Include(pv => pv.ProductAttribute)
                                        .FirstOrDefaultAsync(pv => pv.ProductAttributeId == productAttributeId);
            if (attributeValue == null)
            {
                return new ApiResponse<ProductValue>
                {
                    Success = false,
                    Message = "Không tìm thấy giá trị thuộc tính"
                };
            }

            return new ApiResponse<ProductValue>
            {
                Data = attributeValue
            };
        }

        public async Task<ApiResponse<bool>> SoftDeleteAttributeValue(Guid productId, Guid productAttributeId)
        {
            var attributeValue = await _context.ProductValues
                                       .Where(pv => !pv.Deleted && pv.ProductId == productId)
                                       .FirstOrDefaultAsync(pv => pv.ProductAttributeId == productAttributeId);
            if (attributeValue == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy giá trị thuộc tính"
                };
            }

            attributeValue.Deleted = true;
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Message = "Đã xóa giá trị thuộc tính"
            };
        }

        public async Task<ApiResponse<bool>> UpdateAttributeValue(Guid productId, UpdateProductValueDto updateAttributeValue)
        {
            var dbAttributeValue = await _context.ProductValues
                                          .Where(pv => !pv.Deleted && pv.ProductAttributeId == updateAttributeValue.ProductAttributeId)
                                          .FirstOrDefaultAsync(pv => pv.ProductId == productId);
            var dbProduct = await _context.Products
                                          .Where(p => !p.Deleted)
                                          .FirstOrDefaultAsync(p => p.Id == productId);
            if (dbAttributeValue == null || dbProduct == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy giá trị thuộc tính"
                };
            }

            _mapper.Map(updateAttributeValue, dbAttributeValue);
            dbProduct.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Đã cập nhật giá trị thuộc tính thành công!"
            };
        }
    }
}
