using Edwin.Infrastructure.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.EntityFramework
{
    public class EntityFrameworkUnitOfWorkManager<TContext> : IUnitOfWorkManager
        where TContext : DbContext
    {
        private TContext _context;

        public EntityFrameworkUnitOfWorkManager(TContext context)
        {
            _context = context;
        }

        public IUnitOfWork Begin()
        {
            return new EntityFrameworkUnitOfWork<TContext>(_context);
        }
    }
}
