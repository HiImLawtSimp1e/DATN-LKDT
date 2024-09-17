using AppBusiness.Model.Pagination;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Style;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs;
using shop.Application.ViewModels.RequestDTOs.OrderCounterDto;
using shop.Application.ViewModels.ResponseDTOs.CustomerResponseDto;
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
        #region OrderCounterService
        public async Task<ApiResponse<bool>> CreateOrderCounter(Guid? voucherId, CreateOrderCounterDto newOrder)
        {
            var username = _authService.GetUserName();

            var newOrderItems = newOrder.OrderItems;

            if (newOrderItems == null || newOrderItems.Count() == 0)
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
                CreatedBy = username,
                PaymentMethodId = newOrder.PaymentMethodId,
                IsCounterOrder = true
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
                        discountValue = CalculateDiscountValue(voucher, totalAmount);
                        order.DiscountValue = discountValue;
                        order.DiscountId = voucher.Id;
                        voucher.Quantity -= 1;
                    }
                }
            }

            order.TotalAmount = order.TotalPrice - discountValue;

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

            if (addresses == null)
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
              .Include(p => p.ProductVariants.Where(pv => !pv.Deleted && pv.Quantity > 0))
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

        public async Task<ApiResponse<List<PaymentMethod>>> GetPaymentMethodSelect()
        {
            var paymentMethods = await _context.PaymentMethods
                                            .Where(pm => pm.Name != "Thanh toán khi nhận hàng (COD)")
                                            .ToListAsync();

            if (paymentMethods == null)
            {
                return new ApiResponse<List<PaymentMethod>>
                {
                    Success = false,
                    Message = "Missing payment method"
                };
            }

            return new ApiResponse<List<PaymentMethod>>
            {
                Data = paymentMethods
            };
        }

        public async Task<ApiResponse<CustomerVoucherResponseDto>> ApplyVoucher(string discountCode, int totalAmount)
        {
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
        #endregion OrderCounterService

        #region ProvisionalOrder

        //Lấy danh sách hóa đơn tạm lưu
        public async Task<ApiResponse<Pagination<List<Order>>>> GetAdminProvisionalOrders(int page, double pageResults)
        {
            var pageCount = Math.Ceiling((await FindProvisionalOrders()).Count / pageResults);

            var orders = await _context.Orders
                                   .Where(o => o.IsCounterOrder && o.State == OrderState.Pending)
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
                PageResults = (int)pageResults,
                Pages = (int)pageCount
            };

            return new ApiResponse<Pagination<List<Order>>>
            {
                Data = pagingData
            };
        }

        //Tìm kiếm hóa đơn tạm lưu
        public async Task<ApiResponse<Pagination<List<Order>>>> SearchAdminProvisionalOrders(string searchText, int page, double pageResults)
        {
            var pageCount = Math.Ceiling((await FindProvisionalOrdersBySearchText(searchText)).Count / pageResults);

            var orders = await _context.Orders
                .Where(o => o.InvoiceCode.ToLower().Contains(searchText.ToLower())
                && o.IsCounterOrder && o.State == OrderState.Pending)
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

        //Tạm lưu hóa đơn

        public  async Task<ApiResponse<bool>> SaveProvisionalInvoice(SaveOrderCounterDto saveOrder)
        {
            var username = _authService.GetUserName();

            var newOrderItems = saveOrder.OrderItems;

            if (newOrderItems == null || newOrderItems.Count() == 0)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Chưa có sản phẩm"
                };
            }

            var paymentMethod = await _context.PaymentMethods.FirstOrDefaultAsync(pm => pm.Name == "Thanh toán tiền mặt tại quầy");
            if (paymentMethod == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy phương thức thanh toán"
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
                FullName = saveOrder.Name,
                Email = saveOrder.Email,
                Phone = saveOrder.PhoneNumber,
                Address = saveOrder.Address,
                OrderItems = orderItems,
                TotalPrice = totalAmount,
                TotalAmount = totalAmount,
                State = OrderState.Pending,
                CreatedBy = username,
                PaymentMethod = paymentMethod,
                IsCounterOrder = true
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Message = "Lưu đơn hàng thành công"
            };
        }

        // Thêm sản phẩm vào đơn hàng tạm lưu

        public async Task<ApiResponse<bool>> AddToCart(Guid orderId, OrderCounterItemDto newItem)
        {
            var order = await _context.Orders
                                        .FirstOrDefaultAsync(o => o.Id == orderId);

            if(order == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy đơn hàng"
                };
            }

            if(!order.IsCounterOrder || order.State != OrderState.Pending)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Chỉ có thể thay đổi đơn hàng tạm lưu"
                };
            }

            var variant = await _context.ProductVariants
                                  .FirstOrDefaultAsync(v => v.ProductId == newItem.ProductId
                                  && v.ProductTypeId == newItem.ProductTypeId
                                  && v.IsActive && !v.Deleted);

            if (variant == null) 
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Sản phẩm đã ngừng bán"
                };
            }

            if (variant.Quantity <= 0)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Sản phẩm đã hết số lượng"
                };
            }

            var orderItem = _mapper.Map<OrderItem>(newItem);
            _context.OrderItems.Add(orderItem);

            variant.Quantity -= newItem.Quantity;

            await _context.SaveChangesAsync();

            await UpdateTotalAmountProvisionalOrderAsync(orderId);

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Đã thêm sản phẩm vào đơn hàng"
            };
        }

        // Thay đổi số lượng sản phẩm đơn hàng tạm lưu

        public async Task<ApiResponse<bool>> UpdateQuantity(Guid orderId, OrderCounterItemDto updateItem)
        {
            var order = await _context.Orders
                                       .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy đơn hàng"
                };
            }

            if (!order.IsCounterOrder || order.State != OrderState.Pending)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Chỉ có thể thay đổi đơn hàng tạm lưu"
                };
            }

            var variant = await _context.ProductVariants
                                  .FirstOrDefaultAsync(v => v.ProductId == updateItem.ProductId
                                  && v.ProductTypeId == updateItem.ProductTypeId
                                  && v.IsActive && !v.Deleted);

            if (variant == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Sản phẩm đã ngừng bán"
                };
            }

            if (variant.Quantity <= 0)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Sản phẩm đã hết số lượng"
                };
            }

            var orderItem = await _context.OrderItems
                                    .FirstOrDefaultAsync(oi => oi.ProductId == updateItem.ProductId
                                    && oi.ProductTypeId == updateItem.ProductTypeId
                                    && oi.OrderId == orderId);

            if(orderItem == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy sản phẩm trong đơn hàng"
                };
            }

            orderItem.Quantity += updateItem.Quantity;
            variant.Quantity -= updateItem.Quantity;

            await _context.SaveChangesAsync();

            await UpdateTotalAmountProvisionalOrderAsync(orderId);

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Đã thêm sản phẩm vào đơn hàng"
            };
        }

        public async Task<ApiResponse<bool>> RemoveFromCart(Guid orderId, Guid productId, Guid productTypeId)
        {
            var order = await _context.Orders
                                     .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy đơn hàng"
                };
            }

            if (!order.IsCounterOrder || order.State != OrderState.Pending)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Chỉ có thể thay đổi đơn hàng tạm lưu"
                };
            }

            var variant = await _context.ProductVariants
                                  .FirstOrDefaultAsync(v => v.ProductId == productId
                                  && v.ProductTypeId == productTypeId);

            if (variant == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy sản phẩm"
                };
            }

            var orderItem = await _context.OrderItems
                                    .FirstOrDefaultAsync(oi => oi.ProductId == productId
                                    && oi.ProductTypeId == productTypeId
                                    && oi.OrderId == orderId);

            if (orderItem == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy sản phẩm trong đơn hàng"
                };
            }

            variant.Quantity += orderItem.Quantity;
            _context.OrderItems.RemoveRange(orderItem);

            await _context.SaveChangesAsync();

            await UpdateTotalAmountProvisionalOrderAsync(orderId);

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Đã xóa sản phẩm khỏi đơn hàng"
            };
        }

        #endregion ProvisionalOrder

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

        private async Task<List<Order>> FindProvisionalOrders()
        {
            return await _context.Orders
                             .Where(o => o.IsCounterOrder && o.State == OrderState.Pending)
                             .ToListAsync();
        }

        private async Task<List<Order>> FindProvisionalOrdersBySearchText(string searchText)
        {
            return await _context.Orders
                                .Where(o => o.InvoiceCode.ToLower().Contains(searchText.ToLower())
                                && o.IsCounterOrder && o.State == OrderState.Pending)
                                .ToListAsync();
        }

        private async Task<bool> UpdateTotalAmountProvisionalOrderAsync(Guid orderId)
        {
            var order = await _context.Orders
                                  .Include(o => o.OrderItems) 
                                  .FirstOrDefaultAsync(o => o.Id == orderId);

            if(order == null)
            {
                return false;
            }

            var totalAmount = 0;

            if(order.OrderItems != null)
            {
                order.OrderItems.ForEach(oi => totalAmount += oi.Price * oi.Quantity);
            }

            order.TotalPrice = totalAmount;
            order.TotalAmount = totalAmount;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
