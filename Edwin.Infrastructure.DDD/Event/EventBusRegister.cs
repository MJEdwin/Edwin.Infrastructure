using Edwin.Infrastructure.DDD.Event.Implement;
using Edwin.Infrastructure.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.Event
{
    public static class EventBusRegister
    {
        public static IServiceCollection AddEventBus(this IServiceCollection service)
        {
            var types = TypeFinder.GetTypes(type => type.IsClass && type.GetInterface(typeof(IEventHandler<>).Name) != null);
            foreach (var type in types)
            {
                service.AddTransient(type);
            }
            service.AddSingleton<IEventBus, EventBus>(serviceProvider =>
            {
                var instance = new EventBus(serviceProvider);
                foreach (var type in types)
                {
                    foreach (var @interface in type.GetInterfaces())
                    {
                        if (@interface.Name == typeof(IEventHandler<>).Name)
                        {
                            instance.Register(@interface.GetGenericArguments()[0], type);
                        }
                    }
                }
                return instance;
            });
            return service;
        }
    }
}
