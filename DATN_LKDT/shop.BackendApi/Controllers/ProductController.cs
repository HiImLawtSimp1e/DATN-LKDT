using AppBusiness.Model.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.ProductDto;
using shop.Application.ViewModels.ResponseDTOs.CustomerProductResponseDto;
using shop.Domain.Entities;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<Pagination<List<CustomerProductResponseDto>>>>> GetProductsAsync([FromQuery] int page, [FromQuery] double pageResults)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            if (pageResults == null || pageResults <= 0)
            {
                pageResults = 12f;
            }
            var response = await _service.GetProductsAsync(page, pageResults);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("admin")]
        public async Task<ActionResult<ApiResponse<Pagination<List<Product>>>>> GetAdminProducts([FromQuery] int page, [FromQuery] double pageResults)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            if (pageResults == null || pageResults <= 0)
            {
                pageResults = 10f;
            }
            var response = await _service.GetAdminProducts(page, pageResults);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("admin/{id}")]
        public async Task<ActionResult<ApiResponse<Product>>> GetAdminProduct(Guid id)
        {
            var response = await _service.GetAdminSingleProduct(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("{slug}")]
        public async Task<ActionResult<ApiResponse<CustomerProductResponseDto>>> GetProductBySlug(string slug)
        {
            var response = await _service.GetProductBySlug(slug);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("list/{categorySlug}")]
        public async Task<ActionResult<ApiResponse<Pagination<List<CustomerProductResponseDto>>>>> GetProductsByCategory(string categorySlug, [FromQuery] int page, [FromQuery] double pageResults)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            if (pageResults == null || pageResults <= 0)
            {
                pageResults = 12f;
            }
            var response = await _service.GetProductsByCategory(categorySlug, page, pageResults);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost("admin")]
        public async Task<ActionResult<ApiResponse<bool>>> CreateProduct(AddProductDto newProduct)
        {
            var response = await _service.CreateProduct(newProduct);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateProduct(Guid id, UpdateProductDto product)
        {
            var response = await _service.UpdateProduct(id, product);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete("admin/{productId}")]
        public async Task<ActionResult<ApiResponse<bool>>> SoftDeleteProduct(Guid productId)
        {
            var response = await _service.SoftDeleteProduct(productId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<ApiResponse<Pagination<List<CustomerProductResponseDto>>>>> SearchProducts(string searchText, [FromQuery] int page, [FromQuery] double pageResults)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            if (pageResults == null || pageResults <= 0)
            {
                pageResults = 12f;
            }
            var response = await _service.SearchProducts(searchText, page, pageResults);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("search-suggestions/{searchText}")]
        public async Task<ActionResult<ApiResponse<List<string>>>> GetProductSearchSuggestions(string searchText)
        {
            var response = await _service.GetProductSearchSuggestions(searchText);
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("admin/search/{searchText}")]
        public async Task<ActionResult<ApiResponse<Pagination<List<Product>>>>> SearchAdminProducts(string searchText, [FromQuery] int page, [FromQuery] double pageResults)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            if (pageResults == null || pageResults <= 0)
            {
                pageResults = 10f;
            }
            var response = await _service.SearchAdminProducts(searchText, page, pageResults);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
