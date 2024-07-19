using Microsoft.EntityFrameworkCore;
using shop.Infrastructure.Model.Common.Pagination;
using System.Security.Principal;

namespace shop.Infrastructure.Utilities;
public static class QueryableExtensions
{
    public static async Task<Pagination<T>> GetPagedAsync<T>(this IQueryable<T> query, int currentPage, int pageSize) where T : class
    {
        return await query.GetPagedOrderAsync(currentPage, pageSize, string.Empty);
    }

    public static async Task<Pagination<T>> GetPagedAsync<T>(this IQueryable<T> query, int currentPage, int pageSize, string sortExpression) where T : class
    {
        return await query.GetPagedOrderAsync(currentPage, pageSize, sortExpression);
    }

    public static async Task<Pagination<T>> GetPagedOrderAsync<T>(this IQueryable<T> query, int currentPage, int pageSize, string sortExpression) where T : class
    {
        if (!string.IsNullOrWhiteSpace(sortExpression))
        {
            query = query.ApplySorting(sortExpression);
        }

        Pagination<T> result = new Pagination<T>(await query.CountAsync(), currentPage, pageSize);
        int count = (currentPage - 1) * pageSize;
        Pagination<T> pagination = result;
        pagination.Content = await query.Skip(count).Take(pageSize).ToListAsync();
        return result;
    }

}