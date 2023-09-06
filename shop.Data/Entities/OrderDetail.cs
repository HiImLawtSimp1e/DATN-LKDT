namespace shop.Data.Entities;
public class OrderDetail
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int Status { get; set; }

    // relationship
    public Guid OrderId { get; set; }
    public Guid ProductDetailId { get; set; }
    public virtual Order Order { get; set; }
    public virtual ProductDetail ProductDetail { get; set; }
}
