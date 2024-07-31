using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.AuthDTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Bạn chưa nhập username")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        public string Password { get; set; } = string.Empty;
    }
}
