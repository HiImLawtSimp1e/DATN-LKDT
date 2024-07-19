using shop.Infrastructure.Model.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace shop.Infrastructure.Utilities;
public static class IQueryableExtensions
{
    private static IOrderedQueryable<T> OrderingHelper<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
    {
        ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "p");
        MemberExpression memberExpression = Expression.PropertyOrField(parameterExpression, propertyName);
        LambdaExpression expression = Expression.Lambda(memberExpression, parameterExpression);
        MethodCallExpression expression2 = Expression.Call(typeof(Queryable), ((!anotherLevel) ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty), new Type[2]
        {
            typeof(T),
            memberExpression.Type
        }, source.Expression, Expression.Quote(expression));
        return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(expression2);
    }

    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
    {
        return OrderingHelper(source, propertyName, descending: false, anotherLevel: false);
    }

    public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
    {
        return OrderingHelper(source, propertyName, descending: true, anotherLevel: false);
    }

    public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
    {
        return OrderingHelper(source, propertyName, descending: false, anotherLevel: true);
    }

    public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName)
    {
        return OrderingHelper(source, propertyName, descending: true, anotherLevel: true);
    }

    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> dbset, string sortExpression)
    {
        IQueryable<T> queryable = dbset.AsQueryable();
        IOrderedQueryable<T> orderedQueryable = queryable as IOrderedQueryable<T>;
        bool flag = false;
        List<string> list = sortExpression.Split(',', ';').ToList();
        for (int i = 0; i < list.Count; i++)
        {
            string text = list[i];
            if (string.IsNullOrEmpty(text))
            {
                continue;
            }

            text = text.Trim();
            char c = text[0];
            string propertyName = text.Substring(1);
            if (c != '-' && c != '+')
            {
                propertyName = text.Substring(0);
            }

            if (typeof(T).HasProperty(propertyName))
            {
                switch (c)
                {
                    case '+':
                        orderedQueryable = ((i == 0) ? (orderedQueryable ?? throw new InvalidOperationException()).OrderBy(propertyName) : (orderedQueryable ?? throw new InvalidOperationException()).ThenBy(propertyName));
                        flag = true;
                        break;
                    case '-':
                        orderedQueryable = ((i == 0) ? (orderedQueryable ?? throw new InvalidOperationException()).OrderByDescending(propertyName) : (orderedQueryable ?? throw new InvalidOperationException()).ThenByDescending(propertyName));
                        flag = true;
                        break;
                }
            }
        }

        if (flag)
        {
            queryable = orderedQueryable;
        }

        return queryable;
    }

    public static IQueryable<T> Where<T>(this IQueryable<T> source, string propertyName, object propertyValue, ExpressionOption type)
    {
        ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "item");
        MemberExpression left = Expression.Property(parameterExpression, propertyName);
        ConstantExpression right = Expression.Constant(propertyValue);
        Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(type switch
        {
            ExpressionOption.Equal => Expression.Equal(left, right),
            ExpressionOption.NotEqual => Expression.NotEqual(left, right),
            ExpressionOption.GreaterThan => Expression.GreaterThan(left, right),
            ExpressionOption.LessThan => Expression.LessThan(left, right),
            ExpressionOption.GreaterThanOrEqual => Expression.GreaterThanOrEqual(left, right),
            ExpressionOption.LessThanOrEqual => Expression.LessThanOrEqual(left, right),
            _ => Expression.Equal(left, right),
        }, new ParameterExpression[1] { parameterExpression });
        return source.Where(predicate);
    }

    public static IQueryable<T> WhereContains<T>(this IQueryable<T> source, string propertyName, IList<Guid> propertyValue)
    {
        ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "item");
        MemberExpression memberExpression = Expression.Property(parameterExpression, propertyName);
        ConstantExpression instance = Expression.Constant(propertyValue);
        MethodInfo method = typeof(ICollection<Guid>).GetMethod("Contains");
        Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(Expression.Call(instance, method ?? throw new InvalidOperationException(), memberExpression), new ParameterExpression[1] { parameterExpression });
        return source.Where(predicate);
    }
}
