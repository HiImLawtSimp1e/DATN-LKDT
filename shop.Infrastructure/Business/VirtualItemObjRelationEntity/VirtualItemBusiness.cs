using shop.Domain.Entities;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Repositories;

namespace shop.Infrastructure.Business
{
    public class VirtualItemBusiness : IVirtualItemBusiness
    {
        private readonly IVirtualItemObjRelationRepository _virtualItemObjRelationRepository;
        public async Task<VirtualItemObjRelationEntity> DeleteAsync(Guid Id)
        {
            return await _virtualItemObjRelationRepository.DeleteAsync(Id);
        }

        public async Task<List<VirtualItemObjRelationEntity>> DeleteAsync(List<Guid> Ids)
        {
            return await _virtualItemObjRelationRepository.DeleteAsync(Ids);
        }

        public async Task<VirtualItemObjRelationEntity> FindAsync(Guid Id)
        {
            return await _virtualItemObjRelationRepository.FindAsync(Id);
        }

        public async Task<List<VirtualItemObjRelationEntity>> FindByVirtualItemAsync(Guid VirtualItemId)
        {
            return await _virtualItemObjRelationRepository.FindByVirtualItemAsync(VirtualItemId);
        }

        public async Task<Pagination<VirtualItemObjRelationEntity>> GetAllAsync(VirtualItemObjRelationQueryModel virtualItemQueryModel)
        {
            return await _virtualItemObjRelationRepository.GetAllAsync(virtualItemQueryModel);
        }

        public async Task<List<VirtualItemObjRelationEntity>> ListAllAsync(VirtualItemObjRelationQueryModel virtualItemQueryModel)
        {
            return await _virtualItemObjRelationRepository.ListAllAsync(virtualItemQueryModel);
        }

        public async Task<VirtualItemObjRelationEntity> SaveAsync(VirtualItemObjRelationEntity virtualItemObjRelationEntity)
        {
            return await _virtualItemObjRelationRepository.SaveAsync(virtualItemObjRelationEntity);
        }

        public async Task<List<VirtualItemObjRelationEntity>> SaveAsync(List<VirtualItemObjRelationEntity> virtualItemObjRelationEntities)
        {
            return await _virtualItemObjRelationRepository.SaveAsync(virtualItemObjRelationEntities);
        }
    }
}
