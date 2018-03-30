using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.DDD.Event
{
    public interface IEventHandler<TEventData>
        where TEventData : IEventData
    {
        /// <summary>
        /// 设置当前是否为异步事件
        /// </summary>
        bool Async { get; }
        /// <summary>
        /// 事件处理程序
        /// </summary>
        /// <param name="eventData">事件数据</param>
        void HandlerEvent(TEventData eventData);
    }
}
