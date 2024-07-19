using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs.ProductAttributeDto
{
    public class AddUpdateProductAttributeDto
    {
        [Required(ErrorMessage = "Tên thuộc tính không được bỏ trống"), MinLength(2, ErrorMessage = "Tên thuộc tính phải chứa ít nhất 2 ký tự")]
        public string Name { get; set; } = string.Empty;
    }
}
