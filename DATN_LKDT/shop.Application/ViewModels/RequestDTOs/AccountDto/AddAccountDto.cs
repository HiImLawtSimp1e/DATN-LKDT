using AppData.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs.AccountDto
{
    public class AddAccountDto
    {
        [Required(ErrorMessage = "Tên tài khoản không được bỏ trống"), StringLength(100, MinimumLength = 6, ErrorMessage = "Tên tài phải dài hơn 6 ký tự & ngắn hơn 100 ký tự")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống"), StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải dài hơn 6 ký tự & ngắn hơn 100 ký tự")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bạn chưa nhập họ và tên"), StringLength(50, MinimumLength = 6, ErrorMessage = "Họ và tên phải dài hơn 6 ký tự & ngắn hơn 50 ký tự")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bạn chưa nhập Email"), EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Email phải có tối thiểu 6 ký tự & không được dài hơn 30 ký tự")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại"), RegularExpression(@"^(\+?\d{1,3})?0?\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ"), StringLength(250, MinimumLength = 6, ErrorMessage = "Địa chỉ phải dài hơn 6 ký tự & ngắn hơn 250 ký tự")]
        public string Address { get; set; } = string.Empty;
        public Guid RoleId { get; set; }
    }
}
