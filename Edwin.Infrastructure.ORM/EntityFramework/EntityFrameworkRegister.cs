using Edwin.Infrastructure.DDD.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Text;
using Edwin.Infrastructure.DDD.Domain;
using System.Reflection;
using Edwin.Infrastructure.DDD.UnitOfWork;

namespace Edwin.Infrastructure.ORM.EntityFramework
{
    public static class EntityFrameworkRegister
    {
        private static Type CreateRepositoryClassProxy(Type repository, Type dbcontext)
        {
            var type = repository;
            //对EntityFrameworkRepository<,,>进行emit编织
            var asmBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("ORM"), AssemblyBuilderAccess.Run);
            var moduleBuilder = asmBuilder.DefineDynamicModule("ORM");
            //设置类
            var typeBuilder = moduleBuilder.DefineType(type.Name, TypeAttributes.Public | TypeAttributes.Class);
            //设置泛型约束
            var gtpBuilder = typeBuilder.DefineGenericParameters("TEntity", "TIdentify");
            //设置泛型约束
            gtpBuilder[0].SetGenericParameterAttributes(GenericParameterAttributes.ReferenceTypeConstraint);
            gtpBuilder[0].SetInterfaceConstraints(typeof(IAggregateRoot),typeof(IEntity<>).MakeGenericType(gtpBuilder[1].AsType()));
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

            //service.AddScoped(typeof(IRepository<>), CreateRepositoryClassProxy(typeof(EntityFrameworkRepository<,,>), typeof(TContext)));
            var proxyType = CreateRepositoryClassProxy(typeof(EntityFrameworkRepository<,,>), typeof(TContext));
            service.AddScoped(typeof(IEntityRepository<,>), proxyType);
            service.AddScoped(typeof(IRepository<>), proxyType);

            service.AddScoped<IUnitOfWorkManager, EntityFrameworkUnitOfWorkManager<TContext>>();
            return service;
        }

        public static IServiceCollection AddEntityFrameworkPool<TContext>(this IServiceCollection service, Action<DbContextOptionsBuilder> options, int poolSize = 128)
            where TContext : DbContext
        {
            service.AddDbContextPool<TContext>(options, poolSize);

            var proxyType = CreateRepositoryClassProxy(typeof(EntityFrameworkRepository<,,>), typeof(TContext));
            service.AddScoped(typeof(IEntityRepository<,>), proxyType);
            service.AddScoped(typeof(IRepository<>), proxyType);

            service.AddScoped<IUnitOfWorkManager, EntityFrameworkUnitOfWorkManager<TContext>>();

            return service;
        }
    }
}
