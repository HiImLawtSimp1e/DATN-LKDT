using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.IRepositories
{
    public interface IBillDetailRepository
    {
        Task<BillDetailsEntity> Add(BillDetailsEntity obj);
        Task<BillDetailsEntity> Update(BillDetailsEntity obj);
        Task<List<BillDetailsEntity>> GetAll();
        Task Delete(Guid id);

        Task<BillDetailsEntity> GetById(Guid id);
        
    }
}
