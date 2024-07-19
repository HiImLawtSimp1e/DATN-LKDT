using AppData.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs
{
    public class UpdateAccountDto
    {
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống"), MinLength(6, ErrorMessage = "Mật khẩu phải chứa ít nhất 6 ký tự"), StringLength(50, ErrorMessage = "Mật khẩu không được dài quá 50 ký tự")]
        public string Password { get; set; }
        [StringLength(50, ErrorMessage = "Tên không được dài quá 50 ký tự")]
        public string? Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email không được bỏ trống"), EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Số điện thoại không được bỏ trống"), RegularExpression(@"^(\+?\d{1,3})?0?\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ")]

        public string PhoneNumber { get; set; } = string.Empty;

        public string? ImageUrl { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
