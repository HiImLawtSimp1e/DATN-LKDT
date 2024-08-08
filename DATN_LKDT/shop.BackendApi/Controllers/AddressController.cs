using AppBusiness.Model.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.RequestDTOs.AddressDto;
using shop.Domain.Entities;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }
        [HttpGet()]
        public async Task<ActionResult<ApiResponse<Pagination<List<AddressEntity>>>>> GetAddresses(int page)
        {
            if (page == null || page <= 0)
            {
                page = 1;
            }
            var response = await _service.GetAddresses(page);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("{addressId}")]
        public async Task<ActionResult<ApiResponse<AddressEntity>>> GetSingleAddress(Guid addressId)
        {
            var response = await _service.GetSingleAddress(addressId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("main")]
        public async Task<ActionResult<ApiResponse<AddressEntity>>> GetMainAddress()
        {
            var response = await _service.GetMainAddress();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost()]
        public async Task<ActionResult<ApiResponse<bool>>> CreateAddress(CreateAddressDto newAddress)
        {
            var response = await _service.CreateAddress(newAddress);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut("{addressId}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateAddress(Guid addressId, UpdateAddressDto updateAddress)
        {
            var response = await _service.UpdateAddress(addressId, updateAddress);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("{addressId}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAddress(Guid addressId)
        {
            var response = await _service.DeleteAddress(addressId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
