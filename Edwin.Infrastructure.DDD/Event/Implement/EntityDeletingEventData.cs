﻿using Edwin.Infrastructure.DDD.Domian;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.Event.Implement
{
    public class EntityDeletingEventData<TEntity> : EntityEventData<TEntity>
        where TEntity : class,IEntity
    {
    }
}