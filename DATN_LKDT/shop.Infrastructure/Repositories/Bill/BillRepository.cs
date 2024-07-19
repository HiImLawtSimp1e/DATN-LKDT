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
    public class BillRepository :IBillRepository
    {
        public readonly AppDbContext _dbContext;
        public BillRepository()
        {
            _dbContext = new AppDbContext();
        }
        public async Task<BillEntity> Add(BillEntity obj)
        {
            BillEntity dbo = new BillEntity();
            try
            {
               
                _dbContext.Bill.Add(obj);
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
            var a = await _dbContext.Bill.FindAsync(id);
            _dbContext.Bill.Remove(a);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<BillEntity>> GetAll()
        {
            return await _dbContext.Bill.ToListAsync();
        }

        public async Task<BillEntity> GetById(Guid id)
        {
            return await _dbContext.Bill.FirstOrDefaultAsync(p => p.Id == id);
        }

        public BillDetailsEntity AddBillDetail(BillDetailsEntity model)
        {
            var data = new BillDetailsEntity();
            try
            {
             
                _dbContext.BillDetails.Add(data);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                data = null;
            }

            return data;
        }

        public async Task<BillEntity> Update(BillEntity obj)
        {
            var a = await _dbContext.Bill.FindAsync(obj.Id);
            a.CreatedTime = obj.CreatedTime;
            a.PaymentDate = obj.PaymentDate;
            a.Name = obj.Name;
            a.Address= obj.Address;
            a.Phone = obj.Phone;
            a.Status = obj.Status;
            a.IdAccount = obj.IdAccount;
            _dbContext.Bill.Update(a);
            await _dbContext.SaveChangesAsync();
            return obj;
        }
    }
}
