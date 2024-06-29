using Microsoft.EntityFrameworkCore;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Utilities;

namespace shop.Infrastructure.Repositories
{
    public class VirtualItemObjRelationRepository : IVirtualItemObjRelationRepository
    {
        private readonly AppDbContext _dbContext;

        public VirtualItemObjRelationRepository(AppDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<VirtualItemObjRelationEntity> DeleteAsync(Guid Id)
        {
            var res = await DeleteAsync(new List<Guid> { Id });
            return res.FirstOrDefault();
        }

        public async Task<List<VirtualItemObjRelationEntity>> DeleteAsync(List<Guid> Ids)
        {
            var lstExist = new List<VirtualItemObjRelationEntity>();
            foreach (var id in Ids)
            {
                var exist = await FindAsync(id);
                if (exist == null)
                    continue;
                exist.Isdeleted= true;
                _dbContext.ItemObjRelationEntities.Update(exist);
                lstExist.Add(exist);
            }

            await _dbContext.SaveChangesAsync();
            return lstExist;
        }

        public async Task<VirtualItemObjRelationEntity> FindAsync(Guid Id)
        {
            var res =await  _dbContext.ItemObjRelationEntities.FindAsync(Id);
            if (res == null)
                throw new Exception(IVirtualItemObjRelationRepository.VirtualItemObjRelationEntity_NotFound);
            return res;
        }

        public async Task<List<VirtualItemObjRelationEntity>> FindByVirtualItemAsync(Guid VirtualItemId)
        {
            var res = await _dbContext.ItemObjRelationEntities.Where(x=>x.VirtualItemId==VirtualItemId).ToListAsync();
            return res;
        }

        public async Task<Pagination<VirtualItemObjRelationEntity>> GetAllAsync(VirtualItemObjRelationQueryModel queryModel)
        {

            var query = await BuildQuery(queryModel);
            var sortExpression = string.Empty;
            if (string.IsNullOrWhiteSpace(queryModel.Sort) || queryModel.Sort.Equals("-LastModifiedOnDate"))
            {
                query = query.OrderByDescending(x => x.LastModifiedOnDate);
            }
            else
            {
                sortExpression = queryModel.Sort;
            }

            var result = await query.GetPagedAsync(queryModel.CurrentPage.Value, queryModel.PageSize.Value, sortExpression);
            return result;
        }

        private async Task<IQueryable<VirtualItemObjRelationEntity>>  BuildQuery(VirtualItemObjRelationQueryModel queryModel)
        {
            IQueryable<VirtualItemObjRelationEntity> query = _dbContext.ItemObjRelationEntities.Where(x => x.Isdeleted==false);

            if (queryModel.ObjectId.HasValue)
            {
                query= query.Where(x => x.ObjectId.Value==queryModel.ObjectId.Value);
            }

            if (!string.IsNullOrEmpty(queryModel.RelationType))
            {
                query=query.Where(x => x.RelationType==queryModel.RelationType);
            }

            if (queryModel.VirtualItemId.HasValue)
            {
                query=query.Where(x => x.VirtualItemId==queryModel.VirtualItemId);
            }
            return query;
        }
        public async Task<List<VirtualItemObjRelationEntity>> ListAllAsync(VirtualItemObjRelationQueryModel virtualItemQueryModel)
        {
            var query = await BuildQuery(virtualItemQueryModel);
            return await query.ToListAsync();
        }

        public async Task<VirtualItemObjRelationEntity> SaveAsync(VirtualItemObjRelationEntity virtualItemObjRelationEntity)
        {
            var res = await SaveAsync(new List<VirtualItemObjRelationEntity> { virtualItemObjRelationEntity });
            return res.FirstOrDefault();
        }

        public async Task<List<VirtualItemObjRelationEntity>> SaveAsync(List<VirtualItemObjRelationEntity> virtualItemObjRelationEntities)
        {
           var updated = new List<VirtualItemObjRelationEntity>();
            foreach(var e in  virtualItemObjRelationEntities)
            {
                VirtualItemObjRelationEntity exist = null;
                if (e.Id==Guid.Empty)
                {
                    e.Id = Guid.NewGuid();
                }
                else
                {
                    exist= await FindAsync(e.Id);
                }
                if(exist==null) {
                _dbContext.ItemObjRelationEntities.Add(e);
                }
                else
                {
                    exist.RelationType= e.RelationType;
                    exist.VirtualItemId= e.VirtualItemId;
                    exist.ObjectId= e.ObjectId;
                    exist.RelatedObj=e.RelatedObj;
                    exist.RelatedObj2 = e.RelatedObj2;
                    exist.Order=e.Order;

                    _dbContext.ItemObjRelationEntities.Update(exist);
                    updated.Add(exist);
                }
                await _dbContext.SaveChangesAsync();
            }
            return updated;
        }

 
    }
}
