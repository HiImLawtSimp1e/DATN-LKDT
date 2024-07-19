using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs.ProductVariantDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IProductVariantService
    {
        Task<ApiResponse<ProductVariant>> GetVartiant(Guid productId, Guid productTypeId);
        Task<ApiResponse<bool>> AddVariant(Guid productId, AddProductVariantDto newVariant);
        Task<ApiResponse<bool>> UpdateVariant(Guid productId, UpdateProductVariantDto updateVariant);
        Task<ApiResponse<bool>> SoftDeleteVariant(Guid productTypeId, Guid productId);

    }
}
