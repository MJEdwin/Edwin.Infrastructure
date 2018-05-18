using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Domain.UnitOfWork
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWork Begin();
    }
}
