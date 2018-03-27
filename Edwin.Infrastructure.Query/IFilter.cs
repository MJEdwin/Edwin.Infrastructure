using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edwin.Infrastructure.Query
{
    public interface IFilter<TEntity>
    {
        IQueryable<TEntity> Query(IQueryable<TEntity> queryable);
    }
}
