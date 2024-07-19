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
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }
        [HttpGet("admin")]
        public async Task<ActionResult<ApiResponse<Pagination<List<AccountEntity>>>>> GetAdminAccounts([FromQuery] int currentPage, [FromQuery] int pageSize)
        {
            if (currentPage == null || currentPage <= 0)
            {
                currentPage = 1;
            }
            if (pageSize == null || pageSize <= 0)
            {
                pageSize = 8;
            }
            var response = await _service.GetAdminAccounts(currentPage, pageSize);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("admin/{id}")]
        public async Task<ActionResult<ApiResponse<AccountEntity>>> GetAdminAccount(Guid id)
        {
            var response = await _service.GetAdminSingleAccount(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("admin")]
        public async Task<ActionResult<ApiResponse<bool>>> CreateAccount(AddAccountDto newAccount)
        {
            var response = await _service.CreateAccount(newAccount);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateAccount(Guid id, UpdateAccountDto updateAccount)
        {
            var response = await _service.UpdateAccount(id, updateAccount);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> SoftDeleteAccount(Guid id)
        {
            var response = await _service.SoftDeleteAccount(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
