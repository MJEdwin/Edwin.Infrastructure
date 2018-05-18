using Edwin.Infrastructure.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Domain.Event.Implement
{
    public class EntityDeletingEventData<TEntity> : EntityEventData<TEntity>
        where TEntity : class,IEntity
    {
    }
}
