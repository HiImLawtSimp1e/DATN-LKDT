namespace shop.Data.Entities;
public class ProductImage
{
    public Guid Id { get; set; }
    public string ImagePath { get; set; }
    public string Caption { get; set; }
    public bool IsDefault { get; set; }
    public int SortOrder { get; set; }

    // relationship
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
}
