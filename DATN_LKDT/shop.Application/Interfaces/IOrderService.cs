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
using shop.Application.ViewModels.ResponseDTOs.CustomerResponseDto;

namespace shop.Application.Interfaces
{
    public interface IOrderService
    {
        public Task<ApiResponse<bool>> PlaceOrder(Guid? voucherId);
        public Task<ApiResponse<CustomerVoucherResponseDto>> ApplyVoucher(string discountCode);
        public Task<ApiResponse<bool>> CancelOrder(Guid orderId);
        public Task<ApiResponse<Pagination<List<Order>>>> GetAdminOrders(int page);
        public Task<ApiResponse<Pagination<List<Order>>>> GetCustomerOrders(int page);
        public Task<ApiResponse<List<OrderItemDto>>> GetOrderItems(Guid orderId);
        public Task<ApiResponse<OrderDetailCustomerDto>> GetOrderDetailInfo(Guid orderId);
        public Task<ApiResponse<bool>> UpdateOrderState(Guid orderId, OrderState state);
        public Task<ApiResponse<int>> GetOrderState(Guid orderId);
    }
}
