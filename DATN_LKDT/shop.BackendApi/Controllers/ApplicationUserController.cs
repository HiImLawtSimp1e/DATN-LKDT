using AppBusiness.Model.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs;
using shop.Domain.Entities;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUserService _service;

        public ApplicationUserController(IApplicationUserService service)
        {
            _service = service;
        }
        [HttpGet("admin")]
        public async Task<ActionResult<ApiResponse<Pagination<List<ApplicationUser>>>>> GetUsers([FromQuery] int currentPage, [FromQuery] int pageSize)
        {
            if (currentPage == null || currentPage <= 0)
            {
                currentPage = 1;
            }
            if (pageSize == null || pageSize <= 0)
            {
                pageSize = 8;
            }
            var response = await _service.GetUsers(currentPage, pageSize);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("admin")]
        public async Task<ActionResult<ApiResponse<bool>>> CreateUser(AddApplicationUserDto newUser)
        {
            var response = await _service.CreateUser(newUser);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateUser(string id, UpdateApplicationUserDto updateUser)
        {
            var response = await _service.UpdateUser(id, updateUser);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> SoftDeleteUser(string id)
        {
            var response = await _service.SoftDeleteUser(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
