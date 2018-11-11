using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edwin.Infrastructure.Query
{
    public static class QueryExtension
    {
        public static IQueryable<TEntity> Paging<TEntity>(this IQueryable<TEntity> queryable, PagingFilter<TEntity> pagingFilter)
            => pagingFilter.Query(queryable);

        public static IQueryable<TEntity> Search<TEntity>(this IQueryable<TEntity> queryable, QueryFilter<TEntity> queryFilter)
            => queryFilter.Query(queryable);

        public static IQueryable<TEntity> Order<TEntity>(this IQueryable<TEntity> queryable, OrderFilter<TEntity> orderFilter)
            => orderFilter.Query(queryable);
    }
}
