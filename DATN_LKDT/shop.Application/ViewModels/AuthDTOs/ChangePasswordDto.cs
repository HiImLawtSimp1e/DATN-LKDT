using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.AuthDTOs
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu cũ")]
        public string OldPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "Mật khẩu mới không được để trống"), StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu mới không được dài hơn 100 ký tự & ngắn hơn 6 ký tự")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không khớp")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
