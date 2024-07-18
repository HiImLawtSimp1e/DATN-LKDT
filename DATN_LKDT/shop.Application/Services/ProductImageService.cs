using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.ProductImageDto;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductImageService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<ProductImage>> GetProductImage(Guid id)
        {
            var productImage = await _context.ProductImages.FirstOrDefaultAsync(pi => pi.Id == id);

            return new ApiResponse<ProductImage>
            {
                ResultObject = productImage,
            };
        }
        public async Task<ApiResponse<bool>> CreateProductImage(AddProductImageDto newImage)
        {
            try
            {
                var image = _mapper.Map<ProductImage>(newImage);

                // Kiểm tra nếu là ảnh chính mới
                // Nếu ảnh mới là ảnh chính => đặt ảnh chính hiện tại trong cơ sở dữ liệu thành không phải là ảnh chính
                if (image.IsMain == true)
                {
                    var mainImage = _context.ProductImages
                                         .Where(pi => pi.ProductId == image.ProductId && !pi.Deleted)
                                         .FirstOrDefault(pi => pi.IsMain);
                    var dbProduct = await _context.Products
                                           .Where(p => !p.Deleted)
                                           .FirstOrDefaultAsync(p => p.Id == image.ProductId);

                    // Nếu đã có ảnh chính trong cơ sở dữ liệu => đặt ảnh đó không phải là ảnh chính
                    if (mainImage != null && dbProduct != null)
                    {
                        mainImage.IsMain = false;
                        dbProduct.ImageUrl = image.ImageUrl;
                    }
                }

                _context.ProductImages.Add(image);
                await _context.SaveChangesAsync();
                return new ApiResponse<bool>
                {
                    ResultObject = true,
                    Message = "Tạo ảnh thành công!"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<bool>> DeleteProductImage(Guid id)
        {
            var dbImage = await _context.ProductImages
                                   .Where(pi => !pi.Deleted)
                                   .FirstOrDefaultAsync(pi => pi.Id == id);
            if (dbImage == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccessed = false,
                    Message = "Không tìm thấy ảnh"
                };
            }

            if (dbImage.IsMain == true)
            {
                return new ApiResponse<bool>
                {
                    IsSuccessed = false,
                    Message = "Không thể xóa ảnh chính"
                };
            }

            var someImageElse = await _context.ProductImages
                                         .Where(pi => pi.Id != dbImage.Id && !pi.Deleted && pi.IsActive)
                                         .FirstOrDefaultAsync(pi => pi.ProductId == dbImage.ProductId);
            if (someImageElse == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccessed = false,
                    Message = "Không thể xóa ảnh mặc định"
                };
            }

            try
            {
                dbImage.Deleted = true;
                await _context.SaveChangesAsync();
                return new ApiResponse<bool>
                {
                    ResultObject = true,
                    Message = "Xóa ảnh thành công!"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<bool>> UpdateProductImage(Guid id, UpdateProductImageDto updateImage)
        {
            var dbImage = await _context.ProductImages
                                     .Where(pi => !pi.Deleted)
                                     .FirstOrDefaultAsync(pi => pi.Id == id);
            if (dbImage == null)
            {
                return new ApiResponse<bool>
                {
                    IsSuccessed = false,
                    Message = "Không tìm thấy ảnh"
                };
            }

            // Kiểm tra nếu ảnh không hoạt động là ảnh chính
            // Nếu ảnh không hoạt động là ảnh chính => chọn ảnh khác làm ảnh chính
            if (updateImage.IsActive == false && dbImage.IsMain == true)
            {
                updateImage.IsMain = false;
                var someImageElse = await _context.ProductImages
                                          .Where(pi => pi.Id != dbImage.Id && !pi.Deleted && pi.IsActive)
                                          .FirstOrDefaultAsync(pi => pi.ProductId == dbImage.ProductId);
                var dbProduct = await _context.Products
                                         .Where(p => !p.Deleted)
                                         .FirstOrDefaultAsync(p => p.Id == dbImage.ProductId);

                // Nếu sản phẩm này có hơn 2 ảnh => chọn ngẫu nhiên 1 ảnh làm ảnh chính
                if (someImageElse != null && dbProduct != null)
                {
                    someImageElse.IsMain = true;
                    dbProduct.ImageUrl = someImageElse.ImageUrl;
                }
                else
                {
                    // Nếu sản phẩm này chỉ có một ảnh => từ chối sửa đổi đó
                    return new ApiResponse<bool>
                    {
                        IsSuccessed = false,
                        Message = "Không thể ngưng hoạt động ảnh mặc định"
                    };
                }
            }
            try
            {
                _mapper.Map(updateImage, dbImage);
                // Kiểm tra nếu ảnh đã được ánh xạ là ảnh chính
                // Nếu ảnh được sửa đổi là ảnh chính => kiểm tra xem cơ sở dữ liệu đã có ảnh chính chưa
                if (dbImage.IsMain == true)
                {
                    dbImage.IsActive = true;
                    var mainImage = _context.ProductImages
                                         .Where(pi => pi.ProductId == dbImage.ProductId && !pi.Deleted)
                                         .FirstOrDefault(pi => pi.IsMain);
                    var dbProduct = await _context.Products
                                         .Where(p => !p.Deleted)
                                         .FirstOrDefaultAsync(p => p.Id == dbImage.ProductId);
                    // Nếu đã có ảnh chính trong cơ sở dữ liệu => đặt ảnh đó không phải là ảnh chính
                    if (mainImage != null && dbProduct != null)
                    {
                        mainImage.IsMain = false;
                        dbProduct.ImageUrl = dbImage.ImageUrl;
                    }
                }
                await _context.SaveChangesAsync();
                return new ApiResponse<bool>
                {
                    IsSuccessed = true,
                    Message = "Cập nhật ảnh thành công!"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
}
