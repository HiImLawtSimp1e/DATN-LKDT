using shop.Domain.Entities.Base;
using System;

namespace shop.Domain.Entities;
public class PaymentMethod : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public int Status { get; set; }
    public virtual ICollection<PaymentMethodDetail>? PaymentMethodDetails { get; set; }
}
