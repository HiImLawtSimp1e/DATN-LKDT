using System.ComponentModel.DataAnnotations;
using shop.Domain.Entities.Base;

namespace shop.Domain.Entities
{
    public class DiscountEntity : BaseEntity
    {
        public Guid Id { get; set; }
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Tên voucher phải từ 2 -25 kí tự")]

        public string? MaVoucher { get; set; }
        [Required(ErrorMessage = "Tên voucher là trường bắt buộc.")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Tên voucher phải nhập từ 2-25 kí tự")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Tên voucher không được chứa ký tự đặc biệt.")]

        public string? NameVoucher { get; set; }
        [Required(ErrorMessage = "Điều kiện là trường bắt buộc")]
        [Range(0, int.MaxValue, ErrorMessage = " là số nguyên không âm")]

        public int? DieuKien { get; set; }
        [Required(ErrorMessage = "Loại khuyến mãi là bắt buộc")]
        public int? TypeVoucher { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Trường cần nhập tối đa từ 0 đến 999.999.999")]
        public int? Quantity { get; set; }

        public double? MucUuDai { get; set; }
        [Required(ErrorMessage = "Ngày bắt đầu là trường bắt buộc")]
        public DateTime DateStart { get; set; }
        [Required(ErrorMessage = "Ngày kết thúc là trường bắt buộc")]
        public DateTime DateEnd { get; set; }
        public string? StatusVoucher { get; set; }
    }
}
