using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Repositories
{
    public interface ISaleRepository
    {
        Task<SalesEntity> Add(SalesEntity obj);
        Task<SalesEntity> Update(SalesEntity obj);
        Task<List<SalesEntity>> GetAll();
        Task Delete(Guid id);
    }
}
