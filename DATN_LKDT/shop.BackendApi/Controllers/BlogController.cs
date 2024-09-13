using AppBusiness.Model.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.BlogDto;
using shop.Application.ViewModels.ResponseDTOs.CustomerResponseDto;
using shop.Domain.Entities;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _service;

        public BlogController(IBlogService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<Pagination<List<CustomerBlogResponse>>>>> GetBlogsAsync([FromQuery] int page, [FromQuery] double pageResults)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            if(pageResults == null || pageResults <= 0)
            {
                pageResults = 8f;
            }
            var response = await _service.GetBlogsAsync(page, pageResults);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("{slug}")]
        public async Task<ActionResult<ApiResponse<CustomerBlogResponse>>> GetSingleBlogAsync(string slug)
        {
            var response = await _service.GetSingleBlog(slug);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("admin")]
        public async Task<ActionResult<ApiResponse<Pagination<List<BlogEntity>>>>> GetAdminBlogs([FromQuery] int page, [FromQuery] double pageResults)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            if (pageResults == null || pageResults <= 0)
            {
                pageResults = 8f;
            }
            var response = await _service.GetAdminBlogs(page, pageResults);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("admin/{id}")]
        public async Task<ActionResult<ApiResponse<BlogEntity>>> GetAdminBlog(Guid id)
        {
            var response = await _service.GetAdminSingleBlog(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost("admin")]
        public async Task<ActionResult<ApiResponse<bool>>> CreateBlog(AddBlogDto newBlog)
        {
            var response = await _service.CreateBlog(newBlog);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateBlog(Guid id,UpdateBlogDto updateBlog)
        {
            var response = await _service.UpdateBlog(id, updateBlog);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> SoftDeleteBlog(Guid id)
        {
            var response = await _service.SoftDeleteBlog(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("admin/search/{searchText}")]
        public async Task<ActionResult<ApiResponse<Pagination<List<Product>>>>> SearchAdminBlogs(string searchText, [FromQuery] int page, [FromQuery] double pageResults)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            if (pageResults == null || pageResults <= 0)
            {
                pageResults = 10f;
            }
            var response = await _service.SearchAdminBlogs(searchText, page, pageResults);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
