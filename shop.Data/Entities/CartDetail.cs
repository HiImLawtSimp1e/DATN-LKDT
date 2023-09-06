namespace shop.Data.Entities;
public class CartDetail
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    // relationship
    public Guid CustomerId { get; set; }
    public Guid ProductDetailId { get; set; }
    public virtual Cart Cart { get; set; }
    public virtual ProductDetail ProductDetail { get; set; }
}
