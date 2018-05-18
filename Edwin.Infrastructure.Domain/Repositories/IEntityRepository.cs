using Edwin.Infrastructure.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.Domain.Repositories
{
    public interface IEntityRepository<TEntity, TIdentify> : IRepository<TEntity> 
        where TEntity : IAggregateRoot, IEntity<TIdentify>
    {
        #region Queries
        /// <summary>
        /// 根据标识寻找实体,如无则抛出异常
        /// </summary>
        /// <param name="identify">标识</param>
        /// <returns>实体</returns>
        TEntity FindById(TIdentify identify);
        /// <summary>
        /// 根据标识异步寻找实体,如无则抛出异常
        /// </summary>
        /// <param name="identify">标识</param>
        /// <returns>实体</returns>
        Task<TEntity> FindByIdAsync(TIdentify identify);
        /// <summary>
        /// 根据标识寻找实体,如无则返回默认值
        /// </summary>
        /// <param name="identify">标识</param>
        /// <returns>实体</returns>
        TEntity FindOrDefaultById(TIdentify identify);
        /// <summary>
        /// 根据标识异步寻找实体,如无则返回默认值
        /// </summary>
        /// <param name="identify">标识</param>
        /// <returns>实体</returns>
        Task<TEntity> FindOrDefaultByIdAsync(TIdentify identify);
        #endregion

        #region Update
        void UpdateById(TIdentify identify, Action<TEntity> action);

        Task UpdateByIdAsync(TIdentify identify, Action<TEntity> action);
        #endregion

        #region Remove
        void RemoveById(TIdentify identify);

        Task RemoveByIdAsync(TIdentify identify);
        #endregion
    }
}
