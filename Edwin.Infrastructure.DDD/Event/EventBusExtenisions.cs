using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.Event
{
    public static class EventBusExtenisions
    {
        public static void Register<TEventData>(this IEventBus eventBus, Type eventHandler)
            where TEventData : IEventData
        {
            eventBus.Register(typeof(TEventData), eventHandler);
        }
        public static void Register<TEventData, TEventHandler>(this IEventBus eventBus) where TEventData : IEventData where TEventHandler : IEventHandler<TEventData>
        {
            eventBus.Register(typeof(TEventData), typeof(TEventHandler));
        }

        public static void UnRegister<TEventData>(this IEventBus eventBus, Type eventHandler)
            where TEventData : IEventData
        {
            eventBus.UnRegister(typeof(TEventData), eventHandler);
        }

        public static void UnRegister<TEventData, TEventHandler>(this IEventBus eventBus)
            where TEventData : IEventData
            where TEventHandler : IEventHandler<TEventData>
        {
            eventBus.UnRegister(typeof(TEventData), typeof(TEventHandler));
        }
    }
}
