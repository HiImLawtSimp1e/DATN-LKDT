namespace shop.Data.Entities;
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }

    // relationship
    public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    public virtual ICollection<ProductImage> ProductImages { get; set; }
    public virtual ICollection<ProductInCategory> ProductInCategories { get; set; }
}
