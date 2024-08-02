using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs;
using shop.Application.ViewModels.ResponseDTOs;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<CustomerCartItemDto>>>> GetCartItems()
        {
            var response = await _service.GetCartItems();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> AddToCart(StoreCartItemDto item)
        {
            var response = await _service.AddToCart(item);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("store-cart")]
        public async Task<ActionResult<ApiResponse<bool>>> StoreCartItems(List<StoreCartItemDto> items)
        {
            var response = await _service.StoreCartItems(items);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateQuantity(StoreCartItemDto item)
        {
            var response = await _service.UpdateQuantity(item);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("{productId}")]
        public async Task<ActionResult<ApiResponse<bool>>> RemoveFromCart(Guid productId, [FromQuery] Guid productTypeId)
        {
            var response = await _service.RemoveFromCart(productId, productTypeId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
