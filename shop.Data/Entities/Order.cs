namespace shop.Data.Entities;
public class Order
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

    // relationship
    public Guid VoucherId { get; set; }
    public Guid StaffId { get; set; }
    public virtual Voucher Voucher { get; set; }
    public virtual Staff Staff { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public ICollection<UsePointHistory> UsePointsHistories { get; set; }
    public ICollection<PaymentMethodDetail> PaymentMethodDetails { get; set; }

    // thiếu khoá của user nhưng đang nối với lịch sử dùng điểm nên không biết nối thế nào :D
}
