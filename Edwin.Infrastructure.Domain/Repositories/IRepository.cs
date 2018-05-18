using Edwin.Infrastructure.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.Domain.Repositories
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        #region Queries
        /// <summary>
        /// 返回仓储实体集合
        /// </summary>
        /// <returns>实体集合</returns>
        IQueryable<TAggregateRoot> FindAll();
        /// <summary>
        /// 依据条件返回仓储实体集合
        /// </summary>
        /// <param name="where">条件lambda</param>
        /// <returns>数量</returns>
        IQueryable<TAggregateRoot> FindAll(Expression<Func<TAggregateRoot, bool>> where = null);
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
        int Count(Expression<Func<TAggregateRoot, bool>> where = null);
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
        long LongCount(Expression<Func<TAggregateRoot, bool>> where = null);
        /// <summary>
        /// 根据条件查找实体,无结果则抛出异常
        /// </summary>
        /// <param name="where">条件lambda</param>
        /// <returns>实体</returns>
        /// <exception cref="InvalidOperationException">未找到实体</exception>
        TAggregateRoot Find(Expression<Func<TAggregateRoot, bool>> where = null);
        /// <summary>
        /// 根据条件异步查找实体,无结果则抛出异常
        /// </summary>
        /// <param name="where">条件</param>
        /// <exception cref="InvalidOperationException">未找到实体</exception>
        /// <returns>实体</returns>
        Task<TAggregateRoot> FindAsync(Expression<Func<TAggregateRoot, bool>> where = null);
        /// <summary>
        /// 根据条件查找实体,如无结果,则返回default(TEntity)
        /// </summary>
        /// <param name="where">条件lambda</param>
        /// <returns>实体</returns>
        TAggregateRoot FindOrDefault(Expression<Func<TAggregateRoot, bool>> where = null);
        /// <summary>
        /// 根据条件异步查找实体,如无结果,则返回default(TEntity)
        /// </summary>
        /// <param name="where">条件lambda</param>
        /// <returns>实体</returns>
        Task<TAggregateRoot> FindOrDefaultAsync(Expression<Func<TAggregateRoot, bool>> where = null);
        /// <summary>
        /// 查找实体是否在仓储中
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>存在标识</returns>
        bool Exist(TAggregateRoot entity);
        /// <summary>
        /// 根据条件查找实体是否在仓储中
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>存在标识</returns>
        bool Exist(Expression<Func<TAggregateRoot, bool>> where);
        #endregion

        #region Add
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体</returns>
        TAggregateRoot Add(TAggregateRoot entity);
        /// <summary>
        /// 异步插入实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task<TAggregateRoot> AddAsync(TAggregateRoot entity);
        #endregion

        #region Update
        /// <summary>
        /// 根据实体更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(TAggregateRoot entity);

        /// <summary>
        /// 根据实体异步更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task UpdateAsync(TAggregateRoot entity);
        #endregion

        #region Remove
        /// <summary>
        /// 根据实体删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Remove(TAggregateRoot entity);
        /// <summary>
        /// 根据实体异步删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task RemoveAsync(TAggregateRoot entity);
        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="where">条件</param>
        void Remove(Expression<Func<TAggregateRoot, bool>> where = null);
        /// <summary>
        /// 根据条件异步删除实体
        /// </summary>
        /// <param name="where">条件</param>
        Task RemoveAsync(Expression<Func<TAggregateRoot, bool>> where = null);
        #endregion
    }
}
