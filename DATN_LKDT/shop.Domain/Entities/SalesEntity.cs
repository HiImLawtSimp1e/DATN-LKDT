using shop.Domain.Entities.Base;

namespace shop.Domain.Entities
{
    public class SalesEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public decimal ReducedAmount { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string? Note { get; set; }

        public int Status { get; set; }


    }
}
