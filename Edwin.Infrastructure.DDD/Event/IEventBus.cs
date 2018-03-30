using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.DDD.Event
{
    public interface IEventBus
    {
        void Register(Type eventData, Type eventHandler);

        void UnRegister(Type eventData, Type eventHandler);

        void Trigger<TEventData>(TEventData data) where TEventData : IEventData;
    }
}
