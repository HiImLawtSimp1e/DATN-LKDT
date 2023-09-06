namespace shop.Data.Entities;
public class Promotion
{
    public Guid Id { get; set; }
    public string PromotionCode { get; set; }
    public DateTime StartedDate { get; set; }
    public DateTime FinishedDate { get; set; }
    public int? DiscountPercent { get; set; }
    public decimal? DiscountAmount { get; set; }
    public int Status { get; set; }

    // relationship
    public virtual ICollection<ProductDetail> ProductDetails { get; set; }
}
