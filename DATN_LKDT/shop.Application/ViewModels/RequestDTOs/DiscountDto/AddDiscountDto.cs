using Microsoft.EntityFrameworkCore.Metadata.Internal;
using shop.Application.Common.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.RequestDTOs.DiscountDto
{
    public class AddDiscountDto
    {
        [Required(ErrorMessage = "Không được bỏ trống mã giảm giá")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Mã giảm giá phải từ 2 đến 25 ký tự")]
        public string Code { get; set; } = string.Empty;
        [Required(ErrorMessage = "Không được bỏ trống tên voucher")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Độ dài tên voucher từ 2 đến 50 ký tự")]
        public string VoucherName { get; set; } = string.Empty;
        public bool IsDiscountPercent { get; set; } = false;
        [Required(ErrorMessage = "Giá trị giảm giá không được bỏ trống")]
        [Range(0.00, (double)decimal.MaxValue, ErrorMessage = "Giá trị giảm giá không được âm")]
        [Column(TypeName = "decimal(18,2)")]
        public double DiscountValue { get; set; } = 0.00;
        [Range(0, int.MaxValue, ErrorMessage = "Giá trị tối thiểu của đơn hàng được áp dụng voucher phải là số nguyên dương")]
        public int MinOrderCondition { get; set; } = 0;
        [Range(0, int.MaxValue, ErrorMessage = "Giá trị giảm giá tối đa không được bỏ trống")]
        public int MaxDiscountValue { get; set; } = 0;
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không được âm")]
        public int Quantity { get; set; } = 1000;
        public DateTime StartDate { get; set; } = DateTime.Now;
        [DateGreaterThan("StartDate", ErrorMessage = "Ngày hết hạn voucher không được sớm hơn ngày bắt đầu áp dụng voucher")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(30);
    }
}
