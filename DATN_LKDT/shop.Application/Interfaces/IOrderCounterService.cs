using AppBusiness.Model.Pagination;
using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs;
using shop.Application.ViewModels.RequestDTOs.OrderCounterDto;
using shop.Application.ViewModels.ResponseDTOs.CustomerResponseDto;
using shop.Application.ViewModels.ResponseDTOs.OrderCounterDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IOrderCounterService
    {
        #region OrderCounterService
        Task<ApiResponse<bool>> CreateOrderCounter(Guid? voucherId, CreateOrderCounterDto newOrder);
        Task<ApiResponse<List<SearchAddressItemResponse>>> SearchAddressItems(string searchText);
        Task<ApiResponse<List<SearchProductItemResponse>>> SearchProducts(string searchText);
        Task<ApiResponse<List<PaymentMethod>>> GetPaymentMethodSelect();
        Task<ApiResponse<CustomerVoucherResponseDto>> ApplyVoucher(string discountCode, int totalAmount);
        #endregion OrderCounterService

        #region ProvisionalOrderService
        Task<ApiResponse<Pagination<List<Order>>>> GetAdminProvisionalOrders(int page, double pageResults);
        public Task<ApiResponse<Pagination<List<Order>>>> SearchAdminProvisionalOrders(string searchText, int page, double pageResults);
        Task<ApiResponse<bool>> SaveProvisionalInvoice(SaveOrderCounterDto saveOrder);
        Task<ApiResponse<bool>> AddToCart(Guid orderId, OrderCounterItemDto newItem);
        Task<ApiResponse<bool>> UpdateQuantity(Guid orderId, OrderCounterItemDto updateItem);
        Task<ApiResponse<bool>> RemoveFromCart(Guid orderId, Guid productId, Guid productTypeId);
        #endregion ProvisionalOrderService
    }
}
