using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs.ProductImageDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IProductImageService
    {
        Task<ApiResponse<ProductImage>> GetProductImage(Guid id);
        Task<ApiResponse<bool>> CreateProductImage(AddProductImageDto newImage);
        Task<ApiResponse<bool>> UpdateProductImage(Guid id, UpdateProductImageDto updateImage);
        Task<ApiResponse<bool>> DeleteProductImage(Guid id);
    }
}
