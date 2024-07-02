using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.IServices
{
    public interface ISaleDetaildEntityServices
    {
        List<SaleDetaildEntity> GetSaleDetailsByIdAccount(Guid id);
    }
}
