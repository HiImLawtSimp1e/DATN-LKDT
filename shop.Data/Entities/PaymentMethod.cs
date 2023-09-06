namespace shop.Data.Entities;
public class PaymentMethod
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Status { get; set; }

    // relationship
    public virtual ICollection<PaymentMethodDetail> PaymentMethodDetails { get; set; }
}
