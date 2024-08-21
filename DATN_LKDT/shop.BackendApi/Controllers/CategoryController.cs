using AppBusiness.Model.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.CategoryDto;
using shop.Application.ViewModels.ResponseDTOs.CustomerResponseDto;
using shop.Domain.Entities;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<Pagination<List<CustomerCategoryResponseDto>>>>> GetCategoriesAsync()
        {
            var response = await _service.GetCategoriesAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("admin")]
        public async Task<ActionResult<ApiResponse<Pagination<List<Category>>>>> GetAdminCategories([FromQuery] int page)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            var response = await _service.GetAdminCategories(page);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("admin/{id}")]
        public async Task<ActionResult<ApiResponse<Category>>> GetAdminCategory(Guid id)
        {
            var response = await _service.GetAdminCategory(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("select")]
        public async Task<ActionResult<ApiResponse<List<Category>>>> GetCategoriesSelect()
        {
            var response = await _service.GetCategoriesSelect();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost("admin")]
        public async Task<ActionResult<ApiResponse<bool>>> CreateCategory(AddCategoryDto newCategory)
        {
            var response = await _service.CreateCategory(newCategory);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateCategory(Guid id, UpdateCategoryDto category)
        {
            var response = await _service.UpdateCategory(id, category);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete("admin/{categoryId}")]
        public async Task<ActionResult<ApiResponse<bool>>> SoftDeleteCategories(Guid categoryId)
        {
            var response = await _service.SoftDeleteCategory(categoryId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
