using Edwin.Infrastructure.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edwin.Infrastructure.AutoMapper
{
    public static class AutoMapperRegister
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection service)
            => service.AddSingleton(AutoMapperConfiguration.Instance.GetMapper());
    }
}
