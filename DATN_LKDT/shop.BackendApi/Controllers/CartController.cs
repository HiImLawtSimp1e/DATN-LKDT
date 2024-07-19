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
            var mockAccountId = new Guid("2B25A754-A50E-4468-942C-D65C0BC2C86F");
            var response = await _service.GetCartItems(mockAccountId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> AddToCart(StoreCartItemDto item)
        {
            var mockAccountId = new Guid("2B25A754-A50E-4468-942C-D65C0BC2C86F");
            var response = await _service.AddToCart(mockAccountId, item);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("store-cart")]
        public async Task<ActionResult<ApiResponse<bool>>> StoreCartItems(List<StoreCartItemDto> items)
        {
            var mockAccountId = new Guid("2B25A754-A50E-4468-942C-D65C0BC2C86F");
            var response = await _service.StoreCartItems(mockAccountId, items);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateQuantity(StoreCartItemDto item)
        {
            var mockAccountId = new Guid("2B25A754-A50E-4468-942C-D65C0BC2C86F");
            var response = await _service.UpdateQuantity(mockAccountId, item);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("{productId}")]
        public async Task<ActionResult<ApiResponse<bool>>> RemoveFromCart(Guid productId, [FromQuery] Guid productTypeId)
        {
            var mockAccountId = new Guid("2B25A754-A50E-4468-942C-D65C0BC2C86F");
            var response = await _service.RemoveFromCart(mockAccountId, productId, productTypeId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
