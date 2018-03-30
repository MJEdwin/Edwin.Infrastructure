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
        private DbSet<TEntity> _store => _context.Set<TEntity>();

        public EntityFrameworkRepository(TContext context)
        {
            _context = context;
        }

        #region Query
        public IQueryable<TEntity> FindAll() => _store;

        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> where = null) => _store.Where(where);

        public TEntity FindById(TPrimaryKey key) => _store.First(e => e.Id.Equals(key));

        public Task<TEntity> FindByIdAsync(TPrimaryKey key) => _store.FirstAsync(e => e.Id.Equals(key));

        public TEntity Find(Expression<Func<TEntity, bool>> where = null) => _store.First(where);

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> where = null) => _store.FirstAsync(where);

        public TEntity FindOrDefaultById(TPrimaryKey key) => _store.FirstOrDefault(e => e.Id.Equals(key));

        public Task<TEntity> FindOrDefaultByIdAsync(TPrimaryKey key) => _store.FirstOrDefaultAsync(e => e.Id.Equals(key));

        public TEntity FindOrDefault(Expression<Func<TEntity, bool>> where = null) => _store.FirstOrDefault(where);

        public Task<TEntity> FindOrDefaultAsync(Expression<Func<TEntity, bool>> where = null) => _store.FirstOrDefaultAsync(where);

        public int Count() => _store.Count();

        public int Count(Expression<Func<TEntity, bool>> where = null) => _store.Count(where);

        public long LongCount() => _store.LongCount();

        public long LongCount(Expression<Func<TEntity, bool>> where = null) => _store.LongCount(where);

        public bool Exist(TEntity entity) => _store.Any(s => s == entity);

        public bool Exist(Expression<Func<TEntity, bool>> where) => _store.Any(where);
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

        public async Task InsertAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            var entry = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entry.Entity.Id;
        }
        #endregion

        #region Update
        public void Update(TEntity entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(TPrimaryKey key, Action<TEntity> action)
        {
            var entity = FindById(key);
            action.Invoke(entity);
            _context.Update(entity);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(TPrimaryKey key, Action<TEntity> action)
        {
            var entity = await FindByIdAsync(key);
            action.Invoke(entity);
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public void Delete(Expression<Func<TEntity, bool>> where = null)
        {
            _context.RemoveRange(_store.Where(where));
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

        public async Task DeleteByIdAsync(TPrimaryKey key)
        {
            _context.Remove(await FindByIdAsync(key));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            _context.RemoveRange(_store.Where(where));
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
