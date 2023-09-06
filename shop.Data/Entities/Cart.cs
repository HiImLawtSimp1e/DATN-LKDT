namespace shop.Data.Entities;
public class Cart
{
    public DateTime CreatedDate { get; set; }

    // relationship
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual ICollection<CartDetail> CartDetails { get; set; }
}
