using shop.Infrastructure.Model.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Model
{
    public class VirtualItemObjRelationQueryModel :PaginationRequest
    {
        public string? RelationType { get; set; }
        public Guid? VirtualItemId { get; set; }
        public Guid? ObjectId { get; set; }
    }
}
