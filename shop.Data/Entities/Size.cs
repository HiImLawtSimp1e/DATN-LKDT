namespace shop.Data.Entities;
public class Size
{
    public Guid Id { get; set; }
    public int SizeNumber { get; set; }

    // relationship
    public virtual ICollection<ProductDetail> ProductDetails { get; set; }
}
