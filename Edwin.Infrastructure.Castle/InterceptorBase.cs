using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.Castle
{
    public class InterceptorBase : IInterceptor
    {
        private readonly IServiceProvider _serviceProvider;

        public InterceptorBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Intercept(IInvocation invocation)
        {

            var attributes = invocation.Method
                .GetCustomAttributes<IntercrptorHandlerAttribute>(true)
                .OrderBy(m => m.Order)
                .ToList();
            if (attributes != null && attributes.Count > 0)
            {
                var context = new InterceptorContext(invocation, _serviceProvider);

                Task action(int index)
                {
                    return attributes[index].ExecuteHandleAsync(context, () =>
                    {
                        if (index != attributes.Count - 1)
                        {
                            return action(index + 1);
                        }
                        else
                        {
                            invocation.Proceed();
                            return Task.CompletedTask;
                        }
                    });
                }

                action(0).GetAwaiter().GetResult();
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
