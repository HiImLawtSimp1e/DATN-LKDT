using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shop.Domain.Entities;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;

namespace shop.Infrastructure.Repositories.Rank
{
    public interface IRankRepository
    {
        /*public Task<Pagination<RankEntity>> GetAllAsync(VirtualItemQueryModel virtualItemQueryModel);
        public Task<List<RankEntity>> ListAllAsync(VirtualItemQueryModel virtualItemQueryModel);*/
        public Task<RankEntity> SaveAsync(RankEntity rankEntities);
        public Task<List<RankEntity>> SaveAsync(List<RankEntity> rankEntities);
        public Task<RankEntity> FindAsync(Guid Id);
        public Task<RankEntity> DeleteAsync(Guid Id);
        public Task<List<RankEntity>> DeleteAsync(List<Guid> Id);
    }
}
