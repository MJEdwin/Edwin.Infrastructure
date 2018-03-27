using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.UnitOfWork
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWork Begin();
    }
}
