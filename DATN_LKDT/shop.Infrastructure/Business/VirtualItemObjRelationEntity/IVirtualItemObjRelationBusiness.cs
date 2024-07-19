using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shop.Domain.Entities;

namespace shop.Infrastructure.Business
{
    public interface IVirtualItemObjRelationBusiness
    {
        public Task<Pagination<VirtualItemObjRelationEntity>> GetAllAsync(VirtualItemObjRelationQueryModel virtualItemQueryModel);
        public Task<List<VirtualItemObjRelationEntity>> ListAllAsync(VirtualItemObjRelationQueryModel virtualItemQueryModel);
        public Task<VirtualItemObjRelationEntity> SaveAsync(VirtualItemObjRelationEntity virtualItemObjRelationEntity);
        public Task<List<VirtualItemObjRelationEntity>> SaveAsync(List<VirtualItemObjRelationEntity> virtualItemObjRelationEntities);
        public Task<VirtualItemObjRelationEntity> DeleteAsync(Guid Id);
        public Task<VirtualItemObjRelationEntity> FindAsync(Guid Id);
        public Task<List<VirtualItemObjRelationEntity>> FindByVirtualItemAsync(Guid VirtualItemId);
        public Task<List<VirtualItemObjRelationEntity>> DeleteAsync(List<Guid> Id);
    }
}
