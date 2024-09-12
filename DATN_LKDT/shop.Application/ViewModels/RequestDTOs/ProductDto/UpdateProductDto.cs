using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs.ProductDto
{
    public class UpdateProductDto
    {
        [Required(ErrorMessage = "Tiêu đề sản phẩm không được để trống")]
        [MinLength(2, ErrorMessage = "Tiêu đề sản phẩm phải chứa ít nhất 2 ký tự")]
        [StringLength(100, ErrorMessage = "Tiêu đề sản phẩm không được dài hơn 100 ký tự")]
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mô tả cản phẩm không được để trống")]
        [MinLength(6, ErrorMessage = "Mô tả sản phẩm phải chứa ít nhất 6 ký tự")]
        [StringLength(250, ErrorMessage = "Mô tả sản phẩm không được dài hơn 250 ký tự")]
        public string Description { get; set; } = string.Empty;
        [StringLength(70, ErrorMessage = "Tiêu đề SEO không được dài hơn 70 ký tự")]
        public string SeoTitle { get; set; } = string.Empty;

        [StringLength(160, ErrorMessage = "Mô tả SEO không được dài hơn 160 ký tự")]
        public string SeoDescription { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Từ khóa SEO không được dài hơn 100 ký tự")]
        public string SeoKeyworks { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn danh mục cho sản phẩm")]
        public Guid CategoryId { get; set; }
    }
}
