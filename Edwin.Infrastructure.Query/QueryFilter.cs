using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Edwin.Infrastructure.Query
{
    public class QueryFilter<TEntity> : IFilter<TEntity>
    {
        private static ParameterExpression DefaultArgument = Expression.Parameter(typeof(TEntity));

        public Expression Body { get; protected set; }

        public ParameterExpression Argument { get; } = DefaultArgument;

        public QueryFilter(Expression body = null)
        {
            Body = body;
        }

        public Expression<Func<TEntity, bool>> GetLambda()
            => Expression.Lambda<Func<TEntity, bool>>(Body, Argument);

        public IQueryable<TEntity> Query(IQueryable<TEntity> queryable)
        {
            return queryable.Where(GetLambda());
        }

        public static QueryFilter<TEntity> operator &(QueryFilter<TEntity> one, QueryFilter<TEntity> other)
        {
            return new QueryFilter<TEntity>(Expression.And(one.Body, other.Body));
        }

        public static QueryFilter<TEntity> operator |(QueryFilter<TEntity> one, QueryFilter<TEntity> other)
        {
            return new QueryFilter<TEntity>(Expression.Or(one.Body, other.Body));
        }
    }
}
