using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.Domain.Event.Implement
{
    /// <summary>
    /// 全球事件总线实现
    /// </summary>
    public class EventBus : IEventBus
    {
        /// <summary>
        /// 事件字典
        /// </summary>
        private readonly Dictionary<Type, List<Type>> _eventDictionary = new Dictionary<Type, List<Type>>();
        /// <summary>
        /// 依赖注入服务
        /// </summary>
        private IServiceProvider _serviceProvider;

        public EventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Subscribe(Type eventData, Type eventHandler)
        {
            if (!_eventDictionary.ContainsKey(eventData))
                _eventDictionary.Add(eventData, new List<Type>());
            //向事件字典注册事件
            _eventDictionary[eventData].Add(eventHandler);
        }

        private void DoHandler<TEventData>(IEventHandler<TEventData> handler, TEventData data)
            where TEventData : IEventData
        {
            //根据事件是否异步判断是否需要异步执行
            if (handler.Async)
            {
                Task.Run(() => handler.HandlerEvent(data));
            }
            else
                handler.HandlerEvent(data);
        }

        public void Publish<TEventData>(TEventData data) where TEventData : IEventData
        {
            var findType = typeof(TEventData);
            foreach (var key in _eventDictionary.Keys)
            {
                //判断是否禁止冒泡
                if ((data.Inherited ? false : findType.IsSubclassOf(key)) || findType == key)
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

        public void UnSubscribe(Type eventData, Type eventHandler)
        {
            if (_eventDictionary.ContainsKey(eventData))
                _eventDictionary[eventData].Remove(eventHandler);
        }
    }
}
