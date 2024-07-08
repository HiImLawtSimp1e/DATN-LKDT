using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.IServices
{
    public interface IBillDetailServices
    {
        public BillDetailsEntity Add(BillDetailsEntity obj);
        public BillDetailsEntity Update(BillDetailsEntity obj);
        public BillDetailsEntity Delete(Guid id);
        public BillDetailsEntity Details(Guid id);
    }
}
