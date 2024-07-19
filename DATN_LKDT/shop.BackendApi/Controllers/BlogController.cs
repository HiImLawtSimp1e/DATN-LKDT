using AppBusiness.Model.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs;
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
        public async Task<ActionResult<ApiResponse<Pagination<List<CustomerBlogResponse>>>>> GetBlogsAsync([FromQuery] int currentPage, [FromQuery] int pageSize)
        {
            if (currentPage == null || currentPage <= 0)
            {
                currentPage = 1;
            }
            if(pageSize == null || pageSize <= 0)
            {
                pageSize = 8;
            }
            var response = await _service.GetBlogsAsync(currentPage, pageSize);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CustomerBlogResponse>>> GetSingleBlogAsync(Guid id)
        {
            var response = await _service.GetSingleBlog(id);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("admin")]
        public async Task<ActionResult<ApiResponse<Pagination<List<BlogEntity>>>>> GetAdminBlogs([FromQuery] int currentPage, [FromQuery] int pageSize)
        {
            if (currentPage == null || currentPage <= 0)
            {
                currentPage = 1;
            }
            if (pageSize == null || pageSize <= 0)
            {
                pageSize = 8;
            }
            var response = await _service.GetAdminBlogs(currentPage, pageSize);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("admin/{id}")]
        public async Task<ActionResult<ApiResponse<BlogEntity>>> GetAdminBlog(Guid id)
        {
            var response = await _service.GetSingleBlog(id);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("admin")]
        public async Task<ActionResult<ApiResponse<bool>>> CreateBlog(AddBlogDto newBlog)
        {
            var response = await _service.CreateBlog(newBlog);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateBlog(Guid id,UpdateBlogDto updateBlog)
        {
            var response = await _service.UpdateBlog(id, updateBlog);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> SoftDeleteBlog(Guid id)
        {
            var response = await _service.SoftDeleteBlog(id);
            if (!response.IsSuccessed)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
