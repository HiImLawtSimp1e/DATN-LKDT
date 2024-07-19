using shop.Domain.Entities;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Repositories.WarrantyCard
{
    public interface IWarrantyCardRepository
    {
        public const string NotFound = "WarrantyCard_NotFound";
        public Task<Pagination<WarrantyCardEntity>> GetAllAsync(WarrantyCardQueryModel  warrantyCardQueryModel);
        public Task<List<WarrantyCardEntity>> ListAllAsync(WarrantyCardQueryModel  warrantyCardQueryModel);
        public Task<WarrantyCardEntity> SaveAsync(WarrantyCardEntity  warrantyCardEntity);
        public Task<List<WarrantyCardEntity>> SaveAsync(List<WarrantyCardEntity>  warrantyCardEntities);
        public Task<WarrantyCardEntity> FindAsync(Guid Id);
        public Task<WarrantyCardEntity> DeleteAsync(Guid Id);
        public Task<List<WarrantyCardEntity>> DeleteAsync(List<Guid> Id);
    }
}
