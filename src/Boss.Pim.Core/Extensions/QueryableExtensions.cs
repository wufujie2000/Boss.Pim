using System;
using System.Linq;
using System.Linq.Expressions;
using Abp.Application.Services.Dto;
using System.Text;

namespace Boss.Pim.Extensions
{
    /// <summary>
    /// Some useful extension methods for <see cref="IQueryable{T}"/>.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Lamdba 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static IQueryable<T> PageIndex<T>(this IQueryable<T> query, int page, int size)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            return query.Skip((page - 1) * size).Take(size);
        }
    }
}
