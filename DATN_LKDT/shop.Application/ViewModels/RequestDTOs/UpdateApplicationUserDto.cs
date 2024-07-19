using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs
{
    public class UpdateApplicationUserDto
    {
        public string? CitizenId { get; set; } = string.Empty;
        [StringLength(50, ErrorMessage = "Tên không được dài quá 50 ký tự")]
        public string? Name { get; set; } = string.Empty;
        [StringLength(250, ErrorMessage = "Địa chỉ không được dài quá 250 ký tự")]
        public string? Address { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
