using shop.Application.Common;
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
        Task<ApiResponse<bool>> CreateOrderCounter(Guid? voucherId, CreateOrderCounterDto newOrder);
        Task<ApiResponse<List<SearchAddressItemResponse>>> SearchAddressItems(string searchText);
        Task<ApiResponse<List<SearchProductItemResponse>>> SearchProducts(string searchText);
        Task<ApiResponse<List<PaymentMethod>>> GetPaymentMethodSelect();
        Task<ApiResponse<CustomerVoucherResponseDto>> ApplyVoucher(string discountCode, int totalAmount);
    }
}
