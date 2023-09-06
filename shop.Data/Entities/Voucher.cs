namespace shop.Data.Entities;
public class Voucher
{
    public Guid Id { get; set; }
    public string VoucherCode { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime StartedDate { get; set; }
    public DateTime FinishedDate { get; set; }
    public int Stock { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }

    // relationship
    public virtual ICollection<Order> Orders { get; set; }
}
