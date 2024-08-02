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

        public DiscountService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                newVoucher.DiscountValue = 0;
            }

            var voucher = _mapper.Map<DiscountEntity>(newVoucher);

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
                    Message = "Không tìm thấy mã giảm giá"
                };
            }


            //Giá trị giảm giá tối đa chỉ dành cho voucher giảm giá theo phần trăm
            if (!updateVoucher.IsDiscountPercent && updateVoucher.MaxDiscountValue != 0)
            {
                updateVoucher.MaxDiscountValue = 0;
            }

            _mapper.Map(updateVoucher, dbVoucher);

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

        private bool CheckDiscountCodeExisting(string discountCode)
        {
            var existingDiscountCode = _context.Discounts
                                             .FirstOrDefault(v => v.Code.ToLower() == discountCode.ToLower());
            return existingDiscountCode != null;
        }
    }
}
