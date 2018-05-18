using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.Domain.Event
{
    /// <summary>
    /// 全球事件总线接口
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 事件订阅
        /// </summary>
        /// <param name="eventData">事件数据类型</param>
        /// <param name="eventHandler">事件处理类型</param>
        void Subscribe(Type eventData, Type eventHandler);
        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="eventData">事件数据类型</param>
        /// <param name="eventHandler">事件处理类型</param>
        void UnSubscribe(Type eventData, Type eventHandler);
        /// <summary>
        /// 事件触发
        /// </summary>
        /// <typeparam name="TEventData">事件数据泛型</typeparam>
        /// <param name="data">数据实例</param>
        void Publish<TEventData>(TEventData data) where TEventData : IEventData;
    }
}
