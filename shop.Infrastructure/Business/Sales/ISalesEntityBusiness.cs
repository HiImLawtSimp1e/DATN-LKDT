using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Business.Sales
{
    public interface ISalesEntityBusiness
    {
        public Task<SalesEntity> Add(SalesEntity obj);
        public Task<SalesEntity> Update(SalesEntity obj);
        public Task<List<SalesEntity>> GetAll();
        public Task<SalesEntity> Delete(Guid id);
    }
}
