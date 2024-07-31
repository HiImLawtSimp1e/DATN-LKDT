using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.AuthDTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Bạn chưa nhập tên tài khoản")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Mật khẩu không được để trống"), StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu không được dài hơn 100 ký tự & ngắn hơn 6 ký tự")]
        public string Password { get; set; } = string.Empty;
        [Compare("Password", ErrorMessage = "   ")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bạn chưa nhập họ và tên"), StringLength(50, MinimumLength = 6, ErrorMessage = "Họ tên không được dài hơn 50 ký tự & ngắn hơn 6 ký tự")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bạn chưa nhập Email"), EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại"), RegularExpression(@"^(\+?\d{1,3})?0?\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
