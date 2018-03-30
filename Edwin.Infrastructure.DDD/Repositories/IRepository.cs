using Edwin.Infrastructure.DDD.Domian;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.DDD.Repositories
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        #region Query
        /// <summary>
        /// 返回仓储实体集合
        /// </summary>
        /// <returns>实体集合</returns>
        IQueryable<TEntity> FindAll();
        /// <summary>
        /// 依据条件返回仓储实体集合
        /// </summary>
        /// <param name="where">条件lambda</param>
        /// <returns>数量</returns>
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> where = null);
        /// <summary>
        /// 返回仓储数量,长整型表示
        /// </summary>
        /// <returns>数量</returns>
        int Count();
        /// <summary>
        /// 依据条件返回仓储数量
        /// </summary>
        /// <param name="where">条件lambda</param>
        /// <returns>数量</returns>
        int Count(Expression<Func<TEntity, bool>> where = null);
        /// <summary>
        /// 返回仓储数量,长整型表示
        /// </summary>
        /// <returns>数量</returns>
        long LongCount();
        /// <summary>
        /// 依据条件返回仓储数量,长整型表示
        /// </summary>
        /// <param name="where">条件lambda</param>
        /// <returns>数量</returns>
        long LongCount(Expression<Func<TEntity, bool>> where = null);
        /// <summary>
        /// 根据标识查找实体,无结果则抛出异常
        /// </summary>
        /// <param name="key">标识</param>
        /// <returns>实体</returns>
        /// <exception cref="InvalidOperationException">未找到实体</exception>
        TEntity FindById(TPrimaryKey key);
        /// <summary>
        /// 根据标识异步查找实体,无结果则抛出异常
        /// </summary>
        /// <param name="key">标识</param>
        /// <returns></returns>
        Task<TEntity> FindByIdAsync(TPrimaryKey key);
        /// <summary>
        /// 根据条件查找实体,无结果则抛出异常
        /// </summary>
        /// <param name="where">条件lambda</param>
        /// <returns>实体</returns>
        /// <exception cref="InvalidOperationException">未找到实体</exception>
        TEntity Find(Expression<Func<TEntity, bool>> where = null);
        /// <summary>
        /// 根据条件异步查找实体,无结果则抛出异常
        /// </summary>
        /// <param name="where">条件</param>
        /// <exception cref="InvalidOperationException">未找到实体</exception>
        /// <returns>实体</returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> where = null);
        /// <summary>
        /// 根据标识查找实体,如无结果,则返回default(TEntity)
        /// </summary>
        /// <param name="key">标识</param>
        /// <returns>实体</returns>
        TEntity FindOrDefaultById(TPrimaryKey key);
        /// <summary>
        /// 根据标识异步查找实体,如无结果,则返回default(TEntity)
        /// </summary>
        /// <param name="key">标识</param>
        /// <returns>实体</returns>
        Task<TEntity> FindOrDefaultByIdAsync(TPrimaryKey key);
        /// <summary>
        /// 根据条件查找实体,如无结果,则返回default(TEntity)
        /// </summary>
        /// <param name="where">条件lambda</param>
        /// <returns>实体</returns>
        TEntity FindOrDefault(Expression<Func<TEntity, bool>> where = null);
        /// <summary>
        /// 根据条件异步查找实体,如无结果,则返回default(TEntity)
        /// </summary>
        /// <param name="where">条件lambda</param>
        /// <returns>实体</returns>
        Task<TEntity> FindOrDefaultAsync(Expression<Func<TEntity, bool>> where = null);
        /// <summary>
        /// 查找实体是否在仓储中
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>存在标识</returns>
        bool Exist(TEntity entity);
        /// <summary>
        /// 根据条件查找实体是否在仓储中
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>存在标识</returns>
        bool Exist(Expression<Func<TEntity, bool>> where);
        #endregion

        #region Insert
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Insert(TEntity entity);
        /// <summary>
        /// 插入实体并获取标识
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>标识</returns>
        TPrimaryKey InsertAndGetId(TEntity entity);
        /// <summary>
        /// 异步插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task InsertAsync(TEntity entity);
        /// <summary>
        /// 异步插入实体并获取标识
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>标识</returns>
        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);
        #endregion

        #region Delete
        /// <summary>
        /// 根据标识删除实体
        /// </summary>
        /// <param name="key">标识</param>
        void DeleteById(TPrimaryKey key);
        /// <summary>
        /// 根据标识异步删除实体
        /// </summary>
        /// <param name="key">标识</param>
        Task DeleteByIdAsync(TPrimaryKey key);
        /// <summary>
        /// 根据实体删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Delete(TEntity entity);
        /// <summary>
        /// 根据实体异步删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task DeleteAsync(TEntity entity);
        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="where">条件</param>
        void Delete(Expression<Func<TEntity, bool>> where = null);
        /// <summary>
        /// 根据条件异步删除实体
        /// </summary>
        /// <param name="where">条件</param>
        Task DeleteAsync(Expression<Func<TEntity, bool>> where = null);
        #endregion

        #region Update
        /// <summary>
        /// 根据实体更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(TEntity entity);

        /// <summary>
        /// 根据实体异步更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task UpdateAsync(TEntity entity);
        /// <summary>
        /// 根据操作更新实体
        /// </summary>
        /// <param name="key">标识</param>
        /// <param name="action">操作</param>
        void Update(TPrimaryKey key, Action<TEntity> action);
        /// <summary>
        /// 根据操作异步更新实体
        /// </summary>
        /// <param name="key">标识</param>
        /// <param name="action">操作</param>
        Task UpdateAsync(TPrimaryKey key, Action<TEntity> action);
        #endregion
    }
}
