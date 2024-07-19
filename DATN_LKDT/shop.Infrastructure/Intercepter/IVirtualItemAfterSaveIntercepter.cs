using shop.Domain.Entities;

namespace shop.Infrastructure.Intercepter
{
    public interface IVirtualItemAfterSaveIntercepter: IAfterSavedInterceptor<VirtualItemEntity>
    { 
    }
}
