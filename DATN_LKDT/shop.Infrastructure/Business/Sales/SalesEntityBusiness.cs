using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Business.Sales
{
    public class SalesEntityBusiness : ISalesEntityBusiness
    {
        private readonly ISalesEntityBusiness _salesEntityBusiness;

       

        public async Task<SalesEntity> Add(SalesEntity obj)
        {
            return await _salesEntityBusiness.Add(obj);
        }

        public async Task<SalesEntity> Delete(Guid id)
        {
            return await _salesEntityBusiness.Delete(id);
        }

        public async Task<List<SalesEntity>> GetAll()
        {
            return await _salesEntityBusiness.GetAll();
        }

        public async Task<SalesEntity> Update(SalesEntity obj)
        {
            return await _salesEntityBusiness.Update(obj);
        }
    }
}
