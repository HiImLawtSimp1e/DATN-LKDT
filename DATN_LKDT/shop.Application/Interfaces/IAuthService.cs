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
        Task<ApiResponse<bool>> ChangePassword(Guid accountId, string newPassword);
        Task<ApiResponse<string>> VerifyToken(string token);
        Guid GetUserId();
        string GetUserName();
    }
}
