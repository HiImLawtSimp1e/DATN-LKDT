using shop.Domain.Entities.Base;
using System;

namespace shop.Domain.Entities;
public class Voucher : BaseEntity
{
    public Guid Id { get; set; }
    public string VoucherCode { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime StartedDate { get; set; }
    public DateTime FinishedDate { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
}
