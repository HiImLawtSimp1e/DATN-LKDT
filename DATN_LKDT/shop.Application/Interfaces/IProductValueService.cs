using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs.ProductValueDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IProductValueService
    {
        Task<ApiResponse<ProductValue>> GetAttributeValue(Guid productId, Guid productAttributeId);
        Task<ApiResponse<bool>> AddAttributeValue(Guid productId, AddProductValueDto newAttributeValue);
        Task<ApiResponse<bool>> UpdateAttributeValue(Guid productId, UpdateProductValueDto updateAttributeValue);
        Task<ApiResponse<bool>> SoftDeleteAttributeValue(Guid productId, Guid productAttributeId);
    }
}
