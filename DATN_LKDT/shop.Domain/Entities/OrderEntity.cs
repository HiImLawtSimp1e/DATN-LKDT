using shop.Domain.Entities.Base;
using shop.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace shop.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid Id { get; set; }
        [StringLength(50)]
        public string InvoiceCode { get; set; } = string.Empty;
        public int TotalPrice { get; set; } = 0;
        public OrderState State { get; set; } = OrderState.Pending;
        [StringLength(50, MinimumLength = 6)]
        public string FullName { get; set; } = string.Empty;
        [StringLength(30, MinimumLength = 6)]
        public string Email { get; set; } = string.Empty;
        [StringLength(14)]
        public string Phone { get; set; } = string.Empty;
        [StringLength(250, MinimumLength = 6)]
        public string Address { get; set; } = string.Empty;
        public int DiscountValue { get; set; } = 0;
        public int TotalAmount { get; set; } = 0; 
        public Guid? DiscountId { get; set; }
        public DiscountEntity? Discount { get; set; }
        public Guid? AccountId { get; set; }
        public AccountEntity? Account { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public Guid PaymentMethodId { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
