using shop.Application.ViewModels.ResponseDTOs;
using shop.Domain.Entities.Enum;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shop.Application.Common;
using AppBusiness.Model.Pagination;

namespace shop.Application.Interfaces
{
    public interface IOrderService
    {
        public Task<ApiResponse<bool>> PlaceOrder();
        public Task<ApiResponse<Pagination<List<Order>>>> GetAdminOrders(int page);
        public Task<ApiResponse<Pagination<List<Order>>>> GetCustomerOrders(int page);
        public Task<ApiResponse<List<OrderItemDto>>> GetOrderItems(Guid orderId);
        public Task<ApiResponse<OrderDetailCustomerDto>> GetOrderCustomerInfo(Guid orderId);
        public Task<ApiResponse<bool>> UpdateOrderState(Guid orderId, OrderState state);
        public Task<ApiResponse<int>> GetOrderState(Guid orderId);
    }
}
