using Edwin.Infrastructure.Domain.Domain;
using Edwin.Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edwin.Infrastructure.Domain.Application
{
    public interface IQueries<TEntity, TPrimaryKey>
        where TEntity : IEntity<TPrimaryKey>, IAggregateRoot
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(params IFilter<TEntity>[] filters);

        bool Exist(TPrimaryKey id);

        TEntity GetById(TPrimaryKey id);
    }
}
