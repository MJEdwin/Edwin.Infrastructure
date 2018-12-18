using Edwin.Infrastructure.Domain.Domain;
using Edwin.Infrastructure.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.EntityFramework
{
    public class EntityFrameworkRepository<TContext, TEntity, TIdentify> : IRepository<TEntity, TIdentify>
        where TEntity : class, IAggregateRoot, IEntity<TIdentify>
        where TContext : DbContext
    {
        private TContext _context;
        private DbSet<TEntity> _store => _context.Set<TEntity>();

        public EntityFrameworkRepository(TContext context)
        {
            _context = context;
        }

        #region Queries
        public IQueryable<TEntity> FindAll() => _store;

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> where = null) => _store.Where(where);

        public TEntity Find(Expression<Func<TEntity, bool>> where = null) => _store.First(where);

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> where = null) => _store.FirstAsync(where);

        public TEntity FindOrDefault(Expression<Func<TEntity, bool>> where = null) => _store.FirstOrDefault(where);

        public Task<TEntity> FindOrDefaultAsync(Expression<Func<TEntity, bool>> where = null) => _store.FirstOrDefaultAsync(where);

        public TEntity FindOrDefaultById(TIdentify identify) => FindOrDefaultById((object)identify);

        public Task<TEntity> FindOrDefaultByIdAsync(TIdentify identify) => FindOrDefaultByIdAsync((object)identify);

        public TEntity FindOrDefaultById(object identify) => _store.Find(identify);

        public Task<TEntity> FindOrDefaultByIdAsync(object identify) => _store.FindAsync(identify);

        public int Count() => _store.Count();

        public int Count(Expression<Func<TEntity, bool>> where = null) => _store.Count(where);

        public long LongCount() => _store.LongCount();

        public long LongCount(Expression<Func<TEntity, bool>> where = null) => _store.LongCount(where);

        public bool Exist(TEntity entity) => _store.Any(s => s == entity);

        public bool Exist(Expression<Func<TEntity, bool>> where) => _store.Any(where);
        #endregion

        #region Add
        public TEntity Add(TEntity entity)
        {
            var result = _store.Add(entity).Entity;
            _context.SaveChanges();
            return result;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = (await _store.AddAsync(entity)).Entity;
            await _context.SaveChangesAsync();
            return result;
        }

        public void AddRange(params TEntity[] entity)
        {
            _store.AddRange(entity);
            _context.SaveChanges();
        }

        public async Task AddRangeAsync(params TEntity[] entity)
        {
            await _store.AddRangeAsync(entity);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Update
        public void Update(TEntity entity)
        {
            _store.Update(entity);
            _context.SaveChanges();
        }

        public Task UpdateAsync(TEntity entity)
            => Task.Run(() => Update(entity));

        public void UpdateById(TIdentify identify, Action<TEntity> action)
        {
            var entity = FindOrDefaultById(identify);
            if (entity != null)
            {
                action.Invoke(entity);
                _store.Update(entity);
                _context.SaveChanges();
            }
        }

        public Task UpdateByIdAsync(TIdentify identify, Action<TEntity> action)
            => Task.Run(() => UpdateById(identify, action));
        #endregion

        #region Remove
        public void Remove(Expression<Func<TEntity, bool>> where = null)
        {
            _store.RemoveRange(_store.Where(where));
            _context.SaveChanges();
        }

        public Task RemoveAsync(Expression<Func<TEntity, bool>> where)
            => Task.Run(() => Remove(where));

        public void Remove(TEntity entity)
        {
            _store.Remove(entity);
            _context.SaveChanges();
        }

        public Task RemoveAsync(TEntity entity)
            => Task.Run(() => Remove(entity));

        public void RemoveById(TIdentify identify)
        {
            var entity = FindOrDefaultById(identify);
            if (entity != null)
                Remove(entity);
        }

        public Task RemoveByIdAsync(TIdentify identify)
            => Task.Run(() => RemoveById(identify));
        #endregion
    }
}
