using AppBusiness.Model.Pagination;
using shop.Application.Common;
using shop.Application.ViewModels.RequestDTOs.DiscountDto;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IDiscountService
    {
        public Task<ApiResponse<Pagination<List<DiscountEntity>>>> GetVouchers(int page, double pageResults);
        public Task<ApiResponse<DiscountEntity>> GetVoucher(Guid voucherId);
        public Task<ApiResponse<bool>> CreateVoucher(AddDiscountDto newVoucher);
        public Task<ApiResponse<bool>> UpdateVoucher(Guid voucherId, UpdateDiscountDto updateVoucher);
        public Task<ApiResponse<bool>> DeleteVoucher(Guid voucherId);
    }
}
