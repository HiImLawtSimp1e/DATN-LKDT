using shop.Domain.Entities.Base;
using System.Web.Mvc;

namespace shop.Domain.Entities
{
    public class BlogEntity : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Title { get; set; }
        public string IntroText { get; set; }

        [AllowHtml]
        public string? Content { get; set; }

        public string? Images { get; set; }

        public int Status { get; set; }
    }
}
