using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using shop.Infrastructure.IRepositories;
using shop.Infrastructure.Repositories.Sale;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Repositories
{
    public class SaleDetaildEntityRepository 
    {
        public readonly AppDbContext _appDbContext;

        public SaleDetaildEntityRepository()
        {
            _appDbContext = new AppDbContext();
        }
        public async Task<bool> Add(Guid idsale, Guid iddetaild)
        {
            try
            {
                SaleDetaildEntity saleDt = new SaleDetaildEntity()
                {
                    Id = Guid.NewGuid(),
                    IdSales = idsale,
                    IdVirtualItem = iddetaild
                };
                await _appDbContext.AddAsync(saleDt);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SaleDetaildEntity>> GetAll()
        {
            return await _appDbContext.SaleDetaild.ToListAsync();
        }

        public Task<bool> Update(Guid id, Guid idsale, Guid iddetaild)
        {
            throw new NotImplementedException();
        }
    }
}
