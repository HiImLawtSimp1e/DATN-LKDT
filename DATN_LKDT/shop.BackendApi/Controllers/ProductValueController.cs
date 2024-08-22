using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<ApiResponse<ProductValue>>> GetAttributeValue(Guid productId, [FromQuery] Guid productAttributeId)
        {
            var response = await _service.GetAttributeValue(productId, productAttributeId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost("admin/{productId}")]
        public async Task<ActionResult<ApiResponse<bool>>> AddAttributeValue(Guid productId, AddProductValueDto newValue)
        {
            var response = await _service.AddAttributeValue(productId, newValue);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("admin/{productId}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateAttributeValue(Guid productId, UpdateProductValueDto updateValue)
        {
            var response = await _service.UpdateAttributeValue(productId, updateValue);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete("admin/{productId}")]
        public async Task<ActionResult<ApiResponse<bool>>> SoftDeleteAttributeValue(Guid productId, [FromQuery] Guid productAttributeId)
        {
            var response = await _service.SoftDeleteAttributeValue(productId, productAttributeId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
