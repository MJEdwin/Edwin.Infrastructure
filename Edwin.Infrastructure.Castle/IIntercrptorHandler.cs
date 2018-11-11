using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.Castle
{
    public interface IIntercrptorHandler
    {
        int Order { get; set; }

        Task ExecuteHandleAsync(InterceptorContext context, InterceptorDelegate next);
    }
}
