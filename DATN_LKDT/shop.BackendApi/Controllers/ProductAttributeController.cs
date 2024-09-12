using AppBusiness.Model.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.ProductAttributeDto;
using shop.Domain.Entities;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class ProductAttributeController : ControllerBase
    {
        private readonly IProductAttributeService _service;

        public ProductAttributeController(IProductAttributeService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<Pagination<List<ProductAttribute>>>>> GetProductAttributes([FromQuery] int page)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            var response = await _service.GetProductAttributes(page);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<List<ProductAttribute>>>> GetProductAttribute(Guid id)
        {
            var response = await _service.GetProductAttribute(id);
            if (!response.Success)
            {   
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("admin")]
        public async Task<ActionResult<ApiResponse<bool>>> AddProductAttribute(AddUpdateProductAttributeDto productAttribute)
        {
            var response = await _service.CreateProductAttribute(productAttribute);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateProductAttribute(Guid id, AddUpdateProductAttributeDto productAttribute)
        {
            var response = await _service.UpdateProductAttribute(id, productAttribute);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteProductAttribute(Guid id)
        {
            var response = await _service.DeleteProductAttribute(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("select/{productId}")]
        public async Task<ActionResult<ApiResponse<List<ProductAttribute>>>> GetSelectProductAttributes(Guid productId)
        {
            var response = await _service.GetProductAttributeSelect(productId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("admin/search/{searchText}")]
        public async Task<ActionResult<ApiResponse<Pagination<List<ProductAttribute>>>>> SearchAdminProductAttributes(string searchText, [FromQuery] int page, [FromQuery] double pageResults)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            if (pageResults == null || pageResults <= 0)
            {
                pageResults = 10f;
            }
            var response = await _service.SearchAdminProductAttributes(searchText, page, pageResults);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
