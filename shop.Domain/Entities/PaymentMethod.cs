namespace shop.Domain.Entities;
public class PaymentMethod
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Status { get; set; }
    public virtual ICollection<PaymentMethodDetail>? PaymentMethodDetails { get; set; }
}
