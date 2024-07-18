using AppBusiness.Model.Pagination;
using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs.ProductAttributeDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IProductAttributeService
    {
        Task<ApiResponse<Pagination<List<ProductAttribute>>>> GetProductAttributes(int page);
        Task<ApiResponse<ProductAttribute>> GetProductAttribute(Guid productAttributeId);
        Task<ApiResponse<bool>> CreateProductAttribute(AddUpdateProductAttributeDto newProductAttribute);
        Task<ApiResponse<bool>> UpdateProductAttribute(Guid productAttributeId, AddUpdateProductAttributeDto updateProductAttribute);
        Task<ApiResponse<bool>> DeleteProductAttribute(Guid productAttributeId);
        Task<ApiResponse<List<ProductAttribute>>> GetProductAttributeSelect(Guid productId);
    }
}
