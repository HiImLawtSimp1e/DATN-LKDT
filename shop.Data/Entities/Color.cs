namespace shop.Data.Entities;
public class Color
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    // relationship
    public virtual ICollection<ProductDetail> ProductDetails { get; set; }
}
