using System.Linq.Expressions;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KardesAile.Database.Extensions;

public static class QueryableExtensions
    {
        private static dynamic CreateExpression(Type type, string property)
        {
            var parameter = Expression.Parameter(type, "x");

            Expression body = parameter;

            property.Split('.').ToList().ForEach(member => body = Expression.PropertyOrField(body, member));

            return Expression.Lambda(body, parameter);
        }

	    private static async Task<IQueryable<T>> PageAsync<T>(this IQueryable<T> queryable, PagedSearchModel pagedSearchModel)
        {
            if (pagedSearchModel.PageSize == 0 || await queryable.AnyAsync() != true)
            {
                return queryable;
            }
            var skip = pagedSearchModel.Page!.Value == 0 ? 0 : (pagedSearchModel.Page!.Value - 1) * pagedSearchModel.PageSize!.Value;

            if (pagedSearchModel.SortModels!.Count == 0 || string.IsNullOrEmpty(pagedSearchModel.SortModels[0].SortName))
                return queryable.Skip(skip).Take(pagedSearchModel.PageSize!.Value);

            var sort = pagedSearchModel.SortModels[0];
            var expression = CreateExpression(typeof(T), sort.SortName);
            IOrderedQueryable<T> ordered =  sort.SortDirection == SortDirection.Descending ? 
                Queryable.OrderByDescending(queryable, expression) :
                Queryable.OrderBy(queryable, expression);

            foreach (var sortModel in pagedSearchModel.SortModels.Skip(1))
            {
                if (string.IsNullOrEmpty(sortModel.SortName)) continue;
                expression = CreateExpression(typeof(T), sortModel.SortName);
                ordered = sortModel.SortDirection == SortDirection.Descending ? 
                    Queryable.ThenByDescending(ordered, expression) :
                    Queryable.ThenBy(ordered, expression);
            }
            return ordered.Skip(skip).Take(pagedSearchModel.PageSize!.Value);
        }
        
        public static async Task<PagedResultModel<T>> ToPagedListAsync<T>(this IQueryable<T> query,
            PagedSearchModel pagedSearchModel) where T : class
        {
            var result = new PagedResultModel<T>
            {
                TotalCount = pagedSearchModel.Page == 1 ? await query.CountAsync() : 0
            };
            var pagedQueryable = await query.PageAsync(pagedSearchModel);
            result.List = await pagedQueryable.ToListAsync();

            return result;
        }

    }