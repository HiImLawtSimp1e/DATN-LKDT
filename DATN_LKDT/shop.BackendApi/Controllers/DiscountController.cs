using AppBusiness.Model.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.DiscountDto;
using shop.Domain.Entities;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _service;

        public DiscountController(IDiscountService service)
        {
            _service = service;
        }
        [HttpGet("admin")]
        public async Task<ActionResult<ApiResponse<Pagination<List<DiscountEntity>>>>> GetVouchers([FromQuery] int page, [FromQuery] double pageResults)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            if (pageResults == null || pageResults <= 0)
            {
                pageResults = 12f;
            }
            var res = await _service.GetVouchers(page, pageResults);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpGet("admin/{id}")]
        public async Task<ActionResult<ApiResponse<DiscountEntity>>> GetVoucher(Guid id)
        {
            var res = await _service.GetVoucher(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpPost("admin")]
        public async Task<ActionResult<ApiResponse<bool>>> CreateVoucher(AddDiscountDto newVoucher)
        {
            var res = await _service.CreateVoucher(newVoucher);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpPut("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateVoucher(Guid id, UpdateDiscountDto updateVoucher)
        {
            var res = await _service.UpdateVoucher(id, updateVoucher);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpDelete("admin/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteVoucher(Guid id)
        {
            var res = await _service.DeleteVoucher(id);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
