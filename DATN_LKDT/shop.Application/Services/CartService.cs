using AutoMapper;
using Microsoft.EntityFrameworkCore;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs;
using shop.Application.ViewModels.ResponseDTOs;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace shop.Application.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public CartService(AppDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;
        }
        public async Task<int> GetCartTotalAmountAsync()
        {
            int totalAmount = 0;
            var cartItem = (await GetCartItems()).Data;
            if (cartItem == null)
            {
                return 0;
            }
            cartItem.ForEach(ci => totalAmount += ci.Price * ci.Quantity);
            return totalAmount;

        }
        public async Task<ApiResponse<bool>> AddToCart(StoreCartItemDto newItem)
        {
            var accountId = _authService.GetUserId();

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);

            if(account == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Bạn chưa đăng nhập"
                };
            }

            var dbCart = await GetCustomerCart(account);

            var sameItem = await _context.CartItems
                                           .FirstOrDefaultAsync(ci => ci.ProductId == newItem.ProductId && ci.ProductTypeId == newItem.ProductTypeId && ci.CartId == dbCart.Id);

            if (sameItem == null)
            {
                var item = _mapper.Map<CartItem>(newItem);
                item.CartId = dbCart.Id;
                _context.CartItems.Add(item);
            }
            else
            {
                sameItem.Quantity += newItem.Quantity;
            }

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Sản phẩm đã thêm vào giỏ hàng"
            };
        }

        public async Task<ApiResponse<bool>> RemoveFromCart(Guid productId, Guid productTypeId)
        {
            var accountId = _authService.GetUserId();

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);

            if (account == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Bạn chưa đăng nhập"
                };
            }

            var dbCart = await _context.Carts.FirstOrDefaultAsync(c => c.AccountId == accountId);

            if (dbCart == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy giỏ hàng"
                };
            }

            var item = await _context.CartItems
                                        .FirstOrDefaultAsync(ci => ci.ProductId == productId && ci.ProductTypeId == productTypeId && ci.CartId == dbCart.Id);

            if (item == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy sản phẩm trong giỏ hàng"
                };
            }

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Đã bỏ sản phẩm ra khỏi giỏ hàng"
            };
        }

        public async Task<ApiResponse<bool>> UpdateQuantity(StoreCartItemDto updateItem)
        {
            var accountId = _authService.GetUserId();

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);

            if (account == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Bạn chưa đăng nhập"
                };
            }

            var dbCart = await GetCustomerCart(account);

            var dbItem = await _context.CartItems
                                            .FirstOrDefaultAsync(ci => ci.ProductId == updateItem.ProductId && ci.ProductTypeId == updateItem.ProductTypeId && ci.CartId == dbCart.Id);

            if (dbItem == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy sản phẩm trong giỏ hàng"
                };
            }

            dbItem.Quantity = updateItem.Quantity;
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Đã cập nhật số lượng sản phẩm trong giỏ hàng"
            };
        }

        public async Task<ApiResponse<List<CustomerCartItemDto>>> GetCartItems()
        {
            var accountId = _authService.GetUserId();

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);

            if (account == null)
            {
                return new ApiResponse<List<CustomerCartItemDto>>
                {
                    Success = false,
                    Message = "Bạn chưa đăng nhập"
                };
            }

            var result = new ApiResponse<List<CustomerCartItemDto>>
            {
                Data = new List<CustomerCartItemDto>()
            };

            var dbCart = await _context.Carts.FirstOrDefaultAsync(c => c.AccountId == accountId);

            if (dbCart == null)
            {
                return result;
            }

            var items = await _context.CartItems
                                    .Where(ci => ci.CartId == dbCart.Id)
                                    .ToListAsync();

            if (items == null)
            {
                return result;
            }

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

                var cartProduct = new CustomerCartItemDto
                {
                    ProductId = product.Id,
                    ProductTitle = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    OriginalPrice = productVariant.OriginalPrice,
                    ProductTypeId = productVariant.ProductTypeId,
                    ProductTypeName = productVariant.ProductType.Name,
                    Quantity = item.Quantity
                };

                result.Data.Add(cartProduct);
            }

            return result;
        }

        public async Task<ApiResponse<bool>> StoreCartItems(List<StoreCartItemDto> items)
        {
            var accountId = _authService.GetUserId();

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);

            if (account == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Bạn chưa đăng nhập"
                };
            }

            // Map DTOs to Entity
            var cartItems = _mapper.Map<List<CartItem>>(items);

            var dbCart = await GetCustomerCart(account);

            if (cartItems != null)
            {
                foreach (var cartItem in cartItems)
                {
                    cartItem.CartId = dbCart.Id; // Set CartId for each CartItem

                    // Check if CartItem already exists in Cart
                    var existingCartItem = dbCart.CartItems
                        .FirstOrDefault(ci => ci.ProductId == cartItem.ProductId && ci.ProductTypeId == cartItem.ProductTypeId);

                    if (existingCartItem != null)
                    {
                        // Update the quantity of existing CartItem
                        existingCartItem.Quantity += cartItem.Quantity;
                    }
                    else
                    {
                        // Add new CartItem to the Cart
                        dbCart.CartItems.Add(cartItem);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Giỏ hàng đã được cập nhật"
            };
        }

        private async Task<CartEntity> CreateCustomerCart(AccountEntity account)
        {
            var newCart = new CartEntity
            {
                Id = Guid.NewGuid(),
                AccountId = account.Id,
                UserName = account.Username,
                CartItems = new List<CartItem>()
            };

            _context.Carts.Add(newCart);
            account.Cart = newCart;

            await _context.SaveChangesAsync();
            return newCart;
        }

        private async Task<CartEntity> GetCustomerCart(AccountEntity account)
        {
            var dbCart = await _context.Carts.FirstOrDefaultAsync(c => c.AccountId == account.Id);

            if (dbCart == null)
            {
                // If customer cart doesn't exist, create new cart
                dbCart = await CreateCustomerCart(account);
            }

            return dbCart;
        }

        public async Task<ApiResponse<List<CustomerCartItemDto>>> GetCartItemsByAccountId(Guid? accountId)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);

            if (account == null)
            {
                return new ApiResponse<List<CustomerCartItemDto>>
                {
                    Success = false,
                    Message = "Bạn chưa đăng nhập"
                };
            }

            var result = new ApiResponse<List<CustomerCartItemDto>>
            {
                Data = new List<CustomerCartItemDto>()
            };

            var dbCart = await _context.Carts.FirstOrDefaultAsync(c => c.AccountId == accountId);

            if (dbCart == null)
            {
                return result;
            }

            var items = await _context.CartItems
                                    .Where(ci => ci.CartId == dbCart.Id)
                                    .ToListAsync();

            if (items == null)
            {
                return result;
            }

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

                var cartProduct = new CustomerCartItemDto
                {
                    ProductId = product.Id,
                    ProductTitle = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    OriginalPrice = productVariant.OriginalPrice,
                    ProductTypeId = productVariant.ProductTypeId,
                    ProductTypeName = productVariant.ProductType.Name,
                    Quantity = item.Quantity
                };

                result.Data.Add(cartProduct);
            }

            return result;
        }
    }
}
