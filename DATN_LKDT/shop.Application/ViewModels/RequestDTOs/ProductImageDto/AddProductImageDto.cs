using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs.ProductImageDto
{
    public class AddProductImageDto
    {
        [Required(ErrorMessage = "Bạn chưa thêm hình ảnh")]
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsMain { get; set; }
        public Guid ProductId { get; set; }
    }
}
