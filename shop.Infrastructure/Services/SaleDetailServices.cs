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
    public class SaleDetailServices : ISaleDetailServices
    {
        private AppDbContext _appDbContext;
        public SaleDetailServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public SaleDetaildEntity Add(SaleDetaildEntity obj)
        {
            var data = new SaleDetaildEntity();
            try
            {
                _appDbContext.SaleDetaild.Add(data);
                _appDbContext.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return data;
        }

        public SaleDetaildEntity Delete(Guid id)
        {
            var data = _appDbContext.SaleDetaild.FirstOrDefault(c => c.Id == id);
            try
            {
                _appDbContext.SaleDetaild.Remove(data);
                _appDbContext.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return data;
        }

        public SaleDetaildEntity Details(Guid id)
        {
            return _appDbContext.SaleDetaild.FirstOrDefault(c => c.Id == id);
        }

        public SaleDetaildEntity Update(SaleDetaildEntity obj)
        {
            var data = _appDbContext.SaleDetaild.FirstOrDefault(c => c.Id == obj.Id);
            try
            {

                _appDbContext.SaleDetaild.Update(data);
                _appDbContext.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return data;
        }
    }
}
