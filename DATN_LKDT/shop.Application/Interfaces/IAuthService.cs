using shop.Application.Common;
using shop.Application.ViewModels.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> Register(RegisterDto registerDTO);
        Task<ApiResponse<string>> Login(LoginDto loginDTO);
        Task<ApiResponse<string>> AdminLogin(LoginDto loginDTO);
        Task<ApiResponse<bool>> ChangePassword(ChangePasswordDto changePasswordDto);
        Task<ApiResponse<string>> VerifyToken(string token);
        Task<ApiResponse<UserInfoResponseDto>> GetClaimsUserInfo();
        Guid GetUserId();
        string GetUserName();
        string GetRoleName();
    }
}
