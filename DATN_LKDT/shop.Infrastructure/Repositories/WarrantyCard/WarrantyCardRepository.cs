using Microsoft.EntityFrameworkCore;
using shop.Domain.Entities;
using shop.Infrastructure.Database.Context;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace shop.Infrastructure.Repositories.WarrantyCard
{
    public class WarrantyCardRepository : IWarrantyCardRepository
    {
        private readonly AppDbContext _dbContext;

        public WarrantyCardRepository(AppDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<WarrantyCardEntity> DeleteAsync(Guid Id)
        {
            var res = await DeleteAsync(new List<Guid> { Id });
            return res.FirstOrDefault();
        }

        public async Task<List<WarrantyCardEntity>> DeleteAsync(List<Guid> Ids)
        {
            var res = new List<WarrantyCardEntity>();
          foreach(var id in Ids)
            {
                var exist = await FindAsync(id);
                if (exist != null)
                {
                    res.Add(exist);
                    _dbContext.WarrantyCards.DeleteByKey(exist);
                }
            }
          await _dbContext.SaveChangesAsync();
            return res;
        }

        public async Task<WarrantyCardEntity> FindAsync(Guid Id)
        {
            var res = await _dbContext.WarrantyCards.FirstOrDefaultAsync(x=>x.Id==Id);
            if (res==null)
                throw new Exception(IWarrantyCardRepository.NotFound);
            return res;
        }

        public async Task<Pagination<WarrantyCardEntity>> GetAllAsync(WarrantyCardQueryModel warrantyCardQueryModel)
        {
            IQueryable<WarrantyCardEntity> query = BuildQuery(warrantyCardQueryModel);

            var sortExpression = string.Empty;
            if (string.IsNullOrWhiteSpace(warrantyCardQueryModel.Sort) || warrantyCardQueryModel.Sort.Equals("-LastModifiedOnDate"))
            {
                query = query.OrderByDescending(x => x.LastModifiedOnDate);
            }
            else
            {
                sortExpression = warrantyCardQueryModel.Sort;
            }

            var result = await query.GetPagedAsync(warrantyCardQueryModel.CurrentPage.Value, warrantyCardQueryModel.PageSize.Value, sortExpression);
            return result;
        }
        public IQueryable<WarrantyCardEntity> BuildQuery (WarrantyCardQueryModel queryModel)
        {
            IQueryable<WarrantyCardEntity> query= _dbContext.WarrantyCards.Where(x=>x.Isdelete!=false);

            if (queryModel.IdVirtuall.HasValue)
            {
                query=query.Where(x=>x.IdVirtualItem==queryModel.IdVirtuall.Value);
            }

            if (queryModel.IdBillDetail.HasValue)
            {
                query=query.Where(x => x.IdBillDetail==queryModel.IdBillDetail.Value);
            }

            if (queryModel.ListId!=null &&queryModel.ListId.Any())
            {
                query=query.Where(x => queryModel.ListId.Contains(x.Id));
            }
            if (queryModel.ExpirationDate.HasValue)
            {
                query=query.Where(x=>x.ExpirationDate==queryModel.ExpirationDate.Value);
            }

            if (!string.IsNullOrEmpty(queryModel.Type))
            {
                query=query.Where(x => x.Type==queryModel.Type);
            }

            return query;  

        }
        public async Task<List<WarrantyCardEntity>> ListAllAsync(WarrantyCardQueryModel warrantyCardQueryModel)
        {
            IQueryable<WarrantyCardEntity> query = BuildQuery(warrantyCardQueryModel);

            var sortExpression = string.Empty;
            if (string.IsNullOrWhiteSpace(warrantyCardQueryModel.Sort) || warrantyCardQueryModel.Sort.Equals("-LastModifiedOnDate"))
            {
                query = query.OrderByDescending(x => x.LastModifiedOnDate);
            }
            else
            {
                query = query.ApplySorting(warrantyCardQueryModel.Sort);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<WarrantyCardEntity> SaveAsync(WarrantyCardEntity warrantyCardEntity)
        {
            var res = await SaveAsync(new List<WarrantyCardEntity> { warrantyCardEntity });
            return res.FirstOrDefault();
        }

        public async Task<List<WarrantyCardEntity>> SaveAsync(List<WarrantyCardEntity> warrantyCardEntities)
        {
            var updated = new List<WarrantyCardEntity>();
            foreach (var e in warrantyCardEntities)
            {
                WarrantyCardEntity exist = null;
                if (e.Id==Guid.Empty)
                {
                    e.Id=Guid.NewGuid();
                }
                else
                {
                    exist= await FindAsync(e.Id);
                }

                if (exist == null)
                {
                    _dbContext.WarrantyCards.Add(e);
                    updated.Add(e);
                }
                else
                {
                    exist.Status = e.Status;
                    exist.IdBillDetail=e.IdBillDetail;
                    exist.IdBillDetail = e.IdBillDetail;
                    exist.Type=e.Type;
                    exist.Status = e.Status;
                    exist.Description = e.Description;
                    exist.Metadata=e.Metadata;
                    exist.ExpirationDate=e.ExpirationDate;
 
                    _dbContext.WarrantyCards.Update(exist);
                    updated.Add(exist);
                }
                await _dbContext.SaveChangesAsync();
            }
            return updated;
        }
    }
}
