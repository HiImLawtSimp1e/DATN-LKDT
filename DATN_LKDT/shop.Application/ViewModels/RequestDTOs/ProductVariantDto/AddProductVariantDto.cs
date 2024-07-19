using shop.Application.Common.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs.ProductVariantDto
{
    public class AddProductVariantDto
    {
        [Required(ErrorMessage = "Bạn chưa chọn loại sản phẩm")]
        public Guid ProductTypeId { get; set; }
        [Required(ErrorMessage = "Giá bán sản phẩm không được để trống"), Range(1000, int.MaxValue, ErrorMessage = "Giá bán phải lớn hơn 1000")]
        public int Price { get; set; }
        [ZeroOrRange(1000, int.MaxValue, ErrorMessage = "Giá gốc (nếu có) phải lớn hơn 1000")]
        [GreaterThanOrEqualTo("Price", ErrorMessage = "Giá gốc (nếu có) phải lớn hơn giá bán")]
        public int OriginalPrice { get; set; } = 0;
    }
}
