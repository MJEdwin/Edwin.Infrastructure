using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.Domain.UnitOfWork
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class UnitOfWorkAttribute : Attribute
    {
        public IUnitOfWorkManager Manager { get; set; }
    }
}
