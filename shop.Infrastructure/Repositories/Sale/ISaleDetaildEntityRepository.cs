using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Repositories.Sale
{
    public interface ISaleDetaildEntityRepository
    {
        Task<bool> Add(Guid idsale, Guid iddetaild);
        Task<bool> Update(Guid id, Guid idsale, Guid iddetaild);
        Task<List<SaleDetaildEntity>> GetAll();
        Task Delete(Guid id);
    }
}
