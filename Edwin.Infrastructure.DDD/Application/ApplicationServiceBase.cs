using Edwin.Infrastructure.AutoMapper;
using Edwin.Infrastructure.Query;
using Edwin.Infrastructure.Core.Serializer;
using Edwin.Infrastructure.DDD.UnitOfWork;
using Edwin.Infrastructure.DDD.Domian;
using Edwin.Infrastructure.DDD.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edwin.Infrastructure.DDD.Application
{
    public class ApplicationServiceBase<TEntity, TPrimaryKey> : IApplicationService<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        protected IRepository<TEntity, TPrimaryKey> _repository;
        protected IUnitOfWorkManager _manager;

        public ApplicationServiceBase(IRepository<TEntity, TPrimaryKey> repository, IUnitOfWorkManager manager)
        {
            _repository = repository;
            _manager = manager;
        }

        #region Query
        public IQueryable<TEntity> GetAll()
            => _repository.FindAll();

        public IQueryable<TEntity> GetAll(params IFilter<TEntity>[] filters)
        {
            var query = GetAll();
            foreach (var filter in filters)
            {
                query = filter.Query(query);
            }
            return query;
        }

        public bool Exist(TPrimaryKey id)
            => _repository.FindOrDefaultById(id) != null;

        public TEntity GetById(TPrimaryKey id)
            => _repository.FindById(id);
        #endregion

        #region Command
        public TEntity Create<TDTO>(TDTO dto)
        {
            try
            {
                using (var unitOfWork = _manager.Begin())
                {
                    var entity = dto.MapTo<TDTO, TEntity>();
                    _repository.Insert(entity);
                    return entity;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public TEntity Create(Dictionary<string, object> dictionary)
        {
            using (var unitOfWork = _manager.Begin())
            {
                var entity = new DictionarySerializer<TEntity>(CompareWay.ToLower)
                    .Deserialize(dictionary);
                _repository.Insert(entity);
                return entity;
            }
        }

        public TEntity Upgrade<TDTO>(TPrimaryKey id, TDTO dto)
        {
            using (var unitOfWork = _manager.Begin())
            {
                var entity = GetById(id);
                _repository.Update(dto.MapTo(entity));
                return entity;
            }
        }

        public TEntity Upgrade(TPrimaryKey id, Dictionary<string, object> dictionary)
        {
            using (var unitOfWork = _manager.Begin())
            {
                var entity = GetById(id);
                entity.Update(dictionary);
                _repository.Update(entity);
                return entity;
            }

        }

        public void Delete(params TPrimaryKey[] id)
        {
            using (var unitOfWork = _manager.Begin())
            {
                foreach (var key in id)
                {
                    _repository.DeleteById(key);
                }
            }

        }
        #endregion
    }
}
