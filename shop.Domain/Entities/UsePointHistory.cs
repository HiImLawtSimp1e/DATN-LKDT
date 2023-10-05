using shop.Domain.Entities.Base;

namespace shop.Domain.Entities;
public class UsePointHistory : BaseEntity
{
    public Guid Id { get; set; }
    public int UsedPoint { get; set; }
    public int Status { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ExchangePointId { get; set; }
    public Guid OrderId { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual ExchangePoint ExchangePoint { get; set; }
    public virtual Order Order { get; set; }
}
