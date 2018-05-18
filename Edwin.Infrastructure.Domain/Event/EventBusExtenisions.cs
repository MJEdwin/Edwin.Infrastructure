using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Domain.Event
{
    public static class EventBusExtenisions
    {
        public static void Subscribe<TEventData>(this IEventBus eventBus, Type eventHandler)
            where TEventData : IEventData
        {
            eventBus.Subscribe(typeof(TEventData), eventHandler);
        }
        public static void Subscribe<TEventData, TEventHandler>(this IEventBus eventBus) where TEventData : IEventData where TEventHandler : IEventHandler<TEventData>
        {
            eventBus.Subscribe(typeof(TEventData), typeof(TEventHandler));
        }

        public static void UnSubscribe<TEventData>(this IEventBus eventBus, Type eventHandler)
            where TEventData : IEventData
        {
            eventBus.UnSubscribe(typeof(TEventData), eventHandler);
        }

        public static void UnSubscribe<TEventData, TEventHandler>(this IEventBus eventBus)
            where TEventData : IEventData
            where TEventHandler : IEventHandler<TEventData>
        {
            eventBus.UnSubscribe(typeof(TEventData), typeof(TEventHandler));
        }
    }
}
