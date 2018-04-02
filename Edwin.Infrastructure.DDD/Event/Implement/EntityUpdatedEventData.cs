using Edwin.Infrastructure.DDD.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.Event.Implement
{
    public class EntityUpdatedEventData<TEntity> : EntityEventData<TEntity>
        where TEntity : class,IEntity
    {
    }
}
