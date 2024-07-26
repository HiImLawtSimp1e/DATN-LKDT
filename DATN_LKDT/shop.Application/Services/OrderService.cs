using AppBusiness.Model.Pagination;
using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.ResponseDTOs;
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
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly ICartService _cartService;

        public OrderService(AppDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }
        public async Task<ApiResponse<Pagination<List<Order>>>> GetAdminOrders(int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling(_context.Orders.Count() / pageResults);

            var orders = await _context.Orders
                                   .OrderByDescending(p => p.ModifiedAt)
                                   .Skip((page - 1) * (int)pageResults)
                                   .Take((int)pageResults)
                                   .ToListAsync();

            var pagingData = new Pagination<List<Order>>
            {
                Result = orders,
                CurrentPage = page,
                PageResults = (int)pageResults,
                Pages = (int)pageCount
            };

            return new ApiResponse<Pagination<List<Order>>>
            {
                Data = pagingData
            };
        }
        public async Task<ApiResponse<List<OrderItemDto>>> GetAdminOrderItems(Guid orderId)
        {
            var items = await _context.OrderItems
                                    .Where(oi => oi.OrderId == orderId)
                                    .ToListAsync();

            var result = new ApiResponse<List<OrderItemDto>>
            {
                Data = new List<OrderItemDto>()
            };

            foreach (var item in items)
            {
                var product = await _context.Products
                    .Where(p => p.Id == item.ProductId)
                    .FirstOrDefaultAsync();

                if (product == null)
                {
                    continue;
                } 

                var productVariant = await _context.ProductVariants
                    .Where(v => v.ProductId == item.ProductId
                        && v.ProductTypeId == item.ProductTypeId)
                    .Include(v => v.ProductType)
                    .FirstOrDefaultAsync();

                if (productVariant == null)
                {
                    continue;
                }

                var cartProduct = new OrderItemDto
                {
                    ProductId = product.Id,
                    ProductTitle = product.Title,
                    ImageUrl = product.ImageUrl,
                    ProductTypeId = productVariant.ProductTypeId,
                    Price = productVariant.Price,
                    ProductTypeName = productVariant.ProductType.Name,
                    Quantity = item.Quantity
                };

                result.Data.Add(cartProduct);
            }

            return result;
        }
        public async Task<ApiResponse<OrderDetailCustomerDto>> GetAdminOrderCustomerInfo(Guid orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return new ApiResponse<OrderDetailCustomerDto>
                {
                    Success = false,
                    Message = "Không tìm thấy đơn hàng"
                };
            }

            var customer = await _context.Accounts.FirstOrDefaultAsync(c => c.Id == order.AccountId);

            if (customer == null)
            {
                return new ApiResponse<OrderDetailCustomerDto>
                {
                    Success = false,
                    Message = "Không tìm thấy thông tin người dùng"
                };
            }

            var address = await _context.Address.FirstOrDefaultAsync(a => a.IdAccount == customer.Id);

            var orderCustomerInfo = new OrderDetailCustomerDto
            {
                Id = customer.Id,
                FullName = customer.Name,
                Email = customer.Email,
                Address = GetCustomerAddress(address),
                Phone = customer.PhoneNumber,
                InvoiceCode = order.InvoiceCode,
                OrderCreatedAt = order.CreatedAt
            };

            return new ApiResponse<OrderDetailCustomerDto>
            {
                Data = orderCustomerInfo
            };
        }

        public async Task<ApiResponse<bool>> UpdateOrderState(Guid orderId, OrderState state)
        {
            if (!Enum.IsDefined(typeof(OrderState), state))
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Invalid state"
                };
            }

            var dbOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (dbOrder == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy đơn hàng"
                };
            }
            dbOrder.State = state;
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Cập nhật thành công trạng thái đơn hàng"
            };
        }
        public async Task<ApiResponse<int>> GetOrderState(Guid orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return new ApiResponse<int>
                {
                    Success = false,
                    Message = "Not found order"
                };
            }
            return new ApiResponse<int>
            {
                Data = (int)order.State
            };
        }

        public async Task<ApiResponse<bool>> PlaceOrder(Guid accountId)
        {
            var customer = await _context.Accounts
                                       .Include(c => c.Cart)
                                       .FirstOrDefaultAsync(c => c.Id == accountId);

            if (customer == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Bạn chưa đăng nhập"
                };
            }

            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.AccountId == accountId);

            var cartItem = (await _cartService.GetCartItems(accountId)).Data;

            if (cart == null || cartItem == null || cartItem.Count == 0)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Giỏ hàng trống"
                };
            }

            int totalAmount = 0;

            cartItem.ForEach(ci => totalAmount += ci.Price * ci.Quantity);

            var orderItems = new List<OrderItem>();

            cartItem.ForEach(ci =>
            {
                var item = new OrderItem
                {
                    ProductId = ci.ProductId,
                    ProductTypeId = ci.ProductTypeId,
                    Quantity = ci.Quantity,
                    TotalPrice = ci.Quantity * ci.Price,
                };
                orderItems.Add(item);
            });

            var order = new Order
            {
                AccountId = customer.Id,
                InvoiceCode = GenerateInvoiceCode(),
                TotalPrice = totalAmount,
                OrderItems = orderItems
            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(_context.CartItems.Where(ci => ci.CartId == cart.Id)); 

            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Đặt hàng thành công"
            };
        }

        private string GetCustomerAddress(AddressEntity address)
        {
            var result = "";
            if(address != null)
            {
                result = $"{address.HomeAddress}, {address.District}, {address.City}, {address.Country}";
            }
            return result;
        }

        private string GenerateInvoiceCode()
        {
            return $"INV-{DateTime.Now:yyyyMMddHHmmssfff}-{new Random().Next(1000, 9999)}";
        }
    }
}
