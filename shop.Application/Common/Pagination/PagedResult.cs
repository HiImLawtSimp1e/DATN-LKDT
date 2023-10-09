namespace shop.Application.Common.Pagination;

public class PagedResult<T> : PagedResultBase
{
    public List<T> Items { get; set; }
}