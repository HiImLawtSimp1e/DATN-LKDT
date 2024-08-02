using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using shop.Domain.Entities.Base;

namespace shop.Domain.Entities
{
    public class DiscountEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string VoucherName { get; set; } = string.Empty;
        public bool IsDiscountPercent { get; set; } = false;
        [Column(TypeName = "decimal(18,2)")]

        public double DiscountValue { get; set; } = 0.00;
        public int MinOrderCondition { get; set; } = 0;
        public int MaxDiscountValue { get; set; } = 0;
        public int Quantity { get; set; } = 1000;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(30);
        public bool IsActive { get; set; } = true;
    }
}
