using Edwin.Infrastructure.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Domain.Event.Implement
{
    public class EntityEventData : IEntityEventData
    {
        public object Entity { get; set; }
        public DateTime EventTime { get; set; }
        public object EventSource { get; set; }
        public bool Inherited { get; set; }
    }

    public class EntityEventData<TEntity> : EntityEventData, IEntityEventData<TEntity>
        where TEntity : class, IEntity
    {
        public new TEntity Entity { get => base.Entity as TEntity; set => base.Entity = value; }
    }
}
