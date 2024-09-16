using AppBusiness.Model.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.ResponseDTOs;
using shop.Application.ViewModels.ResponseDTOs.CustomerResponseDto;
using shop.Domain.Entities;
using shop.Domain.Entities.Enum;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace shop.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;

        public OrderService(AppDbContext context, IMapper mapper, ICartService cartService, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _cartService = cartService;
            _authService = authService;
        }

        #region GetOrderInformationService

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
            var pageCount = Math.Ceiling(_context.Orders.Where(o => o.AccountId == account.Id).Count() / pageResults);

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
                Pages = (int)pageCount,
                PageResults = (int)pageResults
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
        public async Task<ApiResponse<OrderDetailCustomerDto>> GetOrderDetailInfo(Guid orderId)
        {
            var order = await _context.Orders
                                .Include(o => o.PaymentMethod)
                                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return new ApiResponse<OrderDetailCustomerDto>
                {
                    Success = false,
                    Message = "Không tìm thấy đơn hàng"
                };
            }

            var orderCustomerInfo = new OrderDetailCustomerDto
            {
                Id = order.Id,
                FullName = order.FullName,
                Email = order.Email,
                Address = order.Address,
                Phone = order.Phone,
                InvoiceCode = order.InvoiceCode,
                PaymentMethodName = order.PaymentMethod.Name,
                DiscountValue = order.DiscountValue,
                OrderCreatedAt = order.CreatedAt,
                State = order.State
            };

            return new ApiResponse<OrderDetailCustomerDto>
            {
                Data = orderCustomerInfo
            };
        }

        #endregion GetOrderInformationService

        #region AdminManagerOrderService

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

            if (dbOrder.State == OrderState.Cancelled)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không thể cập nhật đơn hàng đã hủy"
                };
            }

            if(dbOrder.State == OrderState.Delivered)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không thể cập nhật đơn hàng đã giao thành công"
                };
            }

            // Trả số lượng sản phẩm về như cũ khi hủy đơn hàng
            if (state == OrderState.Cancelled)
            {
                await TurnBackVariantQuantity(orderId);
            }

            var username = _authService.GetUserName();

            dbOrder.State = state;
            dbOrder.ModifiedAt = DateTime.Now;
            dbOrder.ModifiedBy = username;

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
        public async Task<ApiResponse<Pagination<List<Order>>>> SearchAdminOrders(string searchText, int page, double pageResults)
        {
            var pageCount = Math.Ceiling((await FindAdminOrdersBySearchText(searchText)).Count / pageResults);

            var orders = await _context.Orders
                .Where(p => p.InvoiceCode.ToLower().Contains(searchText.ToLower()))
                .OrderByDescending(p => p.ModifiedAt)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            if (orders == null)
            {
                return new ApiResponse<Pagination<List<Order>>>
                {
                    Success = false,
                    Message = "Không tìm thấy hóa đơn"
                };
            }

            var pagingData = new Pagination<List<Order>>
            {
                Result = orders,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<Order>>>
            {
                Data = pagingData,
            };
        }
        public async Task<ApiResponse<Pagination<List<Order>>>> FilterAdminOrdersByState(OrderState orderState, int page, double pageResults)
        {
            var pageCount = Math.Ceiling((await FindAdminOrdersByState(orderState)).Count / pageResults);

            var orders = await _context.Orders
                                   .Where(o => o.State == orderState)
                                   .OrderByDescending(p => p.ModifiedAt)
                                   .Skip((page - 1) * (int)pageResults)
                                   .Take((int)pageResults)
                                   .ToListAsync();
            if(orders == null)
            {
                return new ApiResponse<Pagination<List<Order>>>
                {
                    Success = false,
                    Message = "Không tìm thấy hóa đơn"
                };
            }

            var pagingData = new Pagination<List<Order>>
            {
                Result = orders,
                CurrentPage = page,
                Pages = (int)pageCount,
                PageResults = (int)pageResults
            };

            return new ApiResponse<Pagination<List<Order>>>
            {
                Data = pagingData,
            };
        }

        #endregion AdminManagerOrderService

        #region CustomerOrderService

        public async Task<ApiResponse<bool>> PlaceOrder(Guid? voucherId)
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

            var createOrder = await CreateOrder(voucherId, customer, "Thanh toán khi nhận hàng (COD)");

            if (!createOrder.Success)
            {
                return createOrder;
            }

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Đặt hàng thành công"
            };
        }

        public async Task<ApiResponse<bool>> CreateOrder(Guid? voucherId, AccountEntity customer, string pmOrder)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.AccountId == customer.Id);

            var cartItem = (await _cartService.GetCartItemsByAccountId(customer.Id)).Data;

            if (cart == null || cartItem == null || cartItem.Count() == 0)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Giỏ hàng trống"
                };
            }

            var address = await _context.Address
                                      .Where(a => a.IsMain)
                                      .FirstOrDefaultAsync(a => a.AccountId == customer.Id);

            if (address == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy địa chỉ khách hàng"
                };
            }

            var paymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync(pm => pm.Name == pmOrder);
            if (paymentMethod == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy phương thức thanh toán"
                };
            }

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
                variant.Quantity -= ci.Quantity; //Trừ số lượng sản phẩm khi khách đặt hàng
                orderItems.Add(item);
            });

            var order = new Order
            {
                AccountId = customer.Id,
                InvoiceCode = GenerateInvoiceCode(),
                TotalPrice = totalAmount,
                OrderItems = orderItems,
                FullName = address.Name,
                Email = address.Email,
                Address = address.Address,
                Phone = address.PhoneNumber,
                CreatedBy = "Khách hàng",
                PaymentMethod = paymentMethod,
                IsCounterOrder = false
            };

            var discountValue = 0;

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
                        discountValue = CaculateDiscountValue(voucher, totalAmount);
                        order.DiscountValue = discountValue;
                        order.DiscountId = voucher.Id;
                        voucher.Quantity -= 1;
                    }
                }
            }

            order.TotalAmount = order.TotalPrice - discountValue;

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(_context.CartItems.Where(ci => ci.CartId == cart.Id));

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true
            };
        }

        public async Task<ApiResponse<CustomerVoucherResponseDto>> ApplyVoucher(string discountCode)
        {
            var totalAmount = await _cartService.GetCartTotalAmountAsync();
            if (totalAmount == 0)
            {
                return new ApiResponse<CustomerVoucherResponseDto>
                {
                    Success = false,
                    Message = "Giỏ hàng trống"
                };
            }

            //Kiểm tra xem voucher còn hoạt động, đã hết hạn hoặc hết số lượng hay chưa
            var voucher = await _context.Discounts
                                           .Where(v => v.IsActive == true && DateTime.Now > v.StartDate && DateTime.Now < v.EndDate && v.Quantity > 0)
                                           .FirstOrDefaultAsync(v => v.Code == discountCode);
            if (voucher == null)
            {
                return new ApiResponse<CustomerVoucherResponseDto>
                {
                    Success = false,
                    Message = "Mã voucher không đúng hoặc đã hết hạn sử dụng"
                };
            }
            else
            {

                // MinOrderCondition = 0 nghĩa là voucher không có giá trị giảm giá tối đa => pass
                // "Giá trị đơn hàng" lớn hơn "giá trị giảm giá tối đa" => pass
                if (voucher.MinOrderCondition > 0 && totalAmount < voucher.MinOrderCondition)
                {
                    return new ApiResponse<CustomerVoucherResponseDto>
                    {
                        Success = false,
                        Message = string.Format("Voucher chỉ áp dụng cho đơn hàng có giá trị từ {0}đ", voucher.MinOrderCondition)
                    };
                }

                var result = _mapper.Map<CustomerVoucherResponseDto>(voucher);

                return new ApiResponse<CustomerVoucherResponseDto>
                {
                    Data = result
                };
            }
        }

        public async Task<ApiResponse<bool>> CancelOrder(Guid orderId)
        {
            var order = await _context.Orders
                                   .Where(o => o.State != OrderState.Cancelled)
                                   .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy đơn hàng"
                };
            }

            if (order.State == OrderState.Delivered)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Bạn không thể hủy đơn hàng này"
                };
            }

            order.State = OrderState.Cancelled;
            order.ModifiedAt = DateTime.Now;
            order.ModifiedBy = "Khách hàng";

            await TurnBackVariantQuantity(orderId); // Trả lại số lượng sản phẩm khi hủy đơn hàng

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Message = "Hủy đơn hàng thành công"
            };
        }

        #endregion CustomerOrderService

        #region PrivateService

        private string GenerateInvoiceCode()
        {
            return $"INV-{DateTime.Now:yyyyMMddHHmmssfff}-{new Random().Next(1000, 9999)}";
        }

        //Logical: Each account is allowed to use one voucher code only once
        private bool IsVoucherUsed(Guid voucherId)
        {
            var accountId = _authService.GetUserId();
            var customer = _context.Accounts.FirstOrDefault(c => c.Id == accountId);
            if (customer == null)
            {
                return true;
            }
            var isUsedVoucher = _context.Orders.FirstOrDefault(o => o.AccountId == customer.Id && o.DiscountId == voucherId);
            return isUsedVoucher != null;
        }

        //Caculate Discount Value
        private int CaculateDiscountValue(DiscountEntity voucher, int totalAmount)
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

        //Turn back product variant quantity after cancel order
        private async Task<bool> TurnBackVariantQuantity(Guid orderId)
        {
            var orderItems = await _context.OrderItems
                                      .Where(o => o.OrderId == orderId)
                                      .ToListAsync();

            if (orderItems == null || orderItems.Count() == 0)
            {
                return false;
            }

            orderItems.ForEach(oi =>
            {
                var variant = _context.ProductVariants
                                   .FirstOrDefault(v => v.ProductId == oi.ProductId && v.ProductTypeId == oi.ProductTypeId);
                // Turn back variant quantity
                variant.Quantity += oi.Quantity;
            });

            return true;
        }

        private async Task<List<Order>> FindAdminOrdersBySearchText(string searchText)
        {
            return await _context.Orders
                                .Where(p => p.InvoiceCode.ToLower().Contains(searchText.ToLower()))
                                .ToListAsync();
        }

        private async Task<List<Order>> FindAdminOrdersByState(OrderState orderState)
        {
            return await _context.Orders
                             .Where(o => o.State == orderState)
                             .ToListAsync();
        }

        #endregion PrivateService
    }
}
