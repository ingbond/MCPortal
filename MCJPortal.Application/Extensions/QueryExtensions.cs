using MCJPortal.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MCJPortal.Application.Extensions
{
    public static class QueryExtensions
    {
        private static IOrderedQueryable<T> Order<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), string.Empty);
            MemberExpression property = Expression.PropertyOrField(param, propertyName);
            LambdaExpression sort = Expression.Lambda(property, param);

            MethodCallExpression call = Expression.Call(
                typeof(Queryable),
                (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
                new[] { typeof(T), property.Type },
                source.Expression,
                Expression.Quote(sort));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return Order(source, propertyName, false, false);
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return Order(source, propertyName, true, false);
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return Order(source, propertyName, false, true);
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName)
        {
            return Order(source, propertyName, true, true);
        }

        public static IQueryable<T> FilterByParams<T>(this IQueryable<T> source, FilterModel filterModel)
        {
            if (filterModel.Pagination != null)
            {
                if (!String.IsNullOrEmpty(filterModel.Pagination.Field))
                {
                    if (filterModel.Pagination.Dir == "asc")
                    {
                        source = source.OrderBy<T>(filterModel.Pagination.Field);
                    }
                    else
                    {
                        source = source.OrderByDescending<T>(filterModel.Pagination.Field);
                    }
                }

                if (filterModel.Pagination.Skip.HasValue)
                {
                    source = source.Skip(filterModel.Pagination.Skip.Value);
                }

                if (filterModel.Pagination.Take.HasValue)
                {
                    source = source.Take(filterModel.Pagination.Take.Value);
                }
            }

            return source;
        }
    }
}
