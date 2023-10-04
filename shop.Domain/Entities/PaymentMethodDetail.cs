namespace shop.Domain.Entities;
public class PaymentMethodDetail
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public int Status { get; set; }
    public Guid PaymentMethodId { get; set; }
    public Guid OrderId { get; set; }
    public virtual PaymentMethod? PaymentMethod { get; set; }
    public virtual Order? Order { get; set; }
}
