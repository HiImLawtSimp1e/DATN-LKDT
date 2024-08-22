using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.OrderCounterDto;
using shop.Application.ViewModels.ResponseDTOs.OrderCounterDto;
using shop.Domain.Entities;
using shop.Domain.Entities.Enum;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Services
{
    public class OrderCounterService : IOrderCounterService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public OrderCounterService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }
        public async Task<ApiResponse<bool>> CreateOrderCounter(Guid? voucherId, CreateOrderCounterDto newOrder)
        {
            var username = _authService.GetUserName();

            var newOrderItems = newOrder.OrderItems;

            if(newOrderItems == null || newOrderItems.Count() == 0)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Chưa có sản phẩm"
                };
            }

            newOrderItems.ForEach(oi =>
            {
                var variant = _context.ProductVariants
                                    .Include(v => v.ProductType)
                                    .FirstOrDefault(v => v.ProductId == oi.ProductId && v.ProductTypeId == oi.ProductTypeId);

                variant.Quantity -= oi.Quantity;
            });

            int totalAmount = 0;

            newOrderItems.ForEach(item => totalAmount += item.Price * item.Quantity);

            var orderItems = _mapper.Map<List<OrderItem>>(newOrderItems);

            var order = new Order
            {
                InvoiceCode = GenerateInvoiceCode(),
                FullName = newOrder.Name,
                Email = newOrder.Email,
                Phone = newOrder.PhoneNumber,
                Address = newOrder.Address,
                OrderItems = orderItems,
                TotalPrice = totalAmount,
                State = OrderState.Delivered,
                CreatedBy = username
            };

            if (voucherId != null)
            {
                //Kiểm tra xem voucher còn hoạt động, đã hết hạn hoặc hết số lượng hay chưa
                var voucher = await _context.Discounts
                                         .Where(v => v.IsActive == true && DateTime.Now > v.StartDate && DateTime.Now < v.EndDate && v.Quantity > 0)
                                         .FirstOrDefaultAsync(v => v.Id == voucherId);
                if (voucher != null)
                {
                    // MinOrderCondition = 0 nghĩa là voucher không có giá trị giảm giá tối đa => pass
                    // "Giá trị đơn hàng" lớn hơn "giá trị giảm giá tối đa" => pass

                    if (voucher.MinOrderCondition <= 0 || totalAmount > voucher.MinOrderCondition)
                    {
                        var discountValue = CalculateDiscountValue(voucher, totalAmount);
                        order.DiscountValue = discountValue;
                        order.DiscountId = voucher.Id;
                        voucher.Quantity -= 1;
                    }
                }
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool> 
            {
                Message = "Tạo đơn hàng thành công"
            };
        }

        public async Task<ApiResponse<List<SearchAddressItemResponse>>> SearchAddressItems(string searchText)
        {
            var addresses = await _context.Address
                                       .Where(a => a.Email.ToLower().Contains(searchText.ToLower())
                                       || a.PhoneNumber.ToLower().Contains(searchText.ToLower()))
                                       .Take(10)
                                       .ToListAsync();

            if(addresses == null)
            {
                return new ApiResponse<List<SearchAddressItemResponse>>();
            }

            var result = _mapper.Map<List<SearchAddressItemResponse>>(addresses);

            return new ApiResponse<List<SearchAddressItemResponse>>
            {
                Data = result
            };
        }

        public async Task<ApiResponse<List<SearchProductItemResponse>>> SearchProducts(string searchText)
        {
            var products = await _context.Products
              .Where(p => p.Title.ToLower().Contains(searchText.ToLower())
              && p.IsActive && !p.Deleted)
              .Include(p => p.ProductVariants.Where(pv => !pv.Deleted))
              .ThenInclude(pv => pv.ProductType)
            .Take(10)
              .ToListAsync();

            if (products == null)
            {
                return new ApiResponse<List<SearchProductItemResponse>>();
            }

            var result = new List<SearchProductItemResponse>();

            products.ForEach(product =>
            {
                product.ProductVariants.ForEach(variant =>
                {
                    var item = new SearchProductItemResponse
                    {
                        ProductId = variant.ProductId,
                        ProductTypeId = variant.ProductTypeId,
                        ImageUrl = product.ImageUrl, 
                        ProductTitle = product.Title,
                        ProductTypeName = variant.ProductType.Name,
                        Price = variant.Price,
                        OriginalPrice = variant.OriginalPrice,
                        Quantity = 1 
                    };
                    result.Add(item);
                });
            });

            return new ApiResponse<List<SearchProductItemResponse>>
            {
                Data = result
            };
        }

        private string GenerateInvoiceCode()
        {
            return $"INV-{DateTime.Now:yyyyMMddHHmmssfff}-{new Random().Next(1000, 9999)}";
        }

        private int CalculateDiscountValue(DiscountEntity voucher, int totalAmount)
        {
            int result = 0;
            if (voucher.IsDiscountPercent)
            {
                var discountValue = (int)(totalAmount * (voucher.DiscountValue / 100));

                // MaxDiscountValue = 0 meaning max discount value doesn't exist
                if (voucher.MaxDiscountValue > 0)
                {
                    result = (discountValue > voucher.MaxDiscountValue) ? voucher.MaxDiscountValue : discountValue;
                }
                else
                {
                    result = discountValue;
                }
            }
            else
            {
                result = (int)voucher.DiscountValue;
            }
            return result;
        }
    }
}
