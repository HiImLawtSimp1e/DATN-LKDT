using AppBusiness.Model.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.AccountDto;
using shop.Application.ViewModels.ResponseDTOs.AccountResponseDto;
using shop.Domain.Entities;
using System.Data;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }
        [HttpGet("admin")]
        public async Task<ActionResult<ApiResponse<Pagination<List<AccountListResponseDto>>>>> GetAdminAccounts([FromQuery] int currentPage, [FromQuery] double pageResults)
        {
            if (currentPage == null || currentPage <= 0)
            {
                currentPage = 1;
            }
            if (pageResults == null || pageResults <= 0)
            {
                pageResults = 8f;
            }
            var response = await _service.GetAdminAccounts(currentPage, pageResults);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("admin/{id}")]
        public async Task<ActionResult<ApiResponse<AccountDetailResponseDto>>> GetAdminSingleAccount(Guid id)
        {
            var response = await _service.GetAdminSingleAccount(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("admin/role")]
        public async Task<ActionResult<ApiResponse<List<RoleEntity>>>> GetAdminRoles()
        {
            var res = await _service.GetAdminRoles();
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
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
