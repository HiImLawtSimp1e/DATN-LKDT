using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs.OrderCounterDto;
using shop.Application.ViewModels.ResponseDTOs.OrderCounterDto;
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
    }
}
