using shop.Application.Common.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs.ProductDto
{
    public class AddProductDto
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống"), MinLength(2, ErrorMessage = "Tên sản phẩm phải chứa ít nhất 2 ký tự")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Slug sản phẩm phải chứa ít nhất 2 ký tự"), MinLength(2, ErrorMessage = "Slug sản phẩm phải chứa ít nhất 2 ký tự")]
        public string Slug { get; set; } = string.Empty;
        [Required(ErrorMessage = "Mô tả cản phẩm không được để trống"), MinLength(6, ErrorMessage = "Mô tả sản phẩm phải chứa ít nhất 6 ký tự")]
        public string Description { get; set; } = string.Empty;
        [StringLength(70, ErrorMessage = "Tiêu đề SEO không được dài hơn 70 ký tự")]
        public string SeoTitle { get; set; } = string.Empty;

        [StringLength(160, ErrorMessage = "Mô tả SEO không được dài hơn 160 ký tự")]
        public string SeoDescription { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Từ khóa SEO không được dài hơn 100 ký tự")]
        public string SeoKeyworks { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bạn chưa chọn danh mục cho sản phẩm")]
        public Guid CategoryId { get; set; }
        [Required(ErrorMessage = "Bạn chứa chọn loại sản phẩm")]
        public Guid ProductTypeId { get; set; }
        [Required(ErrorMessage = "Giá bán sản phẩm không được để trống"), Range(1000, int.MaxValue, ErrorMessage = "Giá bán phải lớn hơn 1000")]
        public int Price { get; set; }
        [ZeroOrRange(1000, int.MaxValue, ErrorMessage = "Giá gốc (nếu có) phải lớn hơn 1000")]
        [GreaterThanOrEqualTo("Price", ErrorMessage = "Giá gốc (nếu có) phải lớn hơn giá bán")]
        public int OriginalPrice { get; set; } = 0;
    }
}
