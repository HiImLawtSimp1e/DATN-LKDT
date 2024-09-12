using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs.ProductValueDto
{
    public class AddProductValueDto
    {
        [Required(ErrorMessage = "Bạn chưa chọn tên thuộc tính sản phẩm")]
        public Guid ProductAttributeId { get; set; }
        [Required(ErrorMessage = "Giá trị thuộc tính sản phẩm không được bỏ trống")]
        [MinLength(2, ErrorMessage = "Giá trị thuộc tính sản phẩm phải chứa ít nhất 2 ký tự")]
        [StringLength(100, ErrorMessage = "Giá trị thuộc tính sản phẩm không được dài quá 100 ký tự")]
        public string Value { get; set; } = string.Empty;
    }
}
