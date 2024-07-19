using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Model.Common.Pagination
{
    public class PaginationRequest
    {
        public virtual string Sort { get; set; } = "+Id";
        public string? Fields { get; set; }
        [Range(1, int.MaxValue)]
        public int? CurrentPage { get; set; } = 1;
        [Range(1, int.MaxValue)]
        public int? PageSize { get; set; } = 20;
        public string? Filter { get; set; } = "{}";
        public string? FullTextSearch { get; set; }
        public Guid? Id { get; set; }
        public IEnumerable<Guid>? ListId { get; set; }
        public IEnumerable<string> ?ListTextSearch { get; set; }
    }
}
