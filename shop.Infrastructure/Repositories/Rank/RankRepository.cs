using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using shop.Infrastructure.Repositories.VirtualItem;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Repositories.Rank
{
    public class RankRepository : IRankRepository
    {
        private readonly AppDbContext _dbContext;
        public RankRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public async Task<RankEntity> DeleteAsync(Guid Id)
        {
            var res = await DeleteAsync(new List<Guid> { Id });
            return res.FirstOrDefault();
        }

        public async Task<List<RankEntity>> DeleteAsync(List<Guid> deleteIds)
        {
            var lstExist = new List<RankEntity>();
            foreach (var id in deleteIds)
            {
                var exist = await FindAsync(id);
                if (exist == null)
                    continue;
                _dbContext.Ranks.Update(exist);
                lstExist.Add(exist);
            }

            await _dbContext.SaveChangesAsync();
            return lstExist;
        }

        public async Task<RankEntity> FindAsync(Guid Id)
        {
            var result = await _dbContext.Ranks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
            if (result == null)
                throw new ArgumentException(IVirtualItemRepository.Message_VirtualItemNotFound);
            return result;
        }

        public async Task<RankEntity> SaveAsync(RankEntity rankEntities)
        {
            var res = await SaveAsync(new List<RankEntity> { rankEntities });
            return res.FirstOrDefault();
        }

        public async Task<List<RankEntity>> SaveAsync(List<RankEntity> rankEntities)
        {
            var updated = new List<RankEntity>();
            foreach (var e in rankEntities)
            {
                RankEntity exist = null;
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
                    _dbContext.Ranks.Add(e);
                    updated.Add(e);
                }
                else
                {
                    exist.STT = e.STT;
                    //exist.Username = e.;
                    //exist.IdAccount = e.;
                    //exist.Point = e.;
                    //exist.TotalPoint = e.;
                    //exist.Ranking = e.;
                    //exist.DateRank = e.;
                    //exist.Status = e.;
                    //exist.Policies = e.;
                    //exist.Benefits = e.;
                    //exist.Days =;

                    _dbContext.Ranks.Update(exist);
                    updated.Add(exist);
                }

                await _dbContext.SaveChangesAsync();

            }
            return updated;
        }
    }
}
