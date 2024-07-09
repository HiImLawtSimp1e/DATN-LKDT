using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs
{
    public class UpdateApplicationRoleDto
    {
        [StringLength(50, ErrorMessage = "Tên chức vụ không được dài quá 50 ký tự")]
        public string Name { get; set; } = string.Empty;
        public string NormalizedName { get; set; } = string.Empty;
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }
}
