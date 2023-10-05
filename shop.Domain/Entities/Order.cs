using shop.Domain.Entities.Base;

namespace shop.Domain.Entities;
public class Order : BaseEntity
{
    public Guid Id { get; set; }
    public string OrderCode { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ConfirmedDate { get; set; }
    public DateTime? PaidDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string ShipName { get; set; }
    public string ShipAddress { get; set; }
    public string ShipPhoneNumber { get; set; }
    public decimal Total { get; set; }
    public int Status { get; set; }
    public Guid VoucherId { get; set; }
    public Guid StaffId { get; set; }
    public virtual Voucher? Voucher { get; set; }
    public virtual Staff? Staff { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
    public ICollection<UsePointHistory>? UsePointsHistories { get; set; }
    public ICollection<PaymentMethodDetail>? PaymentMethodDetails { get; set; }
}
