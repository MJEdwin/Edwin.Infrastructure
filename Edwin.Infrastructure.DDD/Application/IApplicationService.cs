using Edwin.Infrastructure.Query;
using Edwin.Infrastructure.DDD.Domian;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edwin.Infrastructure.DDD.Application
{
    public interface IApplicationService<TEntity, TPrimaryKey>
        where TEntity : IEntity<TPrimaryKey>
    {
        #region Query
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(params IFilter<TEntity>[] filters);

        bool Exist(TPrimaryKey id);

        TEntity GetById(TPrimaryKey id);
        #endregion

        #region Command
        TEntity Create<TDTO>(TDTO dto);
        TEntity Create(Dictionary<string, object> dictionary);
        TEntity Upgrade<TDTO>(TPrimaryKey id, TDTO dto);
        TEntity Upgrade(TPrimaryKey id, Dictionary<string, object> dictionary);
        void Delete(params TPrimaryKey[] id);
        #endregion
    }
}
