using Edwin.Infrastructure.DDD.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.ORM.EntityFramework
{
    public class EntityFrameworkUnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private TContext _context;

        private IDbContextTransaction _transaction;

        public EntityFrameworkUnitOfWork(TContext context)
        {
            _context = context;
            _transaction = _context.Database.BeginTransaction();
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
                _transaction.Dispose();
        }
    }
}
