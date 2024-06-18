using shop.Domain.Entities;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Business.VirtualItem
{
    public interface IVirtualItemBusiness
    {
        public Task<Pagination<VirtualItemEntity>> GetAllAsync(VirtualItemQueryModel virtualItemQueryModel);
        public Task<List<VirtualItemEntity>> ListAllAsync(VirtualItemQueryModel virtualItemQueryModel);
        public Task<VirtualItemEntity> SaveAsync(VirtualItemEntity virtualItemEntity);
        public Task<VirtualItemEntity> PatchAsync(VirtualItemEntity virtualItemEntity);
        public Task<List<VirtualItemEntity>> SaveAsync(List<VirtualItemEntity> virtualItemEntities);
        public Task<VirtualItemEntity> FindAsync(Guid Id);
        public Task<VirtualItemEntity> DeleteAsync(Guid Id);
        public Task<List<VirtualItemEntity>> DeleteAsync(List<Guid> Id);
    }
}
