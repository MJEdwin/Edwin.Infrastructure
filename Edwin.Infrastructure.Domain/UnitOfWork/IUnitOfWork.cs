using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Complete();
    }
}
