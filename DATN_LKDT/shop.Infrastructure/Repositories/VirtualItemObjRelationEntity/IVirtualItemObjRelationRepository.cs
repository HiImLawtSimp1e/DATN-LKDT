using shop.Domain.Entities;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Model;

namespace shop.Infrastructure.Repositories
{
    public interface IVirtualItemObjRelationRepository
    {
        public const string VirtualItemObjRelationEntity_NotFound = "VirtualItemObjRelationEntity_NotFound";
        public Task<Pagination<VirtualItemObjRelationEntity>> GetAllAsync(VirtualItemObjRelationQueryModel virtualItemQueryModel);
        public Task<List<VirtualItemObjRelationEntity>> ListAllAsync(VirtualItemObjRelationQueryModel virtualItemQueryModel);
        public Task<VirtualItemObjRelationEntity> SaveAsync(VirtualItemObjRelationEntity virtualItemObjRelationEntity);
        public Task<List<VirtualItemObjRelationEntity>> SaveAsync(List<VirtualItemObjRelationEntity>  virtualItemObjRelationEntities);
        public Task<VirtualItemObjRelationEntity> DeleteAsync(Guid Id);
        public Task<VirtualItemObjRelationEntity> FindAsync(Guid Id);
        public Task<List<VirtualItemObjRelationEntity>> FindByVirtualItemAsync(Guid VirtualItemId);
        public Task<List<VirtualItemObjRelationEntity>> DeleteAsync(List<Guid> Id);
    }
}
