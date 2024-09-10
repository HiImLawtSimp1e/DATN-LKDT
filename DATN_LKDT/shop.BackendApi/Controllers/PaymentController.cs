using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Application.Interfaces;
using shop.Application.Services;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnpayService _vnPayService;
        private readonly IVnpayTransactionService _vnpayTransactionService;
        private readonly IAuthService _authService;

        public PaymentController(IVnpayService vnPayService, IVnpayTransactionService vnpayTransactionService, IAuthService authService)
        {
            _vnPayService = vnPayService;
            _vnpayTransactionService = vnpayTransactionService;
            _authService = authService;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("vnpay/create-payment")]
        public async Task<IActionResult> CreatePaymentUrl([FromQuery] Guid? voucherId)
        {
            // Save userId & voucherId to db transaction
            var userId = _authService.GetUserId();
            var trans = await _vnpayTransactionService.BeginTransaction(userId, voucherId);

            // Call Service: Generate payment url
            var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, voucherId, trans);

            return Ok(new { paymentUrl });
        }

        [HttpGet("vnpay/payment-callback")]
        public async Task<IActionResult> PaymentCallback()
        {
            var response = await _vnPayService.PaymentExecute(Request.Query);

            if (!response.Success)
            {
                // Thực hiện thanh toán thất bại, chuyển hướng về ứng dụng React
                var message = response.Message;
                string redirectUrl = "http://localhost:3000/payment/failure?status=fail&message=" + message;
                return Redirect(redirectUrl);
            }

            // Get transaction
            var trans = await _vnpayTransactionService.GetTransaction(response.Data.TransactionId);

            if (trans == null)
            {
                // Missing transaction, chuyển hướng về ứng dụng React
                var message = "VNPAY transaction is missing or null.";
                string redirectUrl = "http://localhost:3000/payment/failure?status=fail&message=" + message;
                return Redirect(redirectUrl);
            }

            if (response.Data.VnPayResponseCode != "00")
            {
                //rollback transaction
                await _vnpayTransactionService.RollbackTransaction(response.Data.TransactionId);

                // Thực hiện thanh toán thất bại, chuyển hướng về ứng dụng React
                var message = "Lỗi thanh toán VN Pay: " + response.Data?.VnPayResponseCode;
                string redirectUrl = "http://localhost:3000/payment/failure?status=fail&message=" + message;
                return Redirect(redirectUrl);
            }

            //get userId & voucherId from transaction
            var userId = trans.UserId;
            var voucherId = trans.VoucherId;

            // Create VnPay Order
            var vnpayOrder = await _vnPayService.CreateVnpayOrder(userId, voucherId);

            if (!vnpayOrder.Success)
            {
                //rollback transaction
                await _vnpayTransactionService.RollbackTransaction(response.Data.TransactionId);

                // tạo hóa đơn thất bại thất bại, chuyển hướng về ứng dụng React
                var message = vnpayOrder.Message;
                string redirectUrl = "http://localhost:3000/payment/failure?status=fail&message=" + message;
                return Redirect(redirectUrl);
            }

            // Successfully, Commit transaction
            await _vnpayTransactionService.CommitTransaction(response.Data.TransactionId);

            // Successfully, redirect to React app
            string successUrl = "http://localhost:3000/payment/success?status=success";
            return Redirect(successUrl);

        }
    }
}
