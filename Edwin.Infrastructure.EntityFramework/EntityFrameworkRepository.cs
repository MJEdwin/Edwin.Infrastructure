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
    public class EntityFrameworkRepository<TContext, TEntity, TIdentify> : IEntityRepository<TEntity, TIdentify>
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

        public TEntity FindById(TIdentify identify) => _store.First(entity => entity.Id.Equals(identify));

        public Task<TEntity> FindByIdAsync(TIdentify identify) => _store.FirstAsync(entity => entity.Id.Equals(identify));

        public TEntity FindOrDefaultById(TIdentify identify) => _store.FirstOrDefault(entity => entity.Id.Equals(identify));

        public Task<TEntity> FindOrDefaultByIdAsync(TIdentify identify) => _store.FirstOrDefaultAsync(entity => entity.Id.Equals(identify));

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
            return _context.Add(entity).Entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return (await _context.AddAsync(entity)).Entity;
        }
        #endregion

        #region Update
        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                _context.Update(entity);
            });
        }

        public void UpdateById(TIdentify identify, Action<TEntity> action)
        {
            var entity = FindById(identify);
            action.Invoke(entity);
            _context.Update(entity);
        }

        public async Task UpdateByIdAsync(TIdentify identify, Action<TEntity> action)
        {
            var entity = await FindByIdAsync(identify);
            action.Invoke(entity);
            _context.Update(entity);
        }
        #endregion

        #region Remove
        public void Remove(Expression<Func<TEntity, bool>> where = null)
        {
            _context.RemoveRange(_store.Where(where));
        }

        public void Remove(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                _context.Remove(entity);
            });       
        }

        public async Task RemoveAsync(Expression<Func<TEntity, bool>> where)
        {
            await Task.Run(() =>
            {
                _context.RemoveRange(_store.Where(where));
            });
        }

        public void RemoveById(TIdentify identify)
        {
            _context.Remove(FindById(identify));
        }

        public async Task RemoveByIdAsync(TIdentify identify)
        {
            _context.Remove(await FindByIdAsync(identify));
        }
        #endregion
    }
}
