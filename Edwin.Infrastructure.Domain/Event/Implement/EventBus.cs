using Microsoft.Extensions.DependencyInjection;
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

        public void Publish<TEventData>(TEventData data) where TEventData : IEventData
        {
            var findType = typeof(TEventData);
            if (_eventDictionary.ContainsKey(findType))
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    List<Task> tasks = new List<Task>();
                    foreach (var type in _eventDictionary[findType])
                    {
                        //依赖注入初始化
                        tasks.Add(Task.Run(() => (scope.ServiceProvider.GetService(type) as IEventHandler<TEventData>).HandlerEventAsync(data)));
                    }
                    Task.WaitAll(tasks.ToArray());
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
