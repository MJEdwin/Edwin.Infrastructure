using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.Castle
{
    public class InterceptorContext
    {
        private readonly IInvocation _invocation;
        private readonly IServiceProvider _serviceProvider;

        public InterceptorContext(IInvocation invocation, IServiceProvider serviceProvider)
        {
            _invocation = invocation;
            _serviceProvider = serviceProvider;
        }
        #region DepdencyInjection
        public object GetService(Type serviceType) => _serviceProvider.GetService(serviceType);

        public T GetService<T>() where T : class => _serviceProvider.GetService(typeof(T)) as T;
        #endregion

        #region Aop MethodInfo
        public object[] Arguments => _invocation.Arguments;

        public Type[] GenericArguments => _invocation.GenericArguments;

        public object InvocationTarget => _invocation.InvocationTarget;

        public MethodInfo Method => _invocation.Method;

        public object Proxy => _invocation.Proxy;

        public object ReturnValue
        {
            get
            {
                return _invocation.ReturnValue;
            }
            set
            {
                _invocation.ReturnValue = value;
            }
        }

        public Type TargetType => _invocation.TargetType;

        public object GetArgumentValue(int index) => _invocation.GetArgumentValue(index);

        public MethodInfo GetConcreteMethod() => _invocation.GetConcreteMethod();

        public void SetArgumentValue(int index, object value) => _invocation.SetArgumentValue(index, value);
        #endregion

    }

    public delegate Task InterceptorDelegate();
}
