using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Domain.Event
{
    public interface IEventData
    {
        /// <summary>
        /// 事件触发时间
        /// </summary>
        DateTime EventTime { get; set; }
        /// <summary>
        /// 事件发生
        /// </summary>
        object EventSource { get; set; }
    }
}
