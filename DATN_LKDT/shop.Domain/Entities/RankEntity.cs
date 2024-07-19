using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Domain.Entities
{
    public class RankEntity
    {
        public Guid Id { get; set; }
        public int STT { get; set; }

        public string Username { get; set; }

        public string IdAccount { get; set; }

        public int? Point { get; set; }
        public int? TotalPoint { get; set; }
        public string? Ranking { get; set; }

        public DateTime? DateRank { get; set; }

        public int Status { get; set; }
        public int? Policies { get; set; }
        public int? Benefits { get; set; }
        [NotMapped]
        public int? Days { get; set; }
    }
}
