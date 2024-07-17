using shop.Domain.Entities.Base;
using shop.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace shop.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string InvoiceCode { get; set; } = string.Empty;
        public int TotalPrice { get; set; }
        public OrderState State { get; set; } = OrderState.Pending;
        public List<OrderItem>? OrderItems { get; set; }
        [JsonIgnore]
        public AccountEntity? Account { get; set; }
    }
}
