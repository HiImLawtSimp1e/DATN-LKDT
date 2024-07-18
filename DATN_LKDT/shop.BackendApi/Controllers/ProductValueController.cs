using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.ProductValueDto;
using shop.Domain.Entities;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductValueController : ControllerBase
    {
        private readonly IProductValueService _service;

        public ProductValueController(IProductValueService service)
        {
            _service = service;
        }
        [HttpGet("{productId}")]
        public async Task<ActionResult<ApiResponse<ProductValue>>> Get(Guid productId, [FromQuery] Guid productAttributeId)
        {
            var response = await _service.GetAttributeValue(productId, productAttributeId);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("admin/{productId}")]
        public async Task<ActionResult<ApiResponse<bool>>> AddVariant(Guid productId, AddProductValueDto newValue)
        {
            var response = await _service.AddAttributeValue(productId, newValue);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut("admin/{productId}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateVariant(Guid productId, UpdateProductValueDto updateValue)
        {
            var response = await _service.UpdateAttributeValue(productId, updateValue);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("admin/{productId}")]
        public async Task<ActionResult<ApiResponse<bool>>> SoftDeleteVariant(Guid productId, [FromQuery] Guid productAttributeId)
        {
            var response = await _service.SoftDeleteAttributeValue(productId, productAttributeId);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
