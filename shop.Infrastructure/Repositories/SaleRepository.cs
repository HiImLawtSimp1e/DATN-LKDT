
﻿using Microsoft.EntityFrameworkCore;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;


using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        public readonly AppDbContext _appDbContext;

        public SaleRepository()
        {
            _appDbContext = new AppDbContext();
        }
        public async Task<SalesEntity> Add(SalesEntity obj)
        {
            obj.Id = new Guid();
            await _appDbContext.Sales.AddAsync(obj);
            await _appDbContext.SaveChangesAsync();
            return obj;
        }

        public async Task Delete(Guid id)
        {
            var a = await _appDbContext.Sales.FindAsync(id);
            _appDbContext.Sales.Remove(a);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<SalesEntity>> GetAll()
        {
            return await _appDbContext.Sales.ToListAsync();
        }

        public async Task<SalesEntity> Update(SalesEntity obj)
        {
            var a = await _appDbContext.Sales.FindAsync(obj.Id);
            a.Status = obj.Status;
            a.FromDate = obj.FromDate;
            a.Note = obj.Note;
            a.ToDate = obj.ToDate;
            a.ReducedAmount = obj.ReducedAmount;
            _appDbContext.Sales.Update(a);
            await _appDbContext.SaveChangesAsync();
            return obj;
        }
    }
}
