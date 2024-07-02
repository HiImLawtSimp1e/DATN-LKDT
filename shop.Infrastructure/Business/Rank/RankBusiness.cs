using shop.Domain.Entities;
using shop.Infrastructure.Intercepter;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Repositories.Rank;
using shop.Infrastructure.Repositories.VirtualItem;
using shop.Infrastructure.Utilities;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace shop.Infrastructure.Business.Rank
{
    public class RankBusiness : IRankBusiness
    {
        private readonly IRankRepository _rankRepository;
        public RankBusiness(IRankRepository rankRepository)
        {
            _rankRepository = rankRepository;
        }

        public async Task<RankEntity> DeleteAsync(Guid Id)
        {
            var res = await DeleteAsync(new List<Guid> { Id });
            return res.FirstOrDefault();
        }

        public async Task<List<RankEntity>> DeleteAsync(List<Guid> ids)
        {
            var res = await _rankRepository.DeleteAsync(ids);
            return res;
        }

        public async Task<RankEntity> FindAsync(Guid Id)
        {
            var res = await _rankRepository.FindAsync(Id);
            return res;
        }

        public async Task<RankEntity> PatchAsync(RankEntity rankEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<RankEntity> SaveAsync(RankEntity rankEntity)
        {
            var res = await SaveAsync(new List<RankEntity> { rankEntity });
            return res.FirstOrDefault();
        }

        public async Task<List<RankEntity>> SaveAsync(List<RankEntity> rankEntity)
        {
            var oldRankItems = new List<RankEntity>();
            foreach (var item in rankEntity)
            {
                if (item.Id != Guid.Empty)
                {
                    var oldRankItem = await FindAsync(item.Id);
                    oldRankItems.Add(oldRankItem);
                }
            }
            var result = await _rankRepository.SaveAsync(rankEntity);
            foreach (var virtualItem in result)
            {
                foreach (var intercepter in _virtualItemAfterSaveIntercepter.OrderBy(x => x.Order))
                {
                    var oldObj = oldRankItems.FirstOrDefault(x => x.Id == virtualItem.Id);
                    await intercepter.Intercept(oldObj, virtualItem);
                }
            }
            return result;
        }
    }
}
