using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs.ProductTypeDto
{
    public class AddUpdateProductTypeDto
    {
        [Required(ErrorMessage = "Tên loại sản phẩm không được thể trống"), MinLength(2, ErrorMessage = "Tên loại sản phẩm phải chứa ít nhất 2 ký tự")]
        public string Name { get; set; } = string.Empty;
    }
}
