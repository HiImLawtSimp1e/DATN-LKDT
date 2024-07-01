using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.IServices
{
    public interface ISaleServices
    {
        public SalesEntity Add(SalesEntity obj);
        public SalesEntity Update(SalesEntity obj);
        public SalesEntity Delete(Guid id);
        public SalesEntity Details(Guid id);
    }
}
