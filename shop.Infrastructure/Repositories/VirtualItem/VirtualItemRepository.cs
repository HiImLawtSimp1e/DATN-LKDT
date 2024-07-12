using Microsoft.EntityFrameworkCore;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;
using  LinqKit;
using shop.Infrastructure.Utilities;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Xml;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace shop.Infrastructure.Repositories.VirtualItem
{
    public class VirtualItemRepository : IVirtualItemRepository
    {
        private readonly AppDbContext _dbContext;
        public VirtualItemRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task<VirtualItemEntity> DeleteAsync(Guid Id)
        {
            var res = await DeleteAsync(new List<Guid> { Id });
            return res.FirstOrDefault();
        }

        public async Task<List<VirtualItemEntity>> DeleteAsync(List<Guid> deleteIds)
        {
            var lstExist = new List<VirtualItemEntity>();
            foreach (var id in deleteIds)
            {
                var exist = await FindAsync( id);
                if (exist == null)
                    continue;
                exist.Isdeleted= true;
                _dbContext.VirtualItems.Update(exist);
                lstExist.Add(exist);
            }

            await _dbContext.SaveChangesAsync();
            return lstExist;
        }

        public async Task<VirtualItemEntity> FindAsync(Guid Id)
        {
            var x = _dbContext.Database.GetConnectionString();
            var result = await _dbContext.VirtualItems.AsNoTracking().FirstOrDefaultAsync(x =>x.Id == Id&& x.Isdeleted!=false);
            if (result == null)
                throw new ArgumentException(IVirtualItemRepository.Message_VirtualItemNotFound);
            return result;
        }

        public async Task<Pagination<VirtualItemEntity>> GetAllAsync(VirtualItemQueryModel virtualItemQueryModel)
        {
            var x = _dbContext.Database.GetDbConnection();
            IQueryable<VirtualItemEntity> query =  BuildQuery(virtualItemQueryModel);

            var sortExpression = string.Empty;
            if (string.IsNullOrWhiteSpace(virtualItemQueryModel.Sort) || virtualItemQueryModel.Sort.Equals("-LastModifiedOnDate"))
            {
                query = query.OrderByDescending(x => x.LastModifiedOnDate);
            }
            else
            {
                sortExpression = virtualItemQueryModel.Sort;
            }

            var result = await query.GetPagedAsync(virtualItemQueryModel.CurrentPage.Value, virtualItemQueryModel.PageSize.Value, sortExpression);
            return result;
        }

        public async Task<List<VirtualItemEntity>> ListAllAsync(VirtualItemQueryModel virtualItemQueryModel)
        {
            IQueryable<VirtualItemEntity> query = BuildQuery(virtualItemQueryModel);

            var sortExpression = string.Empty;
            if (string.IsNullOrWhiteSpace(virtualItemQueryModel.Sort) || virtualItemQueryModel.Sort.Equals("-LastModifiedOnDate"))
            {
                query = query.OrderByDescending(x => x.LastModifiedOnDate);
            }
            else
            {
                query = query.ApplySorting(virtualItemQueryModel.Sort);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<VirtualItemEntity> SaveAsync(VirtualItemEntity virtualItemEntity)
        {
            var res = await SaveAsync(new List<VirtualItemEntity> { virtualItemEntity });
            return res.FirstOrDefault();
        }

        public async Task<List<VirtualItemEntity>> SaveAsync(List<VirtualItemEntity> virtualItemEntities)
        {
                var updated = new List<VirtualItemEntity>();
            foreach(var e in virtualItemEntities)
            {
                VirtualItemEntity exist = null;
                if (e.Id==Guid.Empty)
                {
                    e.Id = Guid.NewGuid();
                }
                else
                {
                    exist = await FindAsync(e.Id);
                }

                if(exist == null) {
                _dbContext.VirtualItems.Add(e);
                    updated.Add(e);
                }
                else
                {
                    exist.Status = e.Status;
                    exist.Ispublish=e.Ispublish;
                    exist.Metadata = e.Metadata;
                    exist.Code=e.Code;
                    exist.Name = e.Name;
                    exist.Decription = e.Decription;
                    exist.ParenId=e.ParenId;
                    exist.VirtualType=e.VirtualType;
                    exist.ImgUrl=e.ImgUrl;
                    exist.Decription=e.Decription;

                    _dbContext.VirtualItems.Update(exist);
                    updated.Add(exist);
                }

                await _dbContext.SaveChangesAsync();
              
            }
            return updated;
        }

        protected virtual IQueryable<VirtualItemEntity> BuildQuery( VirtualItemQueryModel queryModel)
        {
            IQueryable<VirtualItemEntity> query;
            query= _dbContext.VirtualItems.AsNoTracking().Where(x=>x.Isdeleted!=false);

            if (queryModel.ListTextSearch!=null)
            {
                var predicate = PredicateBuilder.New<VirtualItemEntity>();
                foreach (var ts in queryModel.ListTextSearch)
                {
                    predicate = predicate.Or(q => q.Name.Contains(ts) || q.Code.Contains(ts));
                }
                query = query.Where(predicate);
            }
            if (!string.IsNullOrWhiteSpace(queryModel.FullTextSearch))
            {
                var ts = queryModel.FullTextSearch;
                query = query.Where(q => q.Name.Contains(ts) || q.Code.Contains(ts) );
            }
            if (queryModel.Id != null)
            {
                query=query.Where(x => x.Id==queryModel.Id);
            }
            if(queryModel.ListId!=null&& !queryModel.ListId.Any())
            {
                query=query.Where(x=> queryModel.ListId.Contains(x.Id));
            }
            if (!string.IsNullOrEmpty(queryModel.Type))
            {
                query=query.Where(x=>x.Type==queryModel.Type);
            }
            if(!string.IsNullOrEmpty(queryModel.Status))
            {
                query=query.Where(x=>x.Status==queryModel.Status);
            }
            if(queryModel.ParenId!=null&& queryModel.ParenId!=Guid.Empty) 
            {
                query=query.Where(x => x.ParenId==queryModel.ParenId);
            }
            if (queryModel.Ispublish.HasValue&&queryModel.Ispublish.Value)
            {
                query=query.Where(x=>x.Ispublish==queryModel.Ispublish.Value);
            }
            if(queryModel.MetadataFilterQueries!=null && queryModel.MetadataFilterQueries.Any())
            {
               //TODO
            }
            return query;
        }
    }
}
