using Edwin.Infrastructure.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Domain.Application
{
    public interface ICommand<TEntity, TPrimaryKey>
        where TEntity : IEntity<TPrimaryKey>, IAggregateRoot
    {
        TEntity Create<TDTO>(TDTO dto);
        TEntity Create(Dictionary<string, object> dictionary);
        TEntity Upgrade<TDTO>(TPrimaryKey id, TDTO dto);
        TEntity Upgrade(TPrimaryKey id, Dictionary<string, object> dictionary);
        void Delete(params TPrimaryKey[] id);
    }
}
