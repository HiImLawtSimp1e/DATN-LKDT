using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs.DiscountDto
{
    public class UpdateDiscountDto
    {
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Tên voucher phải từ 2 -25 kí tự")]

        public string Code { get; set; } = string.Empty;
        [Required(ErrorMessage = "Tên voucher là trường bắt buộc.")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Tên voucher phải nhập từ 2-25 kí tự")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Tên voucher không được chứa ký tự đặc biệt.")]

        public string? VoucherName { get; set; }
        [Required(ErrorMessage = "Loại khuyến mãi là bắt buộc")]
        public int? DiscountType { get; set; }
        [Column(TypeName = "decimal(5,2)")]

        public double DiscountValue { get; set; } = 0.00;
        [Required(ErrorMessage = "Giá trị đơn hàng tối thiểu là trường bắt buộc")]
        [Range(0, int.MaxValue, ErrorMessage = "Giá trị đơn hàng tối thiểu là số nguyên không âm")]

        public int? MinOrderValue { get; set; }
        [Required(ErrorMessage = "Số tiền giảm giá tối đa là trường bắt buộc")]
        [Range(0, int.MaxValue, ErrorMessage = "Số tiền giảm giá tối đa là số nguyên không âm")]

        public int MaxDiscount { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "Trường cần nhập só nguyên không âm")]
        public int Quantity { get; set; } = 1000;
        [Required(ErrorMessage = "Ngày bắt đầu là trường bắt buộc")]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Ngày kết thúc là trường bắt buộc")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);
        public bool IsActive { get; set; }
    }
}
