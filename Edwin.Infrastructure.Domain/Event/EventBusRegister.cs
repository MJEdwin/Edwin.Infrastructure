using Edwin.Infrastructure.Domain.Event.Implement;
using Edwin.Infrastructure.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Domain.Event
{
    public static class EventBusRegister
    {
        public static IServiceCollection AddEventBus(this IServiceCollection service)
        {
            //获取所有事件处理器，并将所有处理器进行依赖注入
            var types = TypeFinder.GetTypes(type => type.IsClass && type.GetInterface(typeof(IEventHandler<>).Name) != null);
            foreach (var type in types)
            {
                service.AddSingleton(type);
            }
            //注册全球事件总线
            service.AddSingleton<IEventBus, EventBus>(serviceProvider =>
            {
                var instance = new EventBus(serviceProvider);
                foreach (var type in types)
                {
                    foreach (var @interface in type.GetInterfaces())
                    {
                        if (@interface.Name == typeof(IEventHandler<>).Name)
                        {
                            //对事件处理器进行订阅
                            instance.Subscribe(@interface.GetGenericArguments()[0], type);
                        }
                    }
                }
                return instance;
            });
            return service;
        }
    }
}
