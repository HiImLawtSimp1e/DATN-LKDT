using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs;
using shop.Application.ViewModels.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface ICartService
    {
        Task<ApiResponse<List<CustomerCartItemDto>>> GetCartItems(Guid accountId);
        Task<ApiResponse<bool>> StoreCartItems(Guid accountId, List<StoreCartItemDto> items);
        Task<ApiResponse<bool>> AddToCart(Guid accountId, StoreCartItemDto newItem);
        Task<ApiResponse<bool>> UpdateQuantity(Guid accountId, StoreCartItemDto updateItem);
        Task<ApiResponse<bool>> RemoveFromCart(Guid accountId, Guid productId, Guid productTypeId);
    }
}
