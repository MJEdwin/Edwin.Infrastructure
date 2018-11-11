using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.Domain.Event
{
    /// <summary>
    /// 事件处理接口
    /// </summary>
    /// <typeparam name="TEventData"></typeparam>
    public interface IEventHandler<TEventData>
        where TEventData : IEventData
    {
        /// <summary>
        /// 事件处理程序
        /// </summary>
        /// <param name="eventData">事件数据</param>
        Task HandlerEventAsync(TEventData eventData);
    }
}
