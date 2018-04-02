using Edwin.Infrastructure.DDD.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.Event.Implement
{
    public class EntityEventData : IEntityEventData
    {
        public object Entity { get; set; }
        public DateTime EventTime { get; set; }
        public object EventSource { get; set; }
        public bool DisableBubbling { get; set; }
    }

    public class EntityEventData<TEntity> : EntityEventData, IEntityEventData<TEntity>
        where TEntity : class, IEntity
    {
        public new TEntity Entity { get => base.Entity as TEntity; set => base.Entity = value; }
    }
}
