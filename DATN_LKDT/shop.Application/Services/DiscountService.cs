using AppBusiness.Model.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.DiscountDto;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public DiscountService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }
        public async Task<ApiResponse<Pagination<List<DiscountEntity>>>> GetVouchers(int page, double pageResults)
        {
            var pageCount = Math.Ceiling(_context.Discounts.Count() / pageResults);

            var vouchers = await _context.Discounts
                   .OrderByDescending(p => p.ModifiedAt)
                   .Skip((page - 1) * (int)pageResults)
                   .Take((int)pageResults)
                   .ToListAsync();

            var pagingData = new Pagination<List<DiscountEntity>>
            {
                Result = vouchers,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<DiscountEntity>>>
            {
                Data = pagingData
            };
        }
        public async Task<ApiResponse<DiscountEntity>> GetVoucher(Guid voucherId)
        {
            var voucher = await _context.Discounts
                                          .FirstOrDefaultAsync(v => v.Id == voucherId);
            if (voucher == null)
            {
                return new ApiResponse<DiscountEntity>
                {
                    Success = false,
                    Message = "Không tìm thấy voucher giảm giá"
                };
            }

            return new ApiResponse<DiscountEntity>
            {
                Data = voucher
            };
        }

        public async Task<ApiResponse<bool>> CreateVoucher(AddDiscountDto newVoucher)
        {
            if (CheckDiscountCodeExisting(newVoucher.Code) == true)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Tên mã giảm giá đã tồn tại, vui lòng chọn tên khác"
                };
            }

            //Giá trị giảm giá tối đa chỉ dành cho voucher giảm giá theo phần trăm
            if (!newVoucher.IsDiscountPercent && newVoucher.DiscountValue != 0)
            {
                newVoucher.MaxDiscountValue = 0;
            }

            var username = _authService.GetUserName();

            var voucher = _mapper.Map<DiscountEntity>(newVoucher);
            voucher.CreatedBy = username;

            _context.Discounts.Add(voucher);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true
            };
        }

        public async Task<ApiResponse<bool>> UpdateVoucher(Guid voucherId, UpdateDiscountDto updateVoucher)
        {
            var dbVoucher = await _context.Discounts.FirstOrDefaultAsync(v => v.Id == voucherId);

            if (dbVoucher == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy voucher giảm giá"
                };
            }

            //Giá trị giảm giá tối đa chỉ dành cho voucher giảm giá theo phần trăm
            if (!updateVoucher.IsDiscountPercent && updateVoucher.MaxDiscountValue != 0)
            {
                updateVoucher.MaxDiscountValue = 0;
            }

            var username = _authService.GetUserName();

            _mapper.Map(updateVoucher, dbVoucher);
            dbVoucher.ModifiedAt = DateTime.Now;
            dbVoucher.ModifiedBy = username;

            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Data = true
            };
        }

        public async Task<ApiResponse<bool>> DeleteVoucher(Guid voucherId)
        {
            var voucher = await _context.Discounts.FirstOrDefaultAsync(v => v.Id == voucherId);

            if (voucher == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy voucher"
                };
            }

            var existingOrdersVouchers = await _context.Orders
                                                  .Where(o => o.DiscountId == voucherId)
                                                  .ToListAsync();

            if (existingOrdersVouchers.Any())
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không thể xóa vì đã có đơn hàng sử dụng voucher này"
                };
            }

            _context.Discounts.Remove(voucher);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Đã xóa voucher"
            };
        }

        public async Task<ApiResponse<Pagination<List<DiscountEntity>>>> SearchVouchers(string searchText, int page, double pageResults)
        {
            var pageCount = Math.Ceiling((await FindAdminVouchersBySearchText(searchText)).Count / pageResults);

            var vouchers = await _context.Discounts
                                .Where(v => v.Code.ToLower().Contains(searchText.ToLower())
                                 || v.VoucherName.ToLower().Contains(searchText.ToLower()))
                                .OrderByDescending(p => p.ModifiedAt)
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();

            if (vouchers == null)
            {
                return new ApiResponse<Pagination<List<DiscountEntity>>>
                {
                    Success = false,
                    Message = "Không tìm thấy voucher"
                };
            }

            var pagingData = new Pagination<List<DiscountEntity>>
            {
                Result = vouchers,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<DiscountEntity>>>
            {
                Data = pagingData,
            };
        }

        private bool CheckDiscountCodeExisting(string discountCode)
        {
            var existingDiscountCode = _context.Discounts
                                             .FirstOrDefault(v => v.Code.ToLower() == discountCode.ToLower());
            return existingDiscountCode != null;
        }

        private async Task<List<DiscountEntity>> FindAdminVouchersBySearchText(string searchText)
        {
            return await _context.Discounts
                                .Where(v => v.Code.ToLower().Contains(searchText.ToLower())
                                    || v.VoucherName.ToLower().Contains(searchText.ToLower()))
                                .ToListAsync();
        }
    }
}
