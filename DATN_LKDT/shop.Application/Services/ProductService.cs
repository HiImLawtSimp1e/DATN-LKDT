using AppBusiness.Model.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.ProductDto;
using shop.Application.ViewModels.ResponseDTOs.CustomerProductResponseDto;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = shop.Domain.Entities.Product;

namespace shop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public ProductService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }
        public async Task<ApiResponse<bool>> CreateProduct(AddProductDto newProduct)
        {
            //Lấy username tài khoản thực hiện tác vụ
            var username = _authService.GetUserName();

            //Map DTO to Product Entity
            var product = _mapper.Map<Product>(newProduct);
            product.CreatedBy = username;

            // Thêm hình ảnh sản phẩm
            var productImage = new ProductImage
            {
                ImageUrl = product.ImageUrl,
                IsActive = true,
                IsMain = true,
            };

            product.ProductImages.Add(productImage);

            // Thêm biến thể sản phẩm(loại sản phẩm)
            var variant = new ProductVariant
            {
                ProductId = product.Id,
                ProductTypeId = newProduct.ProductTypeId,
                Price = newProduct.Price,
                OriginalPrice = newProduct.OriginalPrice
            };

            product.ProductVariants.Add(variant);

            // Thêm sản phẩm
            _context.Products.Add(product);

            // Cập nhật thay đổi cho database từ context
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Thêm mới sản phẩm thành công"
            };

        }

        public async Task<ApiResponse<bool>> UpdateProduct(Guid id, UpdateProductDto updateProduct)
        {
            var dbProduct = await _context.Products
                                   .Where(p => !p.Deleted)
                                   .Include(p => p.ProductVariants)
                                   .FirstOrDefaultAsync(p => p.Id == id);

            if (dbProduct == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy sản phẩm"
                };
            }

            var username = _authService.GetUserName();

            _mapper.Map(updateProduct, dbProduct);
            dbProduct.ModifiedAt = DateTime.Now;
            dbProduct.ModifiedBy = username;

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Cập nhật sản phẩm thành công!"
            };
        }

        public async Task<ApiResponse<bool>> SoftDeleteProduct(Guid productId)
        {
            var product = await _context.Products
                                 .Where(p => !p.Deleted)
                                 .Include(p => p.ProductVariants)
                                 .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy sản phẩm"
                };
            }

            var username = _authService.GetUserName();

            product.Deleted = true;
            product.ModifiedAt = DateTime.Now;
            product.ModifiedBy = username;

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Message = "Xóa sản phẩm thành công"
            };
        }

        public async Task<ApiResponse<Pagination<List<Product>>>> GetAdminProducts(int page, double pageResults)
        {
            var pageCount = Math.Ceiling(_context.Products.Where(p => !p.Deleted).Count() / pageResults);

            var products = await _context.Products
                  .Where(p => !p.Deleted)
                  .OrderByDescending(p => p.ModifiedAt)
                  .Skip((page - 1) * (int)pageResults)
                  .Take((int)pageResults)
                  .Include(p => p.ProductVariants.Where(pv => !pv.Deleted))
                  .ThenInclude(pv => pv.ProductType)
                  .ToListAsync();

            var pagingData = new Pagination<List<Product>>
            {
                Result = products,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<Product>>>
            {
                Data = pagingData
            };

        }

        public async Task<ApiResponse<Product>> GetAdminSingleProduct(Guid id)
        {
            var product = await _context.Products
                 .Where(p => !p.Deleted)
                 .Include(p => p.ProductVariants.Where(pv => !pv.Deleted))
                 .ThenInclude(pv => pv.ProductType)
                 .Include(p => p.ProductValues.Where(pav => !pav.Deleted))
                 .ThenInclude(pav => pav.ProductAttribute)
                 .Include(p => p.ProductImages.Where(pv => !pv.Deleted))
                 .FirstOrDefaultAsync(p => p.Id == id);
            return new ApiResponse<Product>
            {
                Data = product
            };
        }

        public async Task<ApiResponse<CustomerProductResponseDto>> GetProductBySlug(string slug)
        {
            var product = await _context.Products
                  .Include(p => p.ProductVariants.Where(pv => pv.IsActive && !pv.Deleted))
                  .ThenInclude(v => v.ProductType)
                  .Include(p => p.ProductValues.Where(pav => pav.IsActive && !pav.Deleted))
                  .ThenInclude(pav => pav.ProductAttribute)
                  .Include(p => p.ProductImages.Where(pi => pi.IsActive && !pi.Deleted))
                  .FirstOrDefaultAsync(p => p.Slug == slug && p.IsActive && !p.Deleted);
            if (product == null)
            {
                return new ApiResponse<CustomerProductResponseDto>
                {
                    Success = false,
                    Message = "Không tìm thấy sản phẩm"
                };
            }

            var result = _mapper.Map<CustomerProductResponseDto>(product);

            return new ApiResponse<CustomerProductResponseDto>
            {
                Data = result
            };
        }

        public async Task<ApiResponse<Pagination<List<CustomerProductResponseDto>>>> GetProductsAsync(int page, double pageResults)
        {
            var pageCount = Math.Ceiling(_context.Products.Where(p => !p.Deleted && p.IsActive).Count() / pageResults);

            var products = await _context.Products
                  .Where(p => !p.Deleted && p.IsActive)
                  .OrderByDescending(p => p.CreatedAt)
                  .Skip((page - 1) * (int)pageResults)
                  .Take((int)pageResults)
                  .Include(p => p.ProductVariants.Where(pv => !pv.Deleted && pv.IsActive))
                  .ThenInclude(pv => pv.ProductType)
                  .ToListAsync();

            var result = _mapper.Map<List<CustomerProductResponseDto>>(products);

            var pagingData = new Pagination<List<CustomerProductResponseDto>>
            {
                Result = result,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<CustomerProductResponseDto>>>
            {
                Data = pagingData
            };
        }

        public async Task<ApiResponse<Pagination<List<CustomerProductResponseDto>>>> GetProductsByCategory(string categorySlug, int page, double pageResults)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Slug == categorySlug);
            if (category == null)
            {
                return new ApiResponse<Pagination<List<CustomerProductResponseDto>>>
                {
                    Success = false,
                    Message = "Không tìm thấy danh mục sản phẩm"
                };
            }

            var pageCount = Math.Ceiling(_context.Products.Where(p => !p.Deleted && p.IsActive && p.CategoryId == category.Id).Count() / pageResults);

            var products = await _context.Products
                  .Where(p => !p.Deleted && p.IsActive && p.CategoryId == category.Id)
                  .OrderByDescending(p => p.CreatedAt)
                  .Skip((page - 1) * (int)pageResults)
                  .Take((int)pageResults)
                  .Include(p => p.ProductVariants.Where(pv => !pv.Deleted && pv.IsActive))
                  .ThenInclude(pv => pv.ProductType)
                  .ToListAsync();

            var result = _mapper.Map<List<CustomerProductResponseDto>>(products);

            var pagingData = new Pagination<List<CustomerProductResponseDto>>
            {
                Result = result,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<CustomerProductResponseDto>>>
            {
                Data = pagingData,
            };
        }

        public async Task<ApiResponse<List<string>>> GetProductSearchSuggestions(string seacrchText)
        {
            var products = await FindProductsBySearchText(seacrchText);

            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.Contains(seacrchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }

                if (product.Description != null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation).Distinct().ToArray();
                    var words = product.Description.Split().Select(w => w.Trim(punctuation));
                    foreach (var word in words)
                    {
                        if (word.Contains(seacrchText, StringComparison.OrdinalIgnoreCase) && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            return new ApiResponse<List<string>>
            {
                Data = result
            };
        }

        public async Task<ApiResponse<Pagination<List<CustomerProductResponseDto>>>> SearchProducts(string searchText, int page, double pageResults)
        {
            var pageCount = Math.Ceiling((await FindProductsBySearchText(searchText)).Count / pageResults);


            var products = await _context.Products
                .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                || p.Description.ToLower().Contains(searchText.ToLower())
                && p.IsActive && !p.Deleted)
                .Include(p => p.ProductVariants)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            if(products == null) 
            {
                return new ApiResponse<Pagination<List<CustomerProductResponseDto>>>
                {
                    Success = false,
                    Message = "Không tìm thấy sản phẩm"
                };
            }

            var result = _mapper.Map<List<CustomerProductResponseDto>>(products);

            var pagingData = new Pagination<List<CustomerProductResponseDto>>
            {
                Result = result,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<CustomerProductResponseDto>>>
            {
                Data = pagingData,
            };
        }

        private async Task<List<Product>> FindProductsBySearchText(string searchText)
        {
            return await _context.Products
                                .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower()) &&
                                    p.IsActive && !p.Deleted)
                                .ToListAsync();
        }
    }
}
