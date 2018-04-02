﻿using Edwin.Infrastructure.DDD.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.Event.Implement
{
    public class EntityCreatingEventData<TEntity> : EntityEventData<TEntity>
        where TEntity : class,IEntity
    {
    }
}
