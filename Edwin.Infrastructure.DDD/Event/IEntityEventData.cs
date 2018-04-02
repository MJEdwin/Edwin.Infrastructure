using Edwin.Infrastructure.DDD.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.Event
{
    public interface IEntityEventData : IEventData
    {
        object Entity { get; set; }
    }
    /// <summary>
    /// 全局EntityEvent泛型接口
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public interface IEntityEventData<TEntity> : IEventData
        where TEntity : IEntity
    {
        TEntity Entity { get; set; }
    }
}
