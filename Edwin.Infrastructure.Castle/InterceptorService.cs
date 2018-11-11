using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using Edwin.Infrastructure.Extensions;

namespace Edwin.Infrastructure.Castle
{
    public class InterceptorService : IInterceptor
    {
        private readonly IServiceProvider _serviceProvider;

        public InterceptorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Intercept(IInvocation invocation)
        {
            var handlerType = typeof(IIntercrptorHandler);

            var handlers = GetAllHandlers(invocation).ToList();

            if (handlers.IsNullOrEmpty())
            {
                invocation.Proceed();
                return;
            }
            var context = new InterceptorContext(invocation, _serviceProvider);

            DoActionAsync(invocation, context, handlers).GetAwaiter().GetResult();
        }

        protected Task DoActionAsync(IInvocation invocation, InterceptorContext context, List<IIntercrptorHandler> handlers, int index = 0)
        {
            return handlers[index].ExecuteHandleAsync(context, () =>
            {
                if (index != handlers.Count - 1)
                    return DoActionAsync(invocation, context, handlers, index + 1);
                invocation.Proceed();
                return Task.CompletedTask;
            });
        }

        protected IEnumerable<IIntercrptorHandler> GetAllHandlers(IInvocation invocation)
        {
            var handlerType = typeof(IIntercrptorHandler);

            var methodAttributes = invocation.Method
                .GetCustomAttributes()
                .Where(m => handlerType.IsAssignableFrom(m.GetType()))
                .Select(m => m as IIntercrptorHandler)
                .OrderBy(m => m.Order);

            return methodAttributes;
        }
    }
}
