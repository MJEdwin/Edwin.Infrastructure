using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Service.Sms
{
    public static class SmsRegister
    {
        public static IServiceCollection AddSms<TSmsService>(this IServiceCollection builder, Action<SmsConfigure> action)
            where TSmsService : class, ISmsService
        {
            builder.Configure(action);
            builder.AddSingleton<ISmsService, TSmsService>();
            return builder;
        }
    }
}
