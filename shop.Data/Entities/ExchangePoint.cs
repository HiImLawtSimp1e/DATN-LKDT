namespace shop.Data.Entities;
public class ExchangePoint
{
    public Guid Id { get; set; }
    public int Point { get; set; }
    public int AddPointRatio { get; set; }
    public int UsePointRatio { get; set; }
    public int Status { get; set; }

    // relationship
    public virtual ICollection<UsePointHistory> UsePointHistories { get; set; }
}
