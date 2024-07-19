using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Repositories.Bill
{
    public interface IBillRepository
    {
        Task<BillEntity> Add(BillEntity obj);
        Task<BillEntity> Update(BillEntity obj);
        Task<List<BillEntity>> GetAll();
        Task Delete(Guid id);

        Task<BillEntity> GetById(Guid id);
        BillDetailsEntity AddBillDetail(BillDetailsEntity model);

    }
}
