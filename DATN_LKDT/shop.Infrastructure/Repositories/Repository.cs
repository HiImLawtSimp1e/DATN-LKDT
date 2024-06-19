using MicroBase.Share.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using shop.Infrastructure.Database.Context;
using Microsoft.IdentityModel.Tokens;
using Z.EntityFramework.Plus;
using MicroBase.Share.Extensions;

namespace MicroBase.Entity.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext microDbContext;

        public Repository(AppDbContext microDbContext)
        {
            this.microDbContext = microDbContext;
        }

        private AppDbContext OpenDbContext()
        {
            return microDbContext;
        }

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            var dbSet = microDbContext.Set<T>();
            return predicate == null ? dbSet.Count() : dbSet.Count(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            var dbSet = microDbContext.Set<T>();
            return predicate == null ? await dbSet.CountAsync() : await dbSet.CountAsync(predicate);
        }

        public long LongCount(Expression<Func<T, bool>> predicate = null)
        {
            var dbSet = microDbContext.Set<T>();
            return predicate != null ? dbSet.LongCount(predicate) : dbSet.LongCount();
        }

        public async Task<long> LongCountAsync(Expression<Func<T, bool>> predicate = null)
        {
            var dbSet = microDbContext.Set<T>();
            return predicate != null ? await dbSet.LongCountAsync(predicate) : await dbSet.LongCountAsync();
        }

        public void Delete(T entity)
        {
            var dbSet = microDbContext.Set<T>();
            dbSet.Remove(entity);
            microDbContext.SaveChanges();
        }

        public async Task DeleteAsync(T entity)
        {
            var dbSet = microDbContext.Set<T>();
            dbSet.Remove(entity);
            await microDbContext.SaveChangesAsync();
        }

        public void DeleteMany(Expression<Func<T, bool>> filterExpression)
        {
            var dbSet = microDbContext.Set<T>();
            dbSet.Where(filterExpression).Delete();
        }

        public void DeleteMany(IEnumerable<T> items)
        {
            var dbSet = microDbContext.Set<T>();
            dbSet.RemoveRange(items);
            microDbContext.SaveChanges();
        }

        public async Task DeleteManyAsync(Expression<Func<T, bool>> filterExpression)
        {
            var dbSet = microDbContext.Set<T>();
            await dbSet.Where(filterExpression).DeleteAsync();
        }

        public async Task DeleteManyAsync(IEnumerable<T> items)
        {
            var dbSet = microDbContext.Set<T>();
            dbSet.RemoveRange(items);
            await microDbContext.SaveChangesAsync();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filterExpression,
            FindOptions<T> findOptions = null,
            params Expression<Func<T, dynamic>>[] includes)
        {
            IQueryable<T> queryable = microDbContext.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    queryable = queryable.Include(include);
                }
            }

            var skip = 0;
            var limit = 0;

            if (findOptions != null)
            {
                if (findOptions.Sorts != null)
                {
                    bool isFirst = true;
                    foreach (var sortDirection in findOptions.Sorts)
                    {
                        queryable = SetOrderBy(queryable, sortDirection, isFirst);
                        isFirst = false;
                    }
                }

                if (findOptions.Skip.HasValue && findOptions.Skip.Value > 0)
                {
                    skip = findOptions.Skip.Value;
                }

                if (findOptions.Limit.HasValue && findOptions.Limit.Value > 0)
                {
                    limit = findOptions.Limit.Value;
                }
            }

            if (filterExpression != null)
            {
                queryable = queryable.Where(filterExpression);
            }

            if (skip > 0)
            {
                queryable = queryable.Skip(skip);
            }

            if (limit > 0)
            {
                queryable = queryable.Take(limit);
            }

            return queryable.ToImmutableArray();
        }

        public IEnumerable<TProjection> Find<TProjection>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, TProjection>> projection, FindOptions<T> findOptions = null)
        {
            if (projection == null)
            {
                throw new ArgumentNullException("projection");
            }

            IQueryable<T> queryable = microDbContext.Set<T>();
            var skip = 0;
            var limit = 0;

            if (findOptions != null)
            {
                if (!findOptions.Sorts.IsNullOrEmpty())
                {
                    bool isFirst = true;
                    foreach (var sortDirection in findOptions.Sorts)
                    {
                        queryable = SetOrderBy(queryable, sortDirection, isFirst);
                        isFirst = false;
                    }
                }

                if (findOptions.Skip.HasValue && findOptions.Skip.Value > 0)
                {
                    skip = findOptions.Skip.Value;
                }

                if (findOptions.Limit.HasValue && findOptions.Limit.Value > 0)
                {
                    limit = findOptions.Limit.Value;
                }
            }

            if (filterExpression != null)
            {
                queryable = queryable.Where(filterExpression);
            }

            if (skip > 0)
            {
                queryable = queryable.Skip(skip);
            }

            if (limit > 0)
            {
                queryable = queryable.Take(limit);
            }

            return queryable.Select(projection).ToImmutableArray();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filterExpression, FindOptions<T> findOptions = null)
        {
            IQueryable<T> queryable = microDbContext.Set<T>();
            var skip = 0;
            var limit = 0;

            if (findOptions != null)
            {
                if (!findOptions.Sorts.IsNullOrEmpty())
                {
                    bool isFirst = true;
                    foreach (var sortDirection in findOptions.Sorts)
                    {
                        queryable = SetOrderBy(queryable, sortDirection, isFirst);
                        isFirst = false;
                    }
                }

                if (findOptions.Skip.HasValue && findOptions.Skip.Value > 0)
                {
                    skip = findOptions.Skip.Value;
                }

                if (findOptions.Limit.HasValue && findOptions.Limit.Value > 0)
                {
                    limit = findOptions.Limit.Value;
                }
            }

            if (filterExpression != null)
            {
                queryable = queryable.Where(filterExpression);
            }

            if (skip > 0)
            {
                queryable = queryable.Skip(skip);
            }

            if (limit > 0)
            {
                queryable = queryable.Take(limit);
            }

            return await queryable.ToListAsync();
        }

        public async Task<IEnumerable<TProjection>> FindAsync<TProjection>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, TProjection>> projection, FindOptions<T> findOptions = null)
        {
            if (projection == null)
            {
                throw new ArgumentNullException("projection");
            }

            IQueryable<T> queryable = microDbContext.Set<T>();
            var skip = 0;
            var limit = 0;

            if (findOptions != null)
            {
                if (!findOptions.Sorts.IsNullOrEmpty())
                {
                    bool isFirst = true;
                    foreach (var sortDirection in findOptions.Sorts)
                    {
                        queryable = SetOrderBy(queryable, sortDirection, isFirst);
                        isFirst = false;
                    }
                }

                if (findOptions.Skip.HasValue && findOptions.Skip.Value > 0)
                {
                    skip = findOptions.Skip.Value;
                }

                if (findOptions.Limit.HasValue && findOptions.Limit.Value > 0)
                {
                    limit = findOptions.Limit.Value;
                }
            }

            if (filterExpression != null)
            {
                queryable = queryable.Where(filterExpression);
            }

            if (skip > 0)
            {
                queryable = queryable.Skip(skip);
            }

            if (limit > 0)
            {
                queryable = queryable.Take(limit);
            }

            return await queryable.Select(projection).ToArrayAsync();
        }

        public T FindOne(Expression<Func<T, bool>> filterExpression, FindOptions<T> findOptions = null, params Expression<Func<T, dynamic>>[] includes)
        {
            IQueryable<T> queryable = microDbContext.Set<T>();

            if (!includes.IsNullOrEmpty())
            {
                foreach (var include in includes)
                {
                    queryable = queryable.Include(include);
                }
            }

            if (findOptions != null)
            {
                if (!findOptions.Sorts.IsNullOrEmpty())
                {
                    bool isFirst = true;
                    foreach (var sortDirection in findOptions.Sorts)
                    {
                        queryable = SetOrderBy(queryable, sortDirection, isFirst);
                        isFirst = false;
                    }
                }
            }

            if (filterExpression != null)
            {
                queryable = queryable.Where(filterExpression);
            }

            return queryable.FirstOrDefault();
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> filterExpression, FindOptions<T> findOptions = null)
        {
            IQueryable<T> queryable = microDbContext.Set<T>();

            if (findOptions != null)
            {
                if (!findOptions.Sorts.IsNullOrEmpty())
                {
                    bool isFirst = true;
                    foreach (var sortDirection in findOptions.Sorts)
                    {
                        queryable = SetOrderBy(queryable, sortDirection, isFirst);
                        isFirst = false;
                    }
                }
            }

            if (filterExpression != null)
            {
                queryable = queryable.Where(filterExpression);
            }

            return await queryable.FirstOrDefaultAsync();
        }

        public async Task<TProjection> FindOneAsync<TProjection>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, TProjection>> projection)
        {
            if (projection == null)
            {
                throw new ArgumentNullException("projection");
            }

            if (filterExpression == null)
            {
                return await microDbContext.Set<T>().Select(projection).FirstOrDefaultAsync();
            }

            return await microDbContext.Set<T>().Where(filterExpression).Select(projection).FirstOrDefaultAsync();
        }

        public T GetById(Guid id)
        {
            return microDbContext.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await microDbContext.Set<T>().FindAsync(id);
        }

        #region Insert

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            microDbContext.Set<T>().Add(entity);
            microDbContext.SaveChanges();
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            microDbContext.Set<T>().Add(entity);
            await microDbContext.SaveChangesAsync();
        }

        public void InsertMany(IEnumerable<T> items)
        {
            if (items == null)
            {
                return;
            }

            microDbContext.Set<T>().AddRange(items);
            microDbContext.SaveChanges();
        }

        public async Task InsertManyAsync(IEnumerable<T> items)
        {
            if (items == null)
            {
                return;
            }

            microDbContext.Set<T>().AddRange(items);
            await microDbContext.SaveChangesAsync();
        }

        #endregion Insert

        #region Update

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            // Set the entity's state to modified
            microDbContext.Entry(entity).State = EntityState.Modified;
            microDbContext.SaveChanges();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            // Set the entity's state to modified
            microDbContext.Entry(entity).State = EntityState.Modified;
            await microDbContext.SaveChangesAsync();
        }

        public void UpdateMany(IEnumerable<T> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (var item in items)
            {
                microDbContext.Entry(item).State = EntityState.Modified;
            }
            microDbContext.SaveChanges();
        }

        public void UpdateMany(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T>> updateExpression)
        {
            var memberInitExpression = updateExpression.Body as MemberInitExpression;
            if (memberInitExpression == null)
            {
                throw new ArgumentException("The update expression must be of type MemberInitExpression.", "updateExpression");
            }

            var records = microDbContext.Set<T>().Where(filterExpression);
            bool needUpdate = false;

            foreach (var record in records)
            {
                foreach (MemberBinding binding in memberInitExpression.Bindings)
                {
                    var memberAssignment = binding as MemberAssignment;

                    if (memberAssignment == null)
                    {
                        throw new ArgumentException("The update expression MemberBinding must only by type MemberAssignment.", "updateExpression");
                    }

                    Expression memberExpression = memberAssignment.Expression;

                    object value;
                    if (memberExpression.NodeType == ExpressionType.Constant)
                    {
                        var constantExpression = memberExpression as ConstantExpression;
                        if (constantExpression == null)
                        {
                            throw new ArgumentException("The MemberAssignment expression is not a ConstantExpression.", "updateExpression");
                        }

                        value = constantExpression.Value;
                    }
                    else
                    {
                        LambdaExpression lambda = Expression.Lambda(memberExpression, null);
                        value = lambda.Compile().DynamicInvoke();
                    }

                    ((PropertyInfo)memberAssignment.Member).SetValue(record, value);
                    needUpdate = true;
                }
            }

            if (needUpdate)
            {
                microDbContext.SaveChanges();
            }
        }

        public async Task UpdateManyAsync(IEnumerable<T> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (var item in items)
            {
                microDbContext.Entry(item).State = EntityState.Modified;
            }
            await microDbContext.SaveChangesAsync();
        }

        public async Task UpdateManyAsync(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T>> updateExpression)
        {
            var memberInitExpression = updateExpression.Body as MemberInitExpression;

            if (memberInitExpression == null)
            {
                throw new ArgumentException("The update expression must be of type MemberInitExpression.", "updateExpression");
            }

            var records = microDbContext.Set<T>().Where(filterExpression);
            var needUpdate = false;

            foreach (var record in records)
            {
                foreach (MemberBinding binding in memberInitExpression.Bindings)
                {
                    var memberAssignment = binding as MemberAssignment;
                    if (memberAssignment == null)
                    {
                        throw new ArgumentException("The update expression MemberBinding must only by type MemberAssignment.", "updateExpression");
                    }

                    Expression memberExpression = memberAssignment.Expression;

                    object value;
                    if (memberExpression.NodeType == ExpressionType.Constant)
                    {
                        var constantExpression = memberExpression as ConstantExpression;
                        if (constantExpression == null)
                        {
                            throw new ArgumentException("The MemberAssignment expression is not a ConstantExpression.", "updateExpression");
                        }
                        value = constantExpression.Value;
                    }
                    else
                    {
                        LambdaExpression lambda = Expression.Lambda(memberExpression, null);
                        value = lambda.Compile().DynamicInvoke();
                    }

                    ((PropertyInfo)memberAssignment.Member).SetValue(record, value);
                    needUpdate = true;
                }
            }

            if (needUpdate)
            {
                await microDbContext.SaveChangesAsync();
            }
        }

        #endregion Update

        #region Non-Public Methods

        private static Expression<Func<TSource, TSourceKey>> GetExpression<TSource, TSourceKey>(LambdaExpression lambdaExpression)
        {
            return (Expression<Func<TSource, TSourceKey>>)lambdaExpression;
        }

        /// <summary>
        /// Orders a queryable according to specified SortDirection
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">The queryable</param>
        /// <param name="sortDirection">The sort direction</param>
        /// <param name="isFirst">true to use OrderBy(), false to use ThenBy()</param>
        /// <returns></returns>
        private static IOrderedQueryable<TSource> SetOrderBy<TSource>(
            IQueryable<TSource> source, KeyValuePair<LambdaExpression, SortDirection> sortDirection, bool isFirst)
        {
            Type type = sortDirection.Key.Body.Type;
            bool isNullable = type.IsNullable();

            if (isNullable)
            {
                type = Nullable.GetUnderlyingType(type);
            }

            if (isFirst)
            {
                if (sortDirection.Value == SortDirection.Ascending)
                {
                    #region OrderBy

                    switch (Type.GetTypeCode(type))
                    {
                        case TypeCode.String: return source.OrderBy(GetExpression<TSource, string>(sortDirection.Key));
                        case TypeCode.Boolean:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, bool?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, bool>(sortDirection.Key));
                            }
                        case TypeCode.Int16:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, short?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, short>(sortDirection.Key));
                            }
                        case TypeCode.Int32:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, int?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, int>(sortDirection.Key));
                            }
                        case TypeCode.Int64:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, long?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, long>(sortDirection.Key));
                            }
                        case TypeCode.Single:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, float?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, float>(sortDirection.Key));
                            }
                        case TypeCode.Byte:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, byte?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, byte>(sortDirection.Key));
                            }
                        case TypeCode.Decimal:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, decimal?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, decimal>(sortDirection.Key));
                            }
                        case TypeCode.DateTime:
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, DateTime?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, DateTime>(sortDirection.Key));
                            }
                        default:
                            if (type == typeof(int))
                            {
                                return isNullable
                                    ? source.OrderBy(GetExpression<TSource, int?>(sortDirection.Key))
                                    : source.OrderBy(GetExpression<TSource, int>(sortDirection.Key));
                            }
                            throw new ArgumentOutOfRangeException();
                    }

                    #endregion OrderBy
                }
                else
                {
                    #region OrderByDescending

                    switch (Type.GetTypeCode(type))
                    {
                        case TypeCode.String: return source.OrderByDescending(GetExpression<TSource, string>(sortDirection.Key));
                        case TypeCode.Boolean:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, bool?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, bool>(sortDirection.Key));
                            }
                        case TypeCode.Int16:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, short?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, short>(sortDirection.Key));
                            }
                        case TypeCode.Int32:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, int?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, int>(sortDirection.Key));
                            }
                        case TypeCode.Int64:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, long?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, long>(sortDirection.Key));
                            }
                        case TypeCode.Single:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, float?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, float>(sortDirection.Key));
                            }
                        case TypeCode.Byte:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, byte?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, byte>(sortDirection.Key));
                            }
                        case TypeCode.Decimal:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, decimal?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, decimal>(sortDirection.Key));
                            }
                        case TypeCode.DateTime:
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, DateTime?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, DateTime>(sortDirection.Key));
                            }

                        default:
                            if (type == typeof(int))
                            {
                                return isNullable
                                    ? source.OrderByDescending(GetExpression<TSource, int?>(sortDirection.Key))
                                    : source.OrderByDescending(GetExpression<TSource, int>(sortDirection.Key));
                            }
                            throw new ArgumentOutOfRangeException();
                    }

                    #endregion OrderByDescending
                }
            }
            else
            {
                var orderedQueryable = source as IOrderedQueryable<TSource>;

                if (sortDirection.Value == SortDirection.Ascending)
                {
                    #region ThenBy

                    switch (Type.GetTypeCode(type))
                    {
                        case TypeCode.String: return orderedQueryable.ThenBy(GetExpression<TSource, string>(sortDirection.Key));
                        case TypeCode.Boolean:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenBy(GetExpression<TSource, bool?>(sortDirection.Key))
                                    : orderedQueryable.ThenBy(GetExpression<TSource, bool>(sortDirection.Key));
                            }
                        case TypeCode.Int16:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenBy(GetExpression<TSource, short?>(sortDirection.Key))
                                    : orderedQueryable.ThenBy(GetExpression<TSource, short>(sortDirection.Key));
                            }
                        case TypeCode.Int32:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenBy(GetExpression<TSource, int?>(sortDirection.Key))
                                    : orderedQueryable.ThenBy(GetExpression<TSource, int>(sortDirection.Key));
                            }
                        case TypeCode.Int64:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenBy(GetExpression<TSource, long?>(sortDirection.Key))
                                    : orderedQueryable.ThenBy(GetExpression<TSource, long>(sortDirection.Key));
                            }
                        case TypeCode.Single:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenBy(GetExpression<TSource, float?>(sortDirection.Key))
                                    : orderedQueryable.ThenBy(GetExpression<TSource, float>(sortDirection.Key));
                            }
                        case TypeCode.DateTime:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenBy(GetExpression<TSource, DateTime?>(sortDirection.Key))
                                    : orderedQueryable.ThenBy(GetExpression<TSource, DateTime>(sortDirection.Key));
                            }
                        case TypeCode.Byte:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenBy(GetExpression<TSource, byte?>(sortDirection.Key))
                                    : orderedQueryable.ThenBy(GetExpression<TSource, byte>(sortDirection.Key));
                            }
                        case TypeCode.Decimal:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenBy(GetExpression<TSource, decimal?>(sortDirection.Key))
                                    : orderedQueryable.ThenBy(GetExpression<TSource, decimal>(sortDirection.Key));
                            }

                        default:
                            if (type == typeof(int))
                            {
                                return isNullable
                                    ? orderedQueryable.ThenBy(GetExpression<TSource, int?>(sortDirection.Key))
                                    : orderedQueryable.ThenBy(GetExpression<TSource, int>(sortDirection.Key));
                            }
                            throw new ArgumentOutOfRangeException();
                    }

                    #endregion ThenBy
                }
                else
                {
                    #region ThenByDescending

                    switch (Type.GetTypeCode(type))
                    {
                        case TypeCode.String: return orderedQueryable.ThenByDescending(GetExpression<TSource, string>(sortDirection.Key));
                        case TypeCode.Boolean:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenByDescending(GetExpression<TSource, bool?>(sortDirection.Key))
                                    : orderedQueryable.ThenByDescending(GetExpression<TSource, bool>(sortDirection.Key));
                            }
                        case TypeCode.Int16:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenByDescending(GetExpression<TSource, short?>(sortDirection.Key))
                                    : orderedQueryable.ThenByDescending(GetExpression<TSource, short>(sortDirection.Key));
                            }
                        case TypeCode.Int32:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenByDescending(GetExpression<TSource, int?>(sortDirection.Key))
                                    : orderedQueryable.ThenByDescending(GetExpression<TSource, int>(sortDirection.Key));
                            }
                        case TypeCode.Int64:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenByDescending(GetExpression<TSource, long?>(sortDirection.Key))
                                    : orderedQueryable.ThenByDescending(GetExpression<TSource, long>(sortDirection.Key));
                            }
                        case TypeCode.Single:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenByDescending(GetExpression<TSource, float?>(sortDirection.Key))
                                    : orderedQueryable.ThenByDescending(GetExpression<TSource, float>(sortDirection.Key));
                            }
                        case TypeCode.DateTime:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenByDescending(GetExpression<TSource, DateTime?>(sortDirection.Key))
                                    : orderedQueryable.ThenByDescending(GetExpression<TSource, DateTime>(sortDirection.Key));
                            }
                        case TypeCode.Byte:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenByDescending(GetExpression<TSource, byte?>(sortDirection.Key))
                                    : orderedQueryable.ThenByDescending(GetExpression<TSource, byte>(sortDirection.Key));
                            }
                        case TypeCode.Decimal:
                            {
                                return isNullable
                                    ? orderedQueryable.ThenByDescending(GetExpression<TSource, decimal?>(sortDirection.Key))
                                    : orderedQueryable.ThenByDescending(GetExpression<TSource, decimal>(sortDirection.Key));
                            }

                        default:
                            if (type == typeof(int))
                            {
                                return isNullable
                                    ? orderedQueryable.ThenByDescending(GetExpression<TSource, int?>(sortDirection.Key))
                                    : orderedQueryable.ThenByDescending(GetExpression<TSource, int>(sortDirection.Key));
                            }
                            throw new ArgumentOutOfRangeException();
                    }

                    #endregion ThenByDescending
                }
            }
        }

        #endregion Non-Public Methods
    }
}