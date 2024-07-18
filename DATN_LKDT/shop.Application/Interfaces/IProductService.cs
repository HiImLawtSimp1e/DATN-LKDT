using AppBusiness.Model.Pagination;
using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs.ProductDto;
using shop.Application.ViewModels.ResponseDTOs.CustomerProductResponseDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse<Pagination<List<CustomerProductResponseDto>>>> GetProductsAsync(int page, double pageResults);
        Task<ApiResponse<Pagination<List<Product>>>> GetAdminProducts(int page, double pageResults);
        Task<ApiResponse<Product>> GetAdminSingleProduct(Guid id);
        Task<ApiResponse<CustomerProductResponseDto>> GetProductBySlug(string slug);
        Task<ApiResponse<Pagination<List<CustomerProductResponseDto>>>> GetProductsByCategory(string categorySlug, int page, double pageResults);
        Task<ApiResponse<bool>> CreateProduct(AddProductDto newProduct);
        Task<ApiResponse<bool>> UpdateProduct(Guid id, UpdateProductDto updateProduct);
        Task<ApiResponse<bool>> SoftDeleteProduct(Guid productId);
        Task<ApiResponse<Pagination<List<CustomerProductResponseDto>>>> SearchProducts(string searchText, int page, double pageResults);
        Task<ApiResponse<List<string>>> GetProductSearchSuggestions(string seacrchText);
    }
}
