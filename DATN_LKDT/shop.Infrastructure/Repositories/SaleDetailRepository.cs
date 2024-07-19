using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using shop.Infrastructure.Repositories.SaleDetaild;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Repositories
{
    public class SaleDetailRepository : ISaleDetailRepository
    {
        public readonly AppDbContext _appDbContext;
        public SaleDetailRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<SaleDetaildEntity> Add(SaleDetaildEntity obj)
        {
            obj.Id = new Guid();
            await _appDbContext.SaleDetaild.AddAsync(obj);
            await _appDbContext.SaveChangesAsync();
            return obj;
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

        public async Task<SaleDetaildEntity> Update(SaleDetaildEntity obj)
        {
            var a = await _appDbContext.SaleDetaild.FindAsync(obj.Id);
            a.Status = obj.Status;
            _appDbContext.SaleDetaild.Update(a);
            await _appDbContext.SaveChangesAsync();
            return obj;
        }
    }
}
