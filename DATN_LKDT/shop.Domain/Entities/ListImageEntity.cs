namespace shop.Domain.Entities
{
    public class ListImageEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Image { get; set; }

        public Guid? VirtualItemId { get; set; }

        public Guid? IdColor { get; set; } = Guid.NewGuid();


    }
}
