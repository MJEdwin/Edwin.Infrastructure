using Castle.DynamicProxy;
using Edwin.Infrastructure.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edwin.TestApi.Interceptor
{
    public class Transaction : IInterceptor
    {
        private IUnitOfWork _unitOfWork;

        public Transaction(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.CustomAttributes.Any(c => c.AttributeType == typeof(TransactionAttribute)))
            {
                invocation.Proceed();
                _unitOfWork.Complete();
            }
        }
    }
}
