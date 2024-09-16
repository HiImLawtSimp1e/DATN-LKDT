using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Application.Interfaces;
using shop.Application.ViewModels.ResponseDTOs;
using shop.Domain.Entities.Enum;
using shop.Domain.Entities;
using shop.Application.Common;
using AppBusiness.Model.Pagination;
using Microsoft.AspNetCore.Authorization;
using shop.Application.ViewModels.ResponseDTOs.CustomerResponseDto;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }
        [Authorize(Roles = "Customer")]
        [HttpGet()]
        public async Task<ActionResult<ApiResponse<Pagination<List<Order>>>>> GetCustomerOrders([FromQuery] int page)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            var response = await _service.GetCustomerOrders(page);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Customer")]
        [HttpPost("place-order")]
        public async Task<ActionResult<ApiResponse<bool>>> PlaceOrder([FromQuery] Guid? voucherId)
        {
            var response = await _service.PlaceOrder(voucherId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Customer")]
        [HttpPost("apply-voucher")]
        public async Task<ActionResult<ApiResponse<CustomerVoucherResponseDto>>> ApplyVoucher(string discountCode)
        {
            var response = await _service.ApplyVoucher(discountCode);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Customer")]
        [HttpPut("cancel-order/{voucherId}")]
        public async Task<ActionResult<ApiResponse<bool>>> CancelVoucher(Guid voucherId)
        {
            var response = await _service.CancelOrder(voucherId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("admin")]
        public async Task<ActionResult<ApiResponse<Pagination<List<Order>>>>> GetAdminOrders([FromQuery] int page)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            var response = await _service.GetAdminOrders(page);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("{orderId}")]
        public async Task<ActionResult<ApiResponse<List<OrderItemDto>>>> GetOrderItems(Guid orderId)
        {
            var response = await _service.GetOrderItems(orderId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("detail/{orderId}")]
        public async Task<ActionResult<ApiResponse<OrderDetailCustomerDto>>> GetOrderDetailInfo(Guid orderId)
        {
            var response = await _service.GetOrderDetailInfo(orderId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("admin/get-state/{orderId}")]
        public async Task<ActionResult<ApiResponse<int>>> GetOrderState(Guid orderId)
        {
            var response = await _service.GetOrderState(orderId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("admin/{orderId}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateOrderState(Guid orderId, OrderState state)
        {
            var response = await _service.UpdateOrderState(orderId, state);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("admin/search/{searchText}")]
        public async Task<ActionResult<ApiResponse<Pagination<List<Order>>>>> SearchAdminOrders(string searchText, [FromQuery] int page, [FromQuery] double pageResults)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            if (pageResults == null || pageResults <= 0)
            {
                pageResults = 10f;
            }
            var response = await _service.SearchAdminOrders(searchText, page, pageResults);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("admin/filter/{orderState}")]
        public async Task<ActionResult<ApiResponse<Pagination<List<Order>>>>> FilterAdminOrdersByState(OrderState orderState, [FromQuery] int page, [FromQuery] double pageResults)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            if (pageResults == null || pageResults <= 0)
            {
                pageResults = 10f;
            }
            var response = await _service.FilterAdminOrdersByState(orderState, page, pageResults);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
