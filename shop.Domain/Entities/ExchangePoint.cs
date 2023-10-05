using shop.Domain.Entities.Base;

namespace shop.Domain.Entities;
public class ExchangePoint : BaseEntity
{
    public Guid Id { get; set; }
    public int Point { get; set; }
    public int AddPointRatio { get; set; }
    public int UsePointRatio { get; set; }
    public int Status { get; set; }
    public virtual ICollection<UsePointHistory>? UsePointHistories { get; set; }
}
