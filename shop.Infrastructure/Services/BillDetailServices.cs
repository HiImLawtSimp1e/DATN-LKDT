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
    public class BillDetailServices : IBillDetailServices
    {
        private AppDbContext _appDbContext;
        public BillDetailServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public BillDetailsEntity Add(BillDetailsEntity obj)
        {
            var data = new BillDetailsEntity();
            try
            {
                _appDbContext.BillDetails.Add(data);
                _appDbContext.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return data;
        }

        public BillDetailsEntity Delete(Guid id)
        {
            var data = _appDbContext.BillDetails.FirstOrDefault(c => c.Id == id);
            try
            {
                _appDbContext.BillDetails.Remove(data);
                _appDbContext.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return data;
        }

        public BillDetailsEntity Details(Guid id)
        {
            return _appDbContext.BillDetails.FirstOrDefault(c => c.Id == id);
        }

        public BillDetailsEntity Update(BillDetailsEntity obj)
        {
            var data = _appDbContext.BillDetails.FirstOrDefault(c => c.Id == obj.Id);
            try
            {

                _appDbContext.BillDetails.Update(data);
                _appDbContext.SaveChanges();
            }
            catch (Exception e)
            {

            }

            return data;
        }
    }
}
