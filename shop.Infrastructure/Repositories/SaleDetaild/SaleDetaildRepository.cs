using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Repositories.SaleDetaild
{
    public class SaleDetaildRepository : ISaleDetaildRepository
    {
        public readonly AppDbContext _appDbContext;

        public SaleDetaildRepository()
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

        public async Task Delete(Guid id)
        {
            var a = await _appDbContext.SaleDetaild.FindAsync(id);
            _appDbContext.SaleDetaild.Remove(a);
            await _appDbContext.SaveChangesAsync();
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
