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
        Task<ApiResponse<List<CustomerCartItemDto>>> GetCartItems();
        Task<ApiResponse<bool>> StoreCartItems(List<StoreCartItemDto> items);
        Task<ApiResponse<bool>> AddToCart(StoreCartItemDto newItem);
        Task<ApiResponse<bool>> UpdateQuantity(StoreCartItemDto updateItem);
        Task<ApiResponse<bool>> RemoveFromCart(Guid productId, Guid productTypeId);
    }
}
