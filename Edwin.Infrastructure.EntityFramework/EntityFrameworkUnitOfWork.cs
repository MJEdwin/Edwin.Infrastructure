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
        private readonly TContext _context;
        private readonly IDbContextTransaction _transaction;

        public EntityFrameworkUnitOfWork(TContext context)
        {
            _context = context;
            _transaction = context.Database.CurrentTransaction ?? context.Database.BeginTransaction();
        }

        public void Complete()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception e)
            {
                _transaction.Rollback();
                throw e;
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
        }
    }
}
