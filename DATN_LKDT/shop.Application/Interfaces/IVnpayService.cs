using Microsoft.AspNetCore.Http;
using shop.Application.Common;
using shop.Application.Vnpay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IVnpayService
    {
        public Task<string> CreatePaymentUrl(HttpContext context, Guid? voucherId, string transactionId);
        public Task<ApiResponse<VnPaymentResponseModel>> PaymentExecute(IQueryCollection collections);
        public Task<ApiResponse<bool>> CreateVnpayOrder(Guid userId, Guid? voucherId);
    }
}
