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
        {
            //反射获取所有程序集中的autoMapper profile
            var assembies = AssemblyFinder.GetCustomerAssemblies().Select(a => a.FullName).ToArray();
            service.AddSingleton(new AutoMapperConfiguration(assembies).GetMapper());
            return service;
        }
    }
}
