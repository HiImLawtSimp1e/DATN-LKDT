namespace shop.Domain.Entities
{
    public class BlogEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Title { get; set; }
        public string IntroText { get; set; }

        public string? Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? Images { get; set; }

        public int Status { get; set; }
    }
}
