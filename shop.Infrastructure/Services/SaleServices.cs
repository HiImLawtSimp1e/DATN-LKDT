using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using shop.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Services
{
    public class SaleServices : ISaleServices
    {
        private AppDbContext _appDbContext;

        public SaleServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public SalesEntity Add(SalesEntity obj)
        {
            var data = new SalesEntity();
            try
            {

                _appDbContext.Sales.Add(data);
                _appDbContext.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return data;
        }

        public SalesEntity Delete(Guid id)
        {
            var data = _appDbContext.Sales.FirstOrDefault(c => c.Id == id);
            try
            {
                _appDbContext.Sales.Remove(data);
                _appDbContext.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return data;
        }

        public SalesEntity Details(Guid id)
        {
            return _appDbContext.Sales.FirstOrDefault(c => c.Id == id);
        }

        public SalesEntity Update(SalesEntity obj)
        {

            var data = _appDbContext.Sales.FirstOrDefault(c => c.Id == obj.Id);
            try
            {

                _appDbContext.Sales.Update(data);
                _appDbContext.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return data;
        }
    }
}
