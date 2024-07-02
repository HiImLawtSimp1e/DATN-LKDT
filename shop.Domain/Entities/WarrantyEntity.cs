using shop.Domain.Entities.Base;

namespace shop.Domain.Entities
{
    public class WarrantyEntity:BaseEntity
    {
        public Guid Id { get; set; }

        public int TimeWarranty { get; set; }

        public string? Description { get; set; }

        public int? Status { get; set; }
    }
}
