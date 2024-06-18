namespace shop.Infrastructure.Intercepter;

public interface IAfterSavedInterceptor<T>
{
    int Order { get; set; }

    Task Intercept(T oldEntity, T updatedEntity);
}