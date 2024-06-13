using shop.Domain.Entities.Base;

namespace shop.Domain.Entities
{
    public class ReviewEntity : BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public string? Content { get; set; }

        public Guid? VirtualItemId { get; set; }

        public Guid IdAccount { get; set; }
    }
}
