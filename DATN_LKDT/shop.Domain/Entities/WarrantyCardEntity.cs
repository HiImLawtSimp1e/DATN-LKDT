namespace shop.Domain.Entities
{
    public class WarrantyCardEntity
    {
        public Guid Id { get; set; }

        public Guid? IdBillDetail { get; set; }

        public Guid? AccountId { get; set; }

        public Guid? IdPhoneDetail { get; set; }

        public Guid? IdPhone { get; set; }
        public string Imei { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? Description { get; set; }

        public DateTime? ThoiGianConBaoHanh { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public int? Status { get; set; }

    }
}
