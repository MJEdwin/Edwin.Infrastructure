using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.DDD.Event.Implement
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Type>> _eventDictionary = new Dictionary<Type, List<Type>>();

        private IServiceProvider _serviceProvider;

        public EventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Register(Type eventData, Type eventHandler)
        {
            if (!_eventDictionary.ContainsKey(eventData))
                _eventDictionary.Add(eventData, new List<Type>());

            _eventDictionary[eventData].Add(eventHandler);
        }

        private void DoHandler<TEventData>(IEventHandler<TEventData> handler, TEventData data)
            where TEventData : IEventData
        {
            if (handler.Async)
            {
                Task.Run(() => handler.HandlerEvent(data));
            }
            else
                handler.HandlerEvent(data);
        }

        public void Trigger<TEventData>(TEventData data) where TEventData : IEventData
        {
            var findType = typeof(TEventData);
            foreach (var key in _eventDictionary.Keys)
            {
                //判断是否禁止冒泡
                if ((data.DisableBubbling ? false : findType.IsSubclassOf(key)) || findType == key)
                {
                    foreach (Type type in _eventDictionary[key])
                    {
                        try
                        {
                            //依赖注入初始化
                            DoHandler(_serviceProvider.GetService(type) as IEventHandler<TEventData>, data);
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
            }
        }

        public void UnRegister(Type eventData, Type eventHandler)
        {
            if (_eventDictionary.ContainsKey(eventData))
                _eventDictionary[eventData].Remove(eventHandler);
        }
    }
}
