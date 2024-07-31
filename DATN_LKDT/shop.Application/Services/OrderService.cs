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
        private readonly IAuthService _authService;

        public OrderService(AppDbContext context, ICartService cartService, IAuthService authService)
        {
            _context = context;
            _cartService = cartService;
            _authService = authService;
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

        public async Task<ApiResponse<Pagination<List<Order>>>> GetCustomerOrders(int page)
        {
            var accountId = _authService.GetUserId();

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);

            if (account == null)
            {
                return new ApiResponse<Pagination<List<Order>>>
                {
                    Success = false,
                    Message = "You need to log in"
                };
            }

            var pageResults = 10f;
            var pageCount = Math.Ceiling(_context.Orders.Count() / pageResults);

            var orders = await _context.Orders
                                   .Where(o => o.AccountId == account.Id)
                                   .OrderByDescending(o => o.CreatedAt)
                                   .Skip((page - 1) * (int)pageResults)
                                   .Take((int)pageResults)
                                   .ToListAsync();

            var pagingData = new Pagination<List<Order>>
            {
                Result = orders,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return new ApiResponse<Pagination<List<Order>>>
            {
                Data = pagingData
            };
        }

        public async Task<ApiResponse<List<OrderItemDto>>> GetOrderItems(Guid orderId)
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

                var cartProduct = new OrderItemDto
                {
                    ProductId = item.ProductId,
                    ProductTitle = item.ProductTitle,
                    ProductTypeId = item.ProductTypeId,
                    ProductTypeName = item.ProductTypeName,
                    Price = item.Price,
                    OriginalPrice = item.OriginalPrice,
                    Quantity = item.Quantity,
                    ImageUrl = product.ImageUrl
                };

                result.Data.Add(cartProduct);
            }

            return result;
        }
        public async Task<ApiResponse<OrderDetailCustomerDto>> GetOrderCustomerInfo(Guid orderId)
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

            var orderCustomerInfo = new OrderDetailCustomerDto
            {
                Id = customer.Id,
                FullName = order.FullName,
                Email = order.Email,
                Address = order.Address,
                Phone = order.Phone,
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

        public async Task<ApiResponse<bool>> PlaceOrder()
        {
            var accountId = _authService.GetUserId();

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

            var cartItem = (await _cartService.GetCartItems()).Data;

            if (cart == null || cartItem == null || cartItem.Count() == 0)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Giỏ hàng trống"
                };
            }

            var address = await _context.Address.FirstOrDefaultAsync(a => a.IdAccount == customer.Id);

            int totalAmount = 0;

            cartItem.ForEach(ci => totalAmount += ci.Price * ci.Quantity);

            var orderItems = new List<OrderItem>();

            cartItem.ForEach(ci =>
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == ci.ProductId);
                var variant = _context.ProductVariants
                                    .Include(v => v.ProductType)
                                    .FirstOrDefault(v => v.ProductId == ci.ProductId && v.ProductTypeId == ci.ProductTypeId);

                var item = new OrderItem
                {
                    ProductId = ci.ProductId,
                    ProductTypeId = ci.ProductTypeId,
                    Quantity = ci.Quantity,
                    Price = ci.Price,
                    OriginalPrice = variant.OriginalPrice,
                    ProductTypeName = variant.ProductType.Name,
                    ProductTitle = product.Title
                };
                orderItems.Add(item);
            });

            var order = new Order
            {
                AccountId = customer.Id,
                InvoiceCode = GenerateInvoiceCode(),
                TotalPrice = totalAmount,
                OrderItems = orderItems,
                FullName = customer.Name,
                Email = customer.Email,
                Address = GetCustomerAddress(address),
                Phone = customer.PhoneNumber,
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
                result = $"{address.HomeAddress}, {address.District}, {address.City} ";
            }
            return result;
        }

        private string GenerateInvoiceCode()
        {
            return $"INV-{DateTime.Now:yyyyMMddHHmmssfff}-{new Random().Next(1000, 9999)}";
        }
    }
}
