namespace shop.Domain.Entities;
public class Size
{
    public Guid Id { get; set; }
    public int SizeNumber { get; set; }
    public virtual ICollection<ProductDetail>? ProductDetails { get; set; }
}
