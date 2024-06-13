using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shop.Domain.Entities.Base;

namespace shop.Domain.Entities
{
    public class SaleDetaildEntity : BaseEntity
    {
        public Guid Id { get; set; }

        public Guid IdSales { get; set; }

        public Guid IdVirtualItem { get; set; }

        public int Status { get; set; }

        public virtual SalesEntity? Sales { get; set; }
    }
}
