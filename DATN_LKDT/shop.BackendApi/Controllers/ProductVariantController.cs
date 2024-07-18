using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.ProductVariantDto;
using shop.Domain.Entities;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        private readonly IProductVariantService _service;

        public ProductVariantController(IProductVariantService service)
        {
            _service = service;
        }
        [HttpGet("{productId}")]
        public async Task<ActionResult<ApiResponse<ProductVariant>>> GetVariant(Guid productId, [FromQuery] Guid productTypeId)
        {
            var response = await _service.GetVartiant(productId, productTypeId);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("admin/{productId}")]
        public async Task<ActionResult<ApiResponse<bool>>> AddVariant(Guid productId, AddProductVariantDto newVariant)
        {
            var response = await _service.AddVariant(productId, newVariant);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut("admin/{productId}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateVariant(Guid productId, UpdateProductVariantDto updateVariant)
        {
            var response = await _service.UpdateVariant(productId, updateVariant);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("admin/{productId}")]
        public async Task<ActionResult<ApiResponse<bool>>> SoftDeleteVariant(Guid productId, [FromQuery] Guid productTypeId)
        {
            var response = await _service.SoftDeleteVariant(productTypeId, productId);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
