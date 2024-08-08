using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs.AddressDto
{
    public class CreateAddressDto
    {
        [Required(ErrorMessage = "Bạn chưa nhập họ và tên"), MinLength(6, ErrorMessage = "Họ và tên không được ngắn hơn 6 ký tự"), StringLength(50, ErrorMessage = "Họ vè tên không được dài hơn 50 ký tự")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bạn chưa nhập email"), EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại"), RegularExpression(@"^(\+?\d{1,3})?0?\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ"), MinLength(6, ErrorMessage = "Địa chỉ không được ngắn hơn 6 ký tự"), StringLength(250, ErrorMessage = "Địa chỉ không được dài hơn 250 ký tự")]
        public string Address { get; set; } = string.Empty;
        public bool IsMain { get; set; }
    }
}
