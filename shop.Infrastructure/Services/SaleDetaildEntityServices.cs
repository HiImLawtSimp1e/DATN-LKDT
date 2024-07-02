using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using shop.Infrastructure.IRepositories;
using shop.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Services
{
    public class SaleDetaildEntityServices : ISaleDetaildEntityServices
    {
        private AppDbContext _appDbContext;

        public SaleDetaildEntityServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<SaleDetaildEntity> GetSaleDetailsByIdAccount(Guid id)
        {
            var lst = new List<SaleDetaildEntity>();
            try
            {
                lst = _appDbContext.SalePhoneDetailds.Where(c => c.IdSales == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return lst;
        }
    }
}
