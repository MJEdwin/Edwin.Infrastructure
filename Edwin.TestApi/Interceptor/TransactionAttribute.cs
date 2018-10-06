using Edwin.Infrastructure.Castle;
using Edwin.Infrastructure.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edwin.TestApi.Interceptor
{
    public class TransactionAttribute : IntercrptorHandlerAttribute
    {
        public override async Task ExecuteHandleAsync(InterceptorContext context, InterceptorDelegate next)
        {
            var _manager = context.GetService<IUnitOfWorkManager>();

            using (var unitofwork = _manager.Begin())
            {
                await next();
                unitofwork.Complete();
            }
        }
    }
}
