using shop.Domain.Entities;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Business.Rank
{
    public interface IRankBusiness
    {
        /*public Task<Pagination<VirtualItemEntity>> GetAllAsync(VirtualItemQueryModel virtualItemQueryModel);
        public Task<List<VirtualItemEntity>> ListAllAsync(VirtualItemQueryModel virtualItemQueryModel);*/
        public Task<RankEntity> SaveAsync(RankEntity rankEntity);
        public Task<RankEntity> PatchAsync(RankEntity rankEntity);
        public Task<List<RankEntity>> SaveAsync(List<RankEntity> rankEntity);
        public Task<RankEntity> FindAsync(Guid Id);
        public Task<RankEntity> DeleteAsync(Guid Id);
        public Task<List<RankEntity>> DeleteAsync(List<Guid> Id);
    }
}
