using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace shop.Application.ViewModels.RequestDTOs.BlogDto
{
    public class AddBlogDto
    {
        [Required(ErrorMessage = "Tiêu đề bài viết là bắt buộc")]
        [StringLength(250, ErrorMessage = "Tiêu đề không được dài hơn 250 ký tự")]
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;
        [StringLength(250, ErrorMessage = "Mô tả không được dài hơn 250 ký tự")]
        public string Description { get; set; } = string.Empty;

        [AllowHtml]
        public string Content { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "Tiêu đề SEO không được dài hơn 250 ký tự")]
        public string SeoTitle { get; set; } = string.Empty;

        [StringLength(250, ErrorMessage = "Mô tả SEO không được dài hơn 160 ký tự")]
        public string SeoDescription { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Từ khóa SEO không được dài hơn 100 ký tự")]
        public string SeoKeyworks { get; set; } = string.Empty;
    }
}
