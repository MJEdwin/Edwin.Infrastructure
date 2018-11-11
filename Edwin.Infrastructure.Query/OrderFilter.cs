using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Edwin.Infrastructure.Query
{
    public class OrderFilter<TEntity> : IFilter<TEntity>
    {
        private static ParameterExpression Argument = Expression.Parameter(typeof(TEntity));

        public string PropName { get; set; }

        public OrderType Type { get; set; }

        private Expression GetProp()
        {
            var splitPath = PropName.Split('.');
            Expression prop = Argument;
            foreach (var path in splitPath)
            {
                prop = Expression.PropertyOrField(prop, path);
            }
            return prop;
        }

        public Expression<Func<TEntity, object>> GetLambda()
        {
            return Expression.Lambda<Func<TEntity, object>>(GetProp(), Argument);
        }

        public IQueryable<TEntity> Query(IQueryable<TEntity> queryable)
        {
            if (queryable is IOrderedQueryable<TEntity>)
            {
                var query = queryable as IOrderedQueryable<TEntity>;
                switch (Type)
                {
                    case OrderType.Esc:
                        return query.ThenBy(GetLambda());
                    case OrderType.Desc:
                        return query.ThenByDescending(GetLambda());
                    default:
                        return query;
                }
            }
            else
            {
                switch (Type)
                {
                    case OrderType.Esc:
                        return queryable.OrderBy(GetLambda());
                    case OrderType.Desc:
                        return queryable.OrderByDescending(GetLambda());
                    default:
                        return queryable;
                }
            }
        }

        public static explicit operator OrderFilter<TEntity>(JProperty jProperty)
            => new OrderFilter<TEntity>
            {
                PropName = jProperty.Name,
                Type = jProperty.Value.Value<string>() == "desc" ? OrderType.Desc : OrderType.Esc
            };
    }

    public enum OrderType
    {
        Esc = 0,
        Desc = 1
    }
}
