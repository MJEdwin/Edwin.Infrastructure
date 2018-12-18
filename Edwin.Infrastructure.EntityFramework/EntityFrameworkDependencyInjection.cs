using Edwin.Infrastructure.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection.Emit;
using System.Text;
using Edwin.Infrastructure.Domain.Domain;
using System.Reflection;
using Edwin.Infrastructure.Domain.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Edwin.Infrastructure.EntityFramework
{
    public static class EntityFrameworkDependencyInjection
    {
        private static Type CreateRepositoryClassProxy(Type repository, Type dbcontext)
        {
            var type = repository;
            //对EntityFrameworkRepository<,,>进行emit编织
            var asmBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("Edwin.Proxy"), AssemblyBuilderAccess.Run);
            var moduleBuilder = asmBuilder.DefineDynamicModule("Edwin.Proxy");
            //设置类
            var typeBuilder = moduleBuilder.DefineType(type.Name, TypeAttributes.Public | TypeAttributes.Class);
            //设置泛型约束
            var gtpBuilder = typeBuilder.DefineGenericParameters("TEntity", "TIdentify");
            //设置泛型约束
            gtpBuilder[0].SetGenericParameterAttributes(GenericParameterAttributes.ReferenceTypeConstraint);
            gtpBuilder[0].SetInterfaceConstraints(typeof(IAggregateRoot), typeof(IEntity<>).MakeGenericType(gtpBuilder[1].AsType()));
            //设置父类
            var parentType = type.MakeGenericType(dbcontext, gtpBuilder[0].AsType(), gtpBuilder[1].AsType());
            typeBuilder.SetParent(parentType);
            //设置构造函数
            var ctorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { dbcontext });

            var ctorIL = ctorBuilder.GetILGenerator();

            ctorIL.Emit(OpCodes.Ldarg_0);
            ctorIL.Emit(OpCodes.Ldarg_1);
            ctorIL.Emit(OpCodes.Call, TypeBuilder.GetConstructor(parentType, type.GetConstructors()[0]));
            ctorIL.Emit(OpCodes.Ret);

            return typeBuilder.CreateTypeInfo().AsType();
        }

        public static IServiceCollection AddEntityFramework<TContext>(this IServiceCollection service, Action<DbContextOptionsBuilder> options, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TContext : DbContext
        {
            service.AddDbContext<TContext>(options, serviceLifetime);
            service.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork<TContext>>();
            service.AddScoped<IUnitOfWorkManager, EntityFrameworkUnitOfWorkManager<TContext>>();

            var proxyType = CreateRepositoryClassProxy(typeof(EntityFrameworkRepository<,,>), typeof(TContext));
            service.AddScoped(typeof(IRepository<,>), proxyType);
            service.AddScoped(typeof(IRepository<>), proxyType);

            return service;
        }

        public static IServiceCollection AddEntityFrameworkPool<TContext>(this IServiceCollection service, Action<DbContextOptionsBuilder> options, int poolSize = 128)
            where TContext : DbContext
        {
            service.AddDbContextPool<TContext>(options, poolSize);
            service.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork<TContext>>();
            service.AddScoped<IUnitOfWorkManager, EntityFrameworkUnitOfWorkManager<TContext>>();

            var proxyType = CreateRepositoryClassProxy(typeof(EntityFrameworkRepository<,,>), typeof(TContext));
            service.AddScoped(typeof(IRepository<,>), proxyType);
            service.AddScoped(typeof(IRepository<>), proxyType);

            return service;
        }

        public static IWebHost CreateDbContext<TDbContext>(this IWebHost webHost)
            where TDbContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var contextName = typeof(TDbContext).Name;
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TDbContext>>();
                var context = services.GetRequiredService<TDbContext>();
                try
                {
                    context.Database.EnsureCreated();//Create DataBase
                    logger.LogInformation($"{contextName} Created Success!");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"{contextName} Created Failed!");
                }
            }
            return webHost;
        }

        public static IWebHost MigrateDbContext<TDbContext>(this IWebHost webHost)
            where TDbContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var contextName = typeof(TDbContext).Name;
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TDbContext>>();
                var context = services.GetRequiredService<TDbContext>();
                try
                {
                    context.Database.Migrate();//Migrate DataBase
                    logger.LogInformation($"{contextName} Migrated Success!");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"{contextName} Migrated Failed!");
                }
            }
            return webHost;
        }
    }
}
