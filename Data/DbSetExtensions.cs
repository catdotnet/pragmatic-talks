using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PragmaticTalks.Data
{
    public static class DbSetExtensions
    {
        public static Task<IPagedEnumerable<TProjected>> FindOrderedPagedProjectionAsync<TProjected, TEntity>(this DbSet<TEntity> dbSet, int page, int pageSize, string orderBy, string defaultOrderBy, Expression<Func<TEntity, bool>> preCondition, Expression<Func<TProjected, bool>> condition, Expression<Func<TEntity, TProjected>> selector)
            where TEntity : class
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = defaultOrderBy;
            }

            var splited = orderBy.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var order = GetOrderByExpression<TProjected>(splited[0], defaultOrderBy);
            var isDescendant = false;
            if (splited.Length > 1)
            {
                if (splited[1].ToLowerInvariant() == "desc")
                    isDescendant = true;
            }

            if (condition == null)
            {
                if (isDescendant)
                    return dbSet.GetOrderedDescendingPagedProjectionAsync<TProjected, TEntity>(page, pageSize, preCondition, order, selector);

                return dbSet.GetOrderedPagedProjectionAsync<TProjected, TEntity>(page, pageSize, preCondition, order, selector);
            }
            else
            {
                if (isDescendant)
                    return dbSet.FindOrderedDescendingPagedProjectionAsync<TProjected, TEntity>(page, pageSize, preCondition, condition, order, selector);

                return dbSet.FindOrderedPagedProjectionAsync<TProjected, TEntity>(page, pageSize, preCondition, condition, order, selector);
            }
        }

        public static async Task<IPagedEnumerable<TProjected>> FindOrderedPagedProjectionAsync<TProjected, TEntity>(this DbSet<TEntity> dbSet, int page, int pageSize, Expression<Func<TEntity, bool>> preCondition, Expression<Func<TProjected, bool>> condition, Expression<Func<TProjected, object>> orderBy, Expression<Func<TEntity, TProjected>> selector)
            where TEntity : class
        {
            var total = await dbSet.Where(preCondition).Select(selector).Where(condition).CountAsync();
            var items = await dbSet.Where(preCondition).Select(selector).Where(condition).OrderBy(orderBy).Skip(page * pageSize).Take(pageSize).ToListAsync();
            return new PagedEnumerable<TProjected>(page, pageSize, total, items);
        }

        public static Task<IPagedEnumerable<TProjected>> FindOrderedPagedProjectionAsync<TProjected, TEntity>(this DbSet<TEntity> dbSet, int page, int pageSize, Expression<Func<TEntity, bool>> preCondition, string search, string orderBy, string defaultOrderBy, Expression<Func<TEntity, TProjected>> selector)
            where TEntity : class
        {
            return dbSet.FindOrderedPagedProjectionAsync(page, pageSize, orderBy, defaultOrderBy, preCondition, GetSearchExpression<TProjected>(search), selector);
        }

        public static async Task<IPagedEnumerable<TProjected>> FindOrderedDescendingPagedProjectionAsync<TProjected, TEntity>(this DbSet<TEntity> dbSet, int page, int pageSize, Expression<Func<TEntity, bool>> preCondition, Expression<Func<TProjected, bool>> condition, Expression<Func<TProjected, object>> orderBy, Expression<Func<TEntity, TProjected>> selector)
            where TEntity : class
        {
            var total = await dbSet.Select(selector).Where(condition).CountAsync();
            var items = await dbSet.Where(preCondition).Select(selector).Where(condition).OrderByDescending(orderBy).Skip(page * pageSize).Take(pageSize).ToListAsync();
            return new PagedEnumerable<TProjected>(page, pageSize, total, items);
        }

        public static async Task<IPagedEnumerable<TProjected>> GetOrderedPagedProjectionAsync<TProjected, TEntity>(this DbSet<TEntity> dbSet, int page, int pageSize, Expression<Func<TEntity, bool>> preCondition, Expression<Func<TProjected, object>> orderBy, Expression<Func<TEntity, TProjected>> selector)
            where TEntity : class
        {
            var total = await dbSet.Where(preCondition).CountAsync();
            var items = await dbSet.Where(preCondition).Select(selector).OrderBy(orderBy).Skip(page * pageSize).Take(pageSize).ToListAsync();
            return new PagedEnumerable<TProjected>(page, pageSize, total, items);
        }

        public static async Task<IPagedEnumerable<TProjected>> GetOrderedDescendingPagedProjectionAsync<TProjected, TEntity>(this DbSet<TEntity> dbSet, int page, int pageSize, Expression<Func<TEntity, bool>> preCondition, Expression<Func<TProjected, object>> orderBy, Expression<Func<TEntity, TProjected>> selector)
            where TEntity : class
        {
            var total = await dbSet.Where(preCondition).CountAsync();
            var items = await dbSet.Where(preCondition).Select(selector).OrderByDescending(orderBy).Skip(page * pageSize).Take(pageSize).ToListAsync();
            return new PagedEnumerable<TProjected>(page, pageSize, total, items);
        }
        

        private static Expression<Func<T, object>> GetOrderByExpression<T>(string fieldName, string defaultField)
        {
            var parameter = Expression.Parameter(typeof(T));
            var prop = typeof(T).GetProperties().FirstOrDefault(p => p.Name.ToLowerInvariant().Equals(fieldName.ToLowerInvariant()));
            if (prop == null)
                prop = typeof(T).GetProperties().FirstOrDefault(p => p.Name.ToLowerInvariant().Equals(defaultField.ToLowerInvariant()));

            var member = Expression.MakeMemberAccess(parameter, prop);
            var method = prop.PropertyType.GetMethod("ToString", new Type[0]);
            var call = Expression.Call(member, method);
            return Expression.Lambda<Func<T, object>>(call, parameter);
        }

        private static Expression<Func<T, bool>> GetSearchExpression<T>(string search)
        {
            if (string.IsNullOrEmpty(search)) return null;

            var list = new List<Expression<Func<T, bool>>>();
            var parameter = Expression.Parameter(typeof(T));
            var constant = Expression.Constant(search.ToLowerInvariant());
            foreach (var prop in typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string)))
            {
                var member = Expression.MakeMemberAccess(parameter, prop);
                var methodLower = typeof(string).GetMethod("ToLower", new Type[0]); // ToLowerInvariant is not recognized by EF parser
                var methodContains = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var call = Expression.Call(member, methodLower);
                call = Expression.Call(call, methodContains, constant);

                list.Add(Expression.Lambda<Func<T, bool>>(call, parameter));
            }

            Expression result = null;
            foreach (var exp in list)
            {
                if (result == null)
                {
                    result = exp.Body;
                    continue;
                }

                result = Expression.Or(result, exp.Body);
            }

            return Expression.Lambda<Func<T, bool>>(result, parameter);
        }
    }
}
