using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Repositories.VirtualItem;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Repositories.Discount
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly AppDbContext _dbContext;
        public DiscountRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<DiscountEntity> DeleteAsync(Guid Id)
        {
            var res = await DeleteAsync(new List<Guid> { Id });
            return res.FirstOrDefault();
        }

        public async Task<List<DiscountEntity>> DeleteAsync(List<Guid> deleteIds)
        {
            var lstExist = new List<DiscountEntity>();
            foreach (var id in deleteIds)
            {
                var exist = await FindAsync(id);
                if (exist == null)
                    continue;
                _dbContext.Discount.Update(exist);
                lstExist.Add(exist);
            }

            await _dbContext.SaveChangesAsync();
            return lstExist;
        }

        public async Task<DiscountEntity> FindAsync(Guid Id)
        {
            var result = await _dbContext.Discount.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id );
            if (result == null)
                throw new ArgumentException(IVirtualItemRepository.Message_VirtualItemNotFound);
            return result;
        }

        public Task<Pagination<DiscountEntity>> GetAllAsync(DiscountQueryModel discountQueryModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<DiscountEntity>> ListAllAsync(DiscountQueryModel discountQueryModel)
        {
            throw new NotImplementedException();
        }

        public async Task<DiscountEntity> SaveAsync(DiscountEntity discountEntity)
        {
            var res = await SaveAsync(new List<DiscountEntity> { discountEntity });
            return res.FirstOrDefault();
        }

        public async Task<List<DiscountEntity>> SaveAsync(List<DiscountEntity> discountEntity)
        {
            var updated = new List<DiscountEntity>();
            foreach (var e in discountEntity)
            {
                DiscountEntity exist = null;
                if (e.Id == Guid.Empty)
                {
                    e.Id = Guid.NewGuid();
                }
                else
                {
                    exist = await FindAsync(e.Id);
                }

                if (exist == null)
                {
                    _dbContext.Discount.Add(e);
                    updated.Add(e);
                }
                else
                {
                    exist.MaVoucher = e.MaVoucher;
                    exist.NameVoucher = e.NameVoucher;
                    exist.DieuKien = e.DieuKien;
                    exist.TypeVoucher = e.TypeVoucher;
                    exist.Quantity = e.Quantity;
                    exist.MucUuDai = e.MucUuDai;
                    exist.DateStart = e.DateStart;
                    exist.DateEnd = e.DateEnd;
                    exist.StatusVoucher = e.StatusVoucher;
                    
                    _dbContext.Discount.Update(exist);
                    updated.Add(exist);
                }

                await _dbContext.SaveChangesAsync();

            }
            return updated;
        }
    }
}
