using Edwin.Infrastructure.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.EntityFramework
{
    public class EntityFrameworkUnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private TContext _context;

        public EntityFrameworkUnitOfWork(TContext context)
        {
            _context = context;
        }

        public void Complete()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Dispose()
        {
        }
    }
}
