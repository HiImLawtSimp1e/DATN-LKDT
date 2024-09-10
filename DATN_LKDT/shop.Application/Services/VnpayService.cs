using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.Vnpay;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using shop.Domain.Entities;

namespace shop.Application.Services
{
    public class VnpayService : IVnpayService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public VnpayService(AppDbContext context, IConfiguration config, ICartService cartService, IOrderService orderService)
        {
            _context = context;
            _config = config;
            _cartService = cartService;
            _orderService = orderService;
        }
        public async Task<string> CreatePaymentUrl(HttpContext context, Guid? voucherId, string transactionId)
        {
            var totalAmount = await CaculateCartTotalAmount(voucherId);

            #region Required vnpay config

            var vnpay = new VnPayLibrary();

            //Connnect vnpay
            vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["VnPay:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]);

            //Order Info
            //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY
            vnpay.AddRequestData("vnp_Amount", (totalAmount * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"]);

            //IpAddress
            vnpay.AddRequestData("vnp_IpAddr", GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _config["VnPay:Locale"]);

            //Order Info
            vnpay.AddRequestData("vnp_OrderInfo", "Payment by e-waller VNPAY");
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            //Return Url
            vnpay.AddRequestData("vnp_ReturnUrl", _config["VnPay:PaymentBackReturnUrl"]);

            // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
            vnpay.AddRequestData("vnp_TxnRef", transactionId);

            #endregion Default vnpay config

            var paymentUrl = vnpay.CreateRequestUrl(_config["VnPay:BaseUrl"], _config["VnPay:HashSecret"]);

            return paymentUrl;
        }

        public async Task<ApiResponse<VnPaymentResponseModel>> PaymentExecute(IQueryCollection collections)
        {
            var vnpay = new VnPayLibrary();
            foreach (var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            var vnp_TransactionId = vnpay.GetResponseData("vnp_TxnRef");
            var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]);
            if (!checkSignature)
            {
                return new ApiResponse<VnPaymentResponseModel>
                {
                    Success = false,
                    Message = "Signature is not valid"
                };
            }

            var result = new VnPaymentResponseModel
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderDescription = vnp_OrderInfo,
                OrderId = vnp_OrderInfo,
                TransactionId = vnp_TransactionId,
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode
            };
            return new ApiResponse<VnPaymentResponseModel>
            {
                Data = result,
            };
        }

        public async Task<ApiResponse<bool>> CreateVnpayOrder(Guid userId, Guid? voucherId)
        {
            var customer = await GetCustomerById(userId);

            if (customer == null)
            {
                return new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Missing customer information transaction"
                };
            }

            var createOrder = await _orderService.CreateOrder(voucherId, customer, "Ví điện tử (VNPay)");

            if (!createOrder.Success)
            {
                return createOrder;
            }

            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Thanh toán bằng ví điện tử VNPAY thành công!"
            };
        }

        private async Task<int> CaculateCartTotalAmount(Guid? voucherId)
        {
            int shippingCost = 30000; //hard code shipping cost

            var totalAmount = await _cartService.GetCartTotalAmountAsync();

            if (voucherId != null)
            {
                var discountValue = await CalculateDiscountValueWithId(voucherId, totalAmount);
                totalAmount -= discountValue;
            }

            totalAmount += shippingCost;

            return totalAmount;
        }

        private async Task<int> CalculateDiscountValueWithId(Guid? voucherId, int totalAmount)
        {
            int result = 0;

            //Check if the voucher is active, not expired, and has remaining quantity or not.
            var voucher = await _context.Discounts
                                           .Where(v => v.IsActive == true && DateTime.Now > v.StartDate && DateTime.Now < v.EndDate && v.Quantity > 0)
                                           .FirstOrDefaultAsync(v => v.Id == voucherId);

            if (voucher == null)
            {
                return result;
            }

            if (voucher.IsDiscountPercent)
            {
                var discountValue = (int)(totalAmount * (voucher.DiscountValue / 100));

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

        private async Task<AccountEntity> GetCustomerById(Guid userId)
        {
            var customer = await _context.Accounts
                                        .Include(c => c.Cart)
                                        .FirstOrDefaultAsync(c => c.Id == userId);
            return customer;
        }

        private string GetIpAddress(HttpContext context)
        {
            var ipAddress = string.Empty;
            try
            {
                var remoteIpAddress = context.Connection.RemoteIpAddress;

                if (remoteIpAddress != null)
                {
                    if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList
                            .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
                    }

                    if (remoteIpAddress != null) ipAddress = remoteIpAddress.ToString();

                    return ipAddress;
                }
            }
            catch (Exception ex)
            {
                return "Invalid IP:" + ex.Message;
            }

            return "127.0.0.1";
        }
    }
}
