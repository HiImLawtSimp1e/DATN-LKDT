using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs.CategoryDto
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Không được bỏ trống tiêu đề danh mục")]
        [MinLength(2, ErrorMessage = "Tiêu đề danh mục phải có ít nhất 2 ký tự")]
        [StringLength(50, ErrorMessage = "Tiêu đề danh mục không được vượt quá 50 ký tự")]
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
