using shop.Infrastructure.Model.Common.MetadataQueryModel;
using shop.Infrastructure.Model.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Model
{
    public class WarrantyCardQueryModel: PaginationRequest
    {
        public Guid? Id { get; set; }
        public Guid? IdBillDetail { get; set; }

        public Guid? IdVirtuall { get; set; }
        
        public Guid? IdWarranty { get; set; }
        public string? Type { get;set; }

        public string? Description { get; set; }
        public List<MetadataFilterQuery>? MetadataFilterQueries { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
