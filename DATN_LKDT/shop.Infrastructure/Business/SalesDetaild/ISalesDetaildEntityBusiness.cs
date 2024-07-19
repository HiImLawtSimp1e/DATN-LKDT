using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Business.SalesDetaild
{
    public  interface ISalesDetaildEntityBusiness
    {
        public Task<SaleDetaildEntity> Add(Guid idsale, Guid iddetaild);
        public Task<SaleDetaildEntity> Update(Guid id, Guid idsale, Guid iddetaild);
        public Task<List<SaleDetaildEntity>> GetAll();
        public Task<SaleDetaildEntity> Delete(Guid id);
    }
}
