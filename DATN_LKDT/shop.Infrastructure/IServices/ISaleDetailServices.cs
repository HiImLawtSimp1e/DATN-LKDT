using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.IServices
{
    public interface ISaleDetailServices
    {
        public SaleDetaildEntity Add(SaleDetaildEntity obj);
        public SaleDetaildEntity Update(SaleDetaildEntity obj);
        public SaleDetaildEntity Delete(Guid id);
        public SaleDetaildEntity Details(Guid id);
    }
}
