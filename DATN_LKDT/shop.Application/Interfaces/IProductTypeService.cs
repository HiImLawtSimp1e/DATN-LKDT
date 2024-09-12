using AppBusiness.Model.Pagination;
using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs.ProductTypeDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IProductTypeService
    {
        Task<ApiResponse<Pagination<List<ProductType>>>> GetProductTypes(int page);
        Task<ApiResponse<ProductType>> GetProductType(Guid productTypeId);
        Task<ApiResponse<bool>> CreateProductType(AddUpdateProductTypeDto newProductType);
        Task<ApiResponse<bool>> UpdateProductType(Guid productTypeId, AddUpdateProductTypeDto updateProductType);
        Task<ApiResponse<bool>> DeleteProductType(Guid productTypeId);
        Task<ApiResponse<List<ProductType>>> GetProductTypesSelect();
        Task<ApiResponse<List<ProductType>>> GetProductTypesSelectByProduct(Guid productId);
        Task<ApiResponse<Pagination<List<ProductType>>>> SearchAdminProductTypes(string searchText, int page, double pageResults);
    }
}
