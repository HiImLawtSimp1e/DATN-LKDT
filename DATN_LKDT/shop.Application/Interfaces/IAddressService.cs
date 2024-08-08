using AppBusiness.Model.Pagination;
using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs.AddressDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IAddressService
    {
        Task<ApiResponse<Pagination<List<AddressEntity>>>> GetAddresses(int page);
        Task<ApiResponse<AddressEntity>> GetSingleAddress(Guid addressId);
        Task<ApiResponse<AddressEntity>> GetMainAddress();
        Task<ApiResponse<bool>> CreateAddress(CreateAddressDto newAddress);
        Task<ApiResponse<bool>> UpdateAddress(Guid addressId, UpdateAddressDto updateAddress);
        Task<ApiResponse<bool>> DeleteAddress(Guid addressId);
    }
}
