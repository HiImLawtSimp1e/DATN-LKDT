using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs.CategoryDto
{
    public class AddCategoryDto
    {
        [Required(ErrorMessage = "Không được bỏ trống tiêu đề danh mục"), MinLength(2, ErrorMessage = "Tiêu đề danh mục phải có ít nhất 2 ký tự")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "slug của danh mục không được để trống"), MinLength(2, ErrorMessage = "Slug của danh mục phải chứa ít nhất 2 ký tự")]
        public string Slug { get; set; } = string.Empty;
    }
}
