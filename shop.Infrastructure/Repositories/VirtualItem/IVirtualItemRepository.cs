using shop.Domain.Entities;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;

namespace shop.Infrastructure.Repositories.VirtualItem
{
    public interface IVirtualItemRepository
    {
        const string Message_VirtualItemNotFound = "VirtualItemNotFound";

        public Task<Pagination<VirtualItemEntity>> GetAllAsync(VirtualItemQueryModel virtualItemQueryModel);
        public Task<List<VirtualItemEntity>> ListAllAsync(VirtualItemQueryModel virtualItemQueryModel);
        public Task<VirtualItemEntity> SaveAsync(VirtualItemEntity virtualItemEntity);
        public Task<List<VirtualItemEntity>> SaveAsync(List<VirtualItemEntity> virtualItemEntities);
        public Task<VirtualItemEntity> FindAsync(Guid Id);
        public Task<VirtualItemEntity> DeleteAsync(Guid Id);
        public Task<List<VirtualItemEntity>> DeleteAsync(List<Guid> Id);
    }
}
