using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Repositories.SaleDetaild
{
    public interface ISaleDetailRepository
    {
        Task<SaleDetaildEntity> Add(SaleDetaildEntity obj);
        Task<SaleDetaildEntity> Update(SaleDetaildEntity obj);
        Task<List<SaleDetaildEntity>> GetAll();
        Task Delete(Guid id);
    }
}
