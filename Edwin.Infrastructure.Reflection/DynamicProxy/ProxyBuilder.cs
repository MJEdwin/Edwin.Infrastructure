using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Edwin.Infrastructure.Reflection.DynamicProxy
{
    public class ProxyBuilder
    {
        private const string assemblyName = "Edwin.Proxies";
        private static readonly AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run);
        private static readonly ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName);

        public T CreateInstance<T>(T @object)
            where T : class
        {
            var interfaceType = typeof(T);
            //创建代理类
            var typeBuilder = moduleBuilder.DefineType(interfaceType.Name, TypeAttributes.Class | TypeAttributes.Public);
            typeBuilder.AddInterfaceImplementation(interfaceType);
            var fieldBuilder = typeBuilder.DefineField("_proxyField", interfaceType, FieldAttributes.Private);

            var ctorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[] { interfaceType });

            var ctorIL = ctorBuilder.GetILGenerator();
            //指向this
            ctorIL.Emit(OpCodes.Ldarg_0);
            //指向第一个参数
            ctorIL.Emit(OpCodes.Ldarg_1);
            //替换
            ctorIL.Emit(OpCodes.Stfld, fieldBuilder);
            //return
            ctorIL.Emit(OpCodes.Ret);

            //create virtual method
            foreach (var method in interfaceType.GetMethods().Where(m => m.IsVirtual))
            {
                var parameters = method.GetParameters().Select(param => param.ParameterType).ToArray();
                var methodBuilder = typeBuilder.DefineMethod(method.Name, MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.Final, method.CallingConvention, method.ReturnType, parameters);
                var methodIL = methodBuilder.GetILGenerator();

                methodIL.Emit(OpCodes.Ldarg_0);

                methodIL.Emit(OpCodes.Ldfld, fieldBuilder);

                for (var i = 1; i <= parameters.Length; i++)
                {
                    methodIL.Emit(OpCodes.Ldarg_S, i);
                }

                methodIL.Emit(OpCodes.Callvirt, method);

                methodIL.Emit(OpCodes.Ret);

                typeBuilder.DefineMethodOverride(methodBuilder, method);
            }

            return Activator.CreateInstance(typeBuilder.CreateTypeInfo().AsType(), @object) as T;
        }
    }
}
