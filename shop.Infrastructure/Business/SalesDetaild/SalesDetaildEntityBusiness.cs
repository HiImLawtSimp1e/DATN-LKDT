using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Business.SalesDetaild
{
    public class SalesDetaildEntityBusiness : ISalesDetaildEntityBusiness
    {
        private readonly ISalesDetaildEntityBusiness _entityBusiness;
        public async Task<SaleDetaildEntity> Add(Guid idsale, Guid iddetaild)
        {
            return await _entityBusiness.Add(idsale, iddetaild);
        }

        public async Task<SaleDetaildEntity> Delete(Guid id)
        {
            return await _entityBusiness.Delete(id);
        }

        public async Task<List<SaleDetaildEntity>> GetAll()
        {
            return await _entityBusiness.GetAll();
        }

        public async Task<SaleDetaildEntity> Update(Guid id, Guid idsale, Guid iddetaild)
        {
            return await _entityBusiness.Update(id, idsale, iddetaild);
        }
    }
}
