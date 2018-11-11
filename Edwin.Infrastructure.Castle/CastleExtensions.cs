using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection.Emit;

namespace Edwin.Infrastructure.Castle
{
    public static class CastleExtensions
    {
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

        public static IServiceCollection AddWithIntercrpt(this IServiceCollection services, Type serviceType, ServiceLifetime serviceLifetime, params Type[] interceptorTypes)
        {
            var paramsInfo = serviceType.GetConstructors()[0].GetParameters();
            services.Add(new ServiceDescriptor(serviceType, provider =>
            {
                var argument = paramsInfo.Select(p => provider.GetService(p.ParameterType));
                var target = Activator.CreateInstance(serviceType, argument.ToArray());
                var interceptors = interceptorTypes.Select(i => provider.GetService(i) as IInterceptor).ToArray();
                return ProxyGenerator.CreateClassProxyWithTarget(serviceType, target, interceptors);
            }, serviceLifetime));

            return services;
        }

        public static IServiceCollection AddWithIntercrpt(this IServiceCollection services, Type serviceType, Type implentationType, ServiceLifetime serviceLifetime, params Type[] interceptorTypes)
        {
            var paramsInfo = implentationType.GetConstructors()[0].GetParameters();
            services.Add(new ServiceDescriptor(serviceType, provider =>
            {
                var argument = paramsInfo.Select(p => provider.GetService(p.ParameterType));
                var target = Activator.CreateInstance(implentationType, argument.ToArray());
                var interceptors = interceptorTypes.Select(i => provider.GetService(i) as IInterceptor).ToArray();
                return ProxyGenerator.CreateInterfaceProxyWithTarget(serviceType, target, interceptors);
            }, serviceLifetime));

            return services;
        }

        #region Transient
        public static IServiceCollection AddTransientWithIntercrpt(this IServiceCollection services, Type serviceType, params Type[] interceptorTypes)
            => services.AddWithIntercrpt(serviceType, ServiceLifetime.Transient, interceptorTypes);

        public static IServiceCollection AddTransientWithIntercrpt(this IServiceCollection services, Type serviceType, Type implentationType, params Type[] interceptorTypes)
            => services.AddWithIntercrpt(serviceType, implentationType, ServiceLifetime.Transient, interceptorTypes);

        public static IServiceCollection AddTransientWithIntercrpt<TService>(this IServiceCollection services, params Type[] interceptorTypes)
            where TService : class
            => services.AddTransientWithIntercrpt(typeof(TService), interceptorTypes);

        public static IServiceCollection AddTransientWithIntercrpt<TService, TImplentation>(this IServiceCollection services, params Type[] interceptorTypes)
            where TService : class
            where TImplentation : class, TService
            => services.AddTransientWithIntercrpt(typeof(TService), typeof(TImplentation), interceptorTypes);
        #endregion

        #region Scoped
        public static IServiceCollection AddScopedWithIntercrpt(this IServiceCollection services, Type serviceType, params Type[] interceptorTypes)
            => services.AddWithIntercrpt(serviceType, ServiceLifetime.Scoped, interceptorTypes);

        public static IServiceCollection AddScopedWithIntercrpt(this IServiceCollection services, Type serviceType, Type implentationType, params Type[] interceptorTypes)
            => services.AddWithIntercrpt(serviceType, implentationType, ServiceLifetime.Scoped, interceptorTypes);

        public static IServiceCollection AddScopedWithIntercrpt<TService>(this IServiceCollection services, params Type[] interceptorTypes)
            where TService : class
            => services.AddScopedWithIntercrpt(typeof(TService), interceptorTypes);

        public static IServiceCollection AddScopedWithIntercrpt<TService, TImplentation>(this IServiceCollection services, params Type[] interceptorTypes)
            where TService : class
            where TImplentation : class, TService
            => services.AddScopedWithIntercrpt(typeof(TService), typeof(TImplentation), interceptorTypes);
        #endregion

        #region Singleton
        public static IServiceCollection AddSingletonWithIntercrpt(this IServiceCollection services, Type serviceType, params Type[] interceptorTypes)
            => services.AddWithIntercrpt(serviceType, ServiceLifetime.Singleton, interceptorTypes);

        public static IServiceCollection AddSingletonWithIntercrpt(this IServiceCollection services, Type serviceType, Type implentationType, params Type[] interceptorTypes)
            => services.AddWithIntercrpt(serviceType, implentationType, ServiceLifetime.Singleton, interceptorTypes);

        public static IServiceCollection AddSingletonWithIntercrpt<TService>(this IServiceCollection services, params Type[] interceptorTypes)
            where TService : class
            => services.AddSingletonWithIntercrpt(typeof(TService), interceptorTypes);

        public static IServiceCollection AddSingletonWithIntercrpt<TService, TImplentation>(this IServiceCollection services, params Type[] interceptorTypes)
            where TService : class
            where TImplentation : class, TService
            => services.AddSingletonWithIntercrpt(typeof(TService), typeof(TImplentation), interceptorTypes);
        #endregion
    }
}
