using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.ProductVariantDto;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Services
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductVariantService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<bool>> AddVariant(Guid productId, AddProductVariantDto newVariant)
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

            // Kiểm tra loại sản phẩm tồn tại
            var dbProductType = await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == newVariant.ProductTypeId);
            if (dbProductType == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy loại sản phẩm"
                };
            }

            // Kiểm tra biến thể tồn tại
            var existingVariant = await _context.ProductVariants
                                                .Where(v => v.ProductId == productId)
                                                .FirstOrDefaultAsync(v => v.ProductTypeId == newVariant.ProductTypeId);

            // Nếu biến thể tồn tại, từ chối tạo biến thể mới
            if (existingVariant != null && !existingVariant.Deleted)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Biến thể loại sản phẩm đã tồn tại!"
                };
            }

            // Nếu biến thể đã bị xóa, khôi phục biến thể
            if (existingVariant != null && existingVariant.Deleted)
            {
                existingVariant.Deleted = false;
                existingVariant.Price = newVariant.Price;
                existingVariant.OriginalPrice = newVariant.OriginalPrice;

                await _context.SaveChangesAsync();
                return new ApiResponse<bool>
                {
                    Data = true,
                    Message = "Đã thêm biến thể loại sản phẩm!"
                };
            }

            try
            {
                var variant = _mapper.Map<ProductVariant>(newVariant);
                variant.ProductId = productId;
                variant.IsActive = true;

                _context.ProductVariants.Add(variant);
                dbProduct.ModifiedAt = DateTime.Now;

                await _context.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    Data = true,
                    Message = "Đã thêm biến thể loại sản phẩm"
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

        public async Task<ApiResponse<ProductVariant>> GetVartiant(Guid productId, Guid productTypeId)
        {
            var variant = await _context.ProductVariants
                                            .Where(v => !v.Deleted && v.ProductId == productId)
                                            .Include(v => v.ProductType)
                                            .FirstOrDefaultAsync(v => v.ProductTypeId == productTypeId);
            if (variant == null)
            {
                return new ApiResponse<ProductVariant>
                {
                    Success = false,
                    Message = "Không tìm thấy biến thể"
                };
            }

            return new ApiResponse<ProductVariant>
            {
                Data = variant
            };
        }

        public async Task<ApiResponse<bool>> SoftDeleteVariant(Guid productTypeId, Guid productId)
        {
            var variant = await _context.ProductVariants
                                         .Where(v => !v.Deleted && v.ProductId == productId)
                                         .FirstOrDefaultAsync(v => v.ProductTypeId == productTypeId);
            if (variant == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy biến thể"
                };
            }

            variant.Deleted = true;
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Message = "Đã xóa biến thể"
            };
        }

        public async Task<ApiResponse<bool>> UpdateVariant(Guid productId, UpdateProductVariantDto updateVariant)
        {
            var dbVariant = await _context.ProductVariants
                                         .Where(v => !v.Deleted && v.ProductTypeId == updateVariant.ProductTypeId)
                                         .FirstOrDefaultAsync(v => v.ProductId == productId);
            var dbProduct = await _context.Products
                                          .Where(v => !v.Deleted)
                                          .FirstOrDefaultAsync(p => p.Id == productId);
            if (dbVariant == null || dbProduct == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy biến thể sản phẩm"
                };
            }

            _mapper.Map(updateVariant, dbVariant);
            dbProduct.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Đã cập nhật biến thể sản phẩm thành công!"
            };
        }
    }
}
