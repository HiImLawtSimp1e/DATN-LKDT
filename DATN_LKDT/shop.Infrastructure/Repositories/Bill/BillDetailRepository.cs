using shop.Domain.Entities;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using shop.Infrastructure.Database.Context;
using shop.Infrastructure.Repositories.Bill;

namespace AppData.Repositories
{
    public class BillDetailRepository : IBillDetailRepository
    {
        public readonly AppDbContext _dbContext;
        public BillDetailRepository()
        {
            _dbContext = new AppDbContext();
        }
        public async Task<BillDetailsEntity> Add(BillDetailsEntity obj)
        {
            BillDetailsEntity dbo = new BillDetailsEntity();
            try
            {

                _dbContext.BillDetails.Add(obj);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                dbo = null;
            }

            return dbo;
        }

        public async Task Delete(Guid id)
        {
            var a = await _dbContext.BillDetails.FindAsync(id);
            _dbContext.BillDetails.Remove(a);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<BillDetailsEntity>> GetAll()
        {
            return await _dbContext.BillDetails.ToListAsync();
        }

        public async Task<BillDetailsEntity> GetById(Guid id)
        {
            return await _dbContext.BillDetails.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<BillEntity> Update(BillEntity obj)
        {
            var a = await _dbContext.Bill.FindAsync(obj.Id);
            a.CreatedTime = obj.CreatedTime;
            a.PaymentDate = obj.PaymentDate;
            a.Name = obj.Name;
            a.Address = obj.Address;
            a.Phone = obj.Phone;
            a.Status = obj.Status;
            a.IdAccount = obj.IdAccount;
            _dbContext.Bill.Update(a);
            await _dbContext.SaveChangesAsync();
            return obj;
        }

        public Task<BillDetailsEntity> Update(BillDetailsEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
