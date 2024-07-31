using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Application.Common;
using shop.Application.Interfaces;
using shop.Application.ViewModels.AuthDTOs;

namespace shop.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<string>>> Register(RegisterDto req)
        {
            var res = await _service.Register(req);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<string>>> Login(LoginDto req)
        {
            var res = await _service.Login(req);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpPost("admin/login")]
        public async Task<ActionResult<ApiResponse<string>>> AdminLogin(LoginDto req)
        {
            var res = await _service.AdminLogin(req);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
        [HttpPost("verify-token")]
        public async Task<ActionResult<ApiResponse<string>>> VerifyToken(string token)
        {
            var res = await _service.VerifyToken(token);
            if (!res.Success)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
