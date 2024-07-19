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
    public class ApplicationRoleController : ControllerBase
    {
        private readonly IApplicationRoleService _service;

        public ApplicationRoleController(IApplicationRoleService service)
        {
            _service = service;
        }
        [HttpGet("admin")]
        public async Task<ActionResult<ApiResponse<Pagination<List<ApplicationRole>>>>> GetRoles([FromQuery] int currentPage, [FromQuery] int pageSize)
        {
            if (currentPage == null || currentPage <= 0)
            {
                currentPage = 1;
            }
            if (pageSize == null || pageSize <= 0)
            {
                pageSize = 8;
            }
            var response = await _service.GetRoles(currentPage, pageSize);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("admin")]
        public async Task<ActionResult<ApiResponse<bool>>> CreateRole(AddApplicationRoleDto newRole)
        {
            var response = await _service.CreateRole(newRole);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateRole(string id, UpdateApplicationRoleDto updateRole)
        {
            var response = await _service.UpdateRole(id, updateRole);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteRole(string id)
        {
            var response = await _service.DeleteRole(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
