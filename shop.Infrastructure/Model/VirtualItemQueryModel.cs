using shop.Infrastructure.Model.Common.MetadataQueryModel;
using shop.Infrastructure.Model.Common.Pagination;

namespace shop.Infrastructure.Model
{
    public class VirtualItemQueryModel: PaginationRequest
    {
        public Guid? Code { get; set; }
        public Guid? ParenId { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? VirtualType { get; set; }
        public bool? Isdeleted { get; set; } = false;
        public bool? Ispublish { get; set; }
        public int? Solid { get; set; }
        public string? Status { get; set; }
        public List<MetadataFilterQuery>? MetadataFilterQueries { get; set; }
    }
}
