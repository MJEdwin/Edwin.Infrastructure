using Edwin.Infrastructure.DDD.Domian;
using Edwin.Infrastructure.DDD.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.ORM.EntityFramework
{
    public class EntityFrameworkRepository<TContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
        where TContext : DbContext
    {
        private TContext _context;
        private IQueryable<TEntity> _store;

        public EntityFrameworkRepository(TContext context)
        {
            _context = context;
            _store = _context.Set<TEntity>();
        }

        #region Early Load
        public IRepository<TEntity, TPrimaryKey> Load(Expression<Func<TEntity, object>> loadProp)
        {
            _store = _store.Include(loadProp);
            return this;
        }
        #endregion

        #region Query
        public IQueryable<TEntity> FindAll() => _store;

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> where = null) => _store.Where(where);

        public TEntity FindById(TPrimaryKey key) => _store.First(e => e.Id.Equals(key));

        public TEntity Find(Expression<Func<TEntity, bool>> where = null) => _store.First(where);

        public TEntity FindOrDefaultById(TPrimaryKey key) => _store.FirstOrDefault(e => e.Id.Equals(key));

        public TEntity FindOrDefault(Expression<Func<TEntity, bool>> where = null)
        {
            var entity = _store.FirstOrDefault(where);
            _context.SaveChanges();
            return entity;
        }

        public int Count() => _store.Count();

        public int Count(Expression<Func<TEntity, bool>> where = null) => _store.Count(where);

        public long LongCount() => _store.LongCount();

        public long LongCount(Expression<Func<TEntity, bool>> where = null) => _store.LongCount(where);

        #endregion

        #region Insert
        public void Insert(TEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public TPrimaryKey InsertAndGetId(TEntity entity)
        {
            var entry = _context.Add(entity);
            _context.SaveChanges();
            return entry.Entity.Id;
        }
        #endregion

        #region Update
        public void Update(TEntity entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Update(TPrimaryKey key, Action<TEntity> action)
        {
            var entity = FindById(key);
            action.Invoke(entity);
            _context.Update(entity);
            _context.SaveChanges();
        }
        #endregion

        #region Delete
        public void Delete(Expression<Func<TEntity, bool>> where = null)
        {
            foreach (var item in _store.Where(where))
            {
                _context.Remove(item);
            }
            _context.SaveChanges();
        }

        public void DeleteById(TPrimaryKey key)
        {
            _context.Remove(FindById(key));
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }
        #endregion
        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = _context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            _context.Attach(entity);
        }

        private TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
        {
            var entry = _context.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<TPrimaryKey>.Default.Equals(id, (ent.Entity as TEntity).Id)
                );

            return entry?.Entity as TEntity;
        }

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}
