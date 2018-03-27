using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.DDD.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Complete();
    }
}
