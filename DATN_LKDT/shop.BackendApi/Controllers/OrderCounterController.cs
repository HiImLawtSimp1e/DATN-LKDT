using AppBusiness.Model.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.OrderCounterDto;
using shop.Application.ViewModels.ResponseDTOs.CustomerResponseDto;
using shop.Application.ViewModels.ResponseDTOs.OrderCounterDto;
using shop.Domain.Entities;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class OrderCounterController : ControllerBase
    {
        private readonly IOrderCounterService _service;

        public OrderCounterController(IOrderCounterService service)
        {
            _service = service;
        }
        [HttpGet("search-product/{searchText}")]
        public async Task<ActionResult<ApiResponse<List<SearchProductItemResponse>>>> SearchProducts(string searchText)
        {
            var res = await _service.SearchProducts(searchText);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpGet("search-address/{searchText}")]
        public async Task<ActionResult<ApiResponse<List<SearchAddressItemResponse>>>> SearchAddressItems(string searchText)
        {
            var res = await _service.SearchAddressItems(searchText);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpGet("select/payment-method")]
        public async Task<ActionResult<ApiResponse<List<PaymentMethod>>>> GetPaymentMethodSelect()
        {
            var res = await _service.GetPaymentMethodSelect();
            if (!res.Success)
            {
                return BadRequest();
            }
            return Ok(res);
        }
        [HttpPost("create-order")]
        public async Task<ActionResult<ApiResponse<bool>>> CreateOrderCounter([FromQuery] Guid? voucherId, CreateOrderCounterDto newOrder)
        {
            var res = await _service.CreateOrderCounter(voucherId, newOrder);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpPost("apply-voucher")]
        public async Task<ActionResult<ApiResponse<CustomerVoucherResponseDto>>> ApplyVoucher(string discountCode, int totalAmount)
        {
            var response = await _service.ApplyVoucher(discountCode, totalAmount);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("provisional-order")]
        public async Task<ActionResult<ApiResponse<Pagination<List<Order>>>>> GetAdminProvisionalOrders([FromQuery] int page, [FromQuery] double pageResults)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            if (pageResults == null || pageResults <= 0)
            {
                pageResults = 10f;
            }
            var response = await _service.GetAdminProvisionalOrders(page, pageResults);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("search/provisional-order/{searchText}")]
        public async Task<ActionResult<ApiResponse<Pagination<List<Order>>>>> SearchAdminProvisionalOrders(string searchText, [FromQuery] int page, [FromQuery] double pageResults)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            if (pageResults == null || pageResults <= 0)
            {
                pageResults = 10f;
            }
            var response = await _service.SearchAdminProvisionalOrders(searchText, page, pageResults);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("save-provisional-order")]
        public async Task<ActionResult<ApiResponse<bool>>> SaveProvisionalInvoice(SaveOrderCounterDto saveOrder)
        {
            var res = await _service.SaveProvisionalInvoice(saveOrder);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
