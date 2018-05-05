using CallCenter.DAL.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.DAL.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PaginatedList<T>> ToPaginatedList<T, TKey>(this IQueryable<T> query, int pageIndex, int pageSize, Expression<Func<T, TKey>> expression)
        {
            var collection = await query.OrderBy<T, TKey>(expression).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(pageIndex, pageSize, query.Count(), collection);
        } 
    }
}