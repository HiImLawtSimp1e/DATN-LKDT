using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.ProductImageDto;
using shop.Domain.Entities;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _service;

        public ProductImageController(IProductImageService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProductImage>>> GetProductImage(Guid id)
        {
            var res = await _service.GetProductImage(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpPost("admin")]
        public async Task<ActionResult<ApiResponse<bool>>> CreateProductImage(AddProductImageDto newImage)
        {
            var res = await _service.CreateProductImage(newImage);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpPut("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateProductImage(Guid id, UpdateProductImageDto updateImage)
        {
            var res = await _service.UpdateProductImage(id, updateImage);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpDelete("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteProductImage(Guid id)
        {
            var res = await _service.DeleteProductImage(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
