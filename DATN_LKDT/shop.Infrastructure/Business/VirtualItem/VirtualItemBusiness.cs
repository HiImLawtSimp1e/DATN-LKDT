using shop.Domain.Entities;
using shop.Infrastructure.Intercepter;
using shop.Infrastructure.Model;
using shop.Infrastructure.Model.Common.Pagination;
using shop.Infrastructure.Repositories.VirtualItem;
using shop.Infrastructure.Utilities;

namespace shop.Infrastructure.Business.VirtualItem
{
    public class VirtualItemBusiness : IVirtualItemBusiness
    {
        private readonly IVirtualItemRepository _virtualItemRepository;
        private readonly IEnumerable<IVirtualItemAfterSaveIntercepter> _virtualItemAfterSaveIntercepter;
        public VirtualItemBusiness(IVirtualItemRepository virtualItemRepository, IEnumerable<IVirtualItemAfterSaveIntercepter> virtualItemAfterSaveIntercepter)
        {
            _virtualItemRepository=virtualItemRepository;
            _virtualItemAfterSaveIntercepter=virtualItemAfterSaveIntercepter;
        }

        public async Task<VirtualItemEntity> DeleteAsync(Guid Id)
        {
            var res = await DeleteAsync(new List<Guid> { Id });
            return res.FirstOrDefault();
        }

        public async Task<List<VirtualItemEntity>> DeleteAsync(List<Guid> ids)
        {
            var res = await _virtualItemRepository.DeleteAsync(ids);
            return res;
        }

        public async Task<VirtualItemEntity> FindAsync(Guid Id)
        {
            var res = await _virtualItemRepository.FindAsync(Id);
            return res;
        }

        public async Task<Pagination<VirtualItemEntity>> GetAllAsync(VirtualItemQueryModel virtualItemQueryModel)
        {
            var res = await _virtualItemRepository.GetAllAsync(virtualItemQueryModel);
            return res;
        }

        public async Task<List<VirtualItemEntity>> ListAllAsync(VirtualItemQueryModel virtualItemQueryModel)
        {
            var res = await _virtualItemRepository.ListAllAsync(virtualItemQueryModel);
            return res;
        }

        public async Task<VirtualItemEntity> PatchAsync(VirtualItemEntity virtualItemEntity)
        {
            var exist = await FindAsync(virtualItemEntity.Id);

            if (exist == null)
                throw new ArgumentException(IVirtualItemRepository.Message_VirtualItemNotFound);
            var update = AutoMapperUtils.AutoMap<VirtualItemEntity, VirtualItemEntity>(exist);

            if (!string.IsNullOrEmpty(virtualItemEntity.Name))
            {
                update.Name=virtualItemEntity.Name;
            }

            if (!string.IsNullOrEmpty(virtualItemEntity.Type))
            {
                update.Type=virtualItemEntity.Type;
            }

            if (!string.IsNullOrEmpty(virtualItemEntity.Decription))
            {
                update.Decription=virtualItemEntity.Decription;
            }

            if (!string.IsNullOrEmpty(virtualItemEntity.ImgUrl))
            {
                update.ImgUrl=virtualItemEntity.ImgUrl;
            }
            if (virtualItemEntity.Isdeleted.HasValue)
            {
                update.Isdeleted=virtualItemEntity.Isdeleted;
            }
            if (virtualItemEntity.Ispublish.HasValue)
            {
                update.Ispublish=virtualItemEntity.Ispublish;
            }
            if (virtualItemEntity.Metadata !=null && virtualItemEntity.Metadata.Any())
            {
                update.Metadata=virtualItemEntity.Metadata;
            }
            return await SaveAsync(update);
        }

        public async Task<VirtualItemEntity> SaveAsync(VirtualItemEntity virtualItemEntity)
        {
            var res = await SaveAsync(new List<VirtualItemEntity> { virtualItemEntity });
            return res.FirstOrDefault();
        }

        public async Task<List<VirtualItemEntity>> SaveAsync(List<VirtualItemEntity> virtualItemEntities)
        {
            var oldVirtualItems = new List<VirtualItemEntity>();
            foreach (var item in virtualItemEntities)
            {
                if (item.Id != Guid.Empty)
                {
                    var oldVirtualItem = await FindAsync(item.Id);
                    oldVirtualItems.Add(oldVirtualItem);
                }
            }
            var result = await _virtualItemRepository.SaveAsync(virtualItemEntities);
            foreach (var virtualItem in result)
            {
                foreach (var intercepter in _virtualItemAfterSaveIntercepter.OrderBy(x => x.Order))
                {
                    var oldObj = oldVirtualItems.FirstOrDefault(x => x.Id==virtualItem.Id);
                    await intercepter.Intercept(oldObj, virtualItem);
                }
            }
            return result;
        }
    }
}
