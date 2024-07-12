using shop.Domain.Entities;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Repositories;

namespace shop.Infrastructure.Business
{
    public class VirtualItemObjRelationBusiness : IVirtualItemObjRelationBusiness
    {
        private readonly IVirtualItemObjRelationRepository _repository;

        public VirtualItemObjRelationBusiness(IVirtualItemObjRelationRepository repository)
        {
            _repository=repository;
        }

        public async Task<VirtualItemObjRelationEntity> DeleteAsync(Guid Id)
        {
            return await _repository.DeleteAsync(Id);
        }

        public async Task<List<VirtualItemObjRelationEntity>> DeleteAsync(List<Guid> Id)
        {
            return await _repository.DeleteAsync(Id);
        }

        public async Task<VirtualItemObjRelationEntity> FindAsync(Guid Id)
        {
            return await _repository.FindAsync(Id);
        }

        public async Task<List<VirtualItemObjRelationEntity>> FindByVirtualItemAsync(Guid VirtualItemId)
        {
            return await _repository.FindByVirtualItemAsync(VirtualItemId);
        }

        public async Task<Pagination<VirtualItemObjRelationEntity>> GetAllAsync(VirtualItemObjRelationQueryModel virtualItemQueryModel)
        {
           return await _repository.GetAllAsync(virtualItemQueryModel);
        }

        public async Task<List<VirtualItemObjRelationEntity>> ListAllAsync(VirtualItemObjRelationQueryModel virtualItemQueryModel)
        {
           return await _repository.ListAllAsync(virtualItemQueryModel);
        }

        public async Task<VirtualItemObjRelationEntity> SaveAsync(VirtualItemObjRelationEntity virtualItemObjRelationEntity)
        {
            return await _repository.SaveAsync(virtualItemObjRelationEntity);
        }

        public async Task<List<VirtualItemObjRelationEntity>> SaveAsync(List<VirtualItemObjRelationEntity> virtualItemObjRelationEntities)
        {
            return await _repository.SaveAsync(virtualItemObjRelationEntities);
        }
    }
}
