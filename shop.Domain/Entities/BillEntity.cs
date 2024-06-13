namespace shop.Domain.Entities
{
    public class BillEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime? CreatedTime { get; set; }

        public DateTime? PaymentDate { get; set; }


        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public int? Status { get; set; }
        public string? BillCode { get; set; }
        public string? deliveryPaymentMethod { get; set; }
        public decimal? TotalMoney { get; set; }

        public DateTime? Update_at { get; set; }
        public int StatusPayment { get; set; }
        public string? Note { get; set; }
        public Guid? IdAccount { get; set; }

        public virtual List<BillDetailsEntity> BillDetails { get; set; }
        public virtual AccountEntity Accounts { get; set; }

    }
}
