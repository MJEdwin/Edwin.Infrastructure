using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.Castle
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, Inherited = true)]
    public abstract class IntercrptorHandlerAttribute : Attribute
    {

        public int Order { get; set; } = 0;

        public abstract Task ExecuteHandleAsync(InterceptorContext context, InterceptorDelegate next);
    }
}
