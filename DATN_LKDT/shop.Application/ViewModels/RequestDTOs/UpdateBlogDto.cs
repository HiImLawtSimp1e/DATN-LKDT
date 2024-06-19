using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace shop.Application.ViewModels.RequestDTOs
{
    public class UpdateBlogDto
    {

        [Required(ErrorMessage = "Tiêu đề blog không được bỏ trống")]
        [StringLength(100, ErrorMessage = "Tiêu đề blog không được dài hơn 100 ký tự")]
        public string? Title { get; set; }
        [StringLength(100, ErrorMessage = "Intro text không được dài hơn 100 ký tự")]
        public string IntroText { get; set; }

        [Required(ErrorMessage = "Nội dung blog không được bỏ trống")]
        [AllowHtml]
        public string? Content { get; set; }

        public string? Images { get; set; }

        public int Status { get; set; }
    }
}
