using AppBusiness.Model.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.AddressDto;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AddressService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }
        public async Task<ApiResponse<Pagination<List<AddressEntity>>>> GetAddresses(int page)
        {
            var accountId = _authService.GetUserId();

            var pageResults = 6f;
            var pageCount = Math.Ceiling(_context.Address.Where(a => a.AccountId == accountId).Count() / pageResults);

            var addresses = await _context.Address
                                      .Where(a => a.AccountId == accountId)
                                      .OrderByDescending(a => a.IsMain)
                                      .Skip((page - 1) * (int)pageResults)
                                      .Take((int)pageResults)
                                      .ToListAsync();

            var pagingData = new Pagination<List<AddressEntity>>
            {
                Result = addresses,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<AddressEntity>>>
            {
                Data = pagingData
            };
        }

        public async Task<ApiResponse<AddressEntity>> GetSingleAddress(Guid addressId)
        {
            var accountId = _authService.GetUserId();

            var address = await _context.Address
                                       .Where(a => a.AccountId == accountId)
                                       .FirstOrDefaultAsync(a => a.Id == addressId);

            if (address == null)
            {
                return new ApiResponse<AddressEntity>
                {
                    Success = false,
                    Message = "Không tìm thấy địa chỉ"
                };
            }

            return new ApiResponse<AddressEntity>
            {
                Data = address
            };
        }

        public async Task<ApiResponse<AddressEntity>> GetMainAddress()
        {
            var accountId = _authService.GetUserId();

            var address = await _context.Address
                                       .Where(a => a.IsMain)
                                       .FirstOrDefaultAsync(a => a.AccountId == accountId);

            if (address == null)
            {
                return new ApiResponse<AddressEntity>
                {
                    Success = false,
                    Message = "Không tìm thấy địa chỉ"
                };
            }

            return new ApiResponse<AddressEntity>
            {
                Data = address
            };
        }

        public async Task<ApiResponse<bool>> CreateAddress(CreateAddressDto newAddress)
        {
            var accountId = _authService.GetUserId();

            var address = _mapper.Map<AddressEntity>(newAddress);
            address.AccountId = accountId;

            //Kiểm tra xem đây có phải địa chỉ chính hay không
            //Nếu là địa chỉ chính => Sửa địa chỉ đã tồn tại trong db thành không hoạt động
            if (address.IsMain == true)
            {
                var mainAddress = _context.Address
                                     .Where(a => a.AccountId == accountId)
                                     .FirstOrDefault(pi => pi.IsMain);

                //if it has already main address in database => set that addrees is not main
                if (mainAddress != null)
                {
                    mainAddress.IsMain = false;
                }
            }

            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true
            };
        }

        public async Task<ApiResponse<bool>> UpdateAddress(Guid addressId, UpdateAddressDto updateAddress)
        {
            var accountId = _authService.GetUserId();

            var dbAddress = await _context.Address
                                       .FirstOrDefaultAsync(a => a.Id == addressId);

            if (dbAddress == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy địa chỉ"
                };
            }

            //Kiểm tra xem đây địa chỉ mới có isMain hay không
            //Nếu là địa chỉ mới isMain => Sửa địa chỉ chính đã tồn tại trong db thành không hoạt động
            if (updateAddress.IsMain == true)
            {
                var mainAddress = _context.Address
                                     .Where(a => a.AccountId == accountId)
                                     .FirstOrDefault(pi => pi.IsMain);

                //if it has already main address in database => set that addrees is not main
                if (mainAddress != null)
                {
                    mainAddress.IsMain = false;
                }
            }

            //Kiểm tra nếu ngừng hoạt động địa chỉ isMain
            if(updateAddress.IsMain == false && dbAddress.IsMain == true)
            {
                //Timf địa chỉ khác của tài khoản
                var somethingElseAddress = await _context.Address
                                             .Where(a => a.Id != addressId)
                                             .FirstOrDefaultAsync(a => a.AccountId == accountId);

                //Nếu không có địa chỉ nào khác
                if (somethingElseAddress == null)
                {
                    return new ApiResponse<bool>
                    {
                        Success = false,
                        Message = "Không thể ngừng hoạt động địa chỉ duy nhất"
                    };
                }
                //Nếu có, chọn địa chỉ khác bất kỳ của tài khoản làm isMain
                else
                {
                    somethingElseAddress.IsMain = true;
                }

            }

            _mapper.Map(updateAddress, dbAddress);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true
            };
        }

        public async Task<ApiResponse<bool>> DeleteAddress(Guid addressId)
        {
            var address = await _context.Address
                                      .FirstOrDefaultAsync(a => a.Id == addressId);

            if (address == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy địa chỉ"
                };
            }

            if (address.IsMain)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không thể xóa địa chỉ đang sử dụng"
                };
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true
            };
        }
    }
}
