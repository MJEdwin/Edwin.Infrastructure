using Edwin.Infrastructure.AutoMapper;
using Edwin.Infrastructure.Query;
using Edwin.Infrastructure.Serializer;
using Edwin.Infrastructure.Domain.UnitOfWork;
using Edwin.Infrastructure.Domain.Domain;
using Edwin.Infrastructure.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Edwin.Infrastructure.Domain.Application
{
    public class ApplicationServiceBase<TEntity, TPrimaryKey> : IApplicationService<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>,IAggregateRoot
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        protected IEntityRepository<TEntity, TPrimaryKey> _repository;
        protected IUnitOfWorkManager _manager;
        protected ILogger<ApplicationServiceBase<TEntity, TPrimaryKey>> _logger;

        public ApplicationServiceBase(IEntityRepository<TEntity, TPrimaryKey> repository, IUnitOfWorkManager manager, ILogger<ApplicationServiceBase<TEntity, TPrimaryKey>> logger)
        {
            _repository = repository;
            _manager = manager;
            _logger = logger;
        }

        #region Queries
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
            => _repository.Exist(entity => entity.Id.Equals(id));

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
                    entity = _repository.Add(entity);
                    unitOfWork.Complete();
                    return entity;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Action {nameof(this.Create)} Error");
                throw e;
            }

        }

        public TEntity Create(Dictionary<string, object> dictionary)
        {
            try
            {
                using (var unitOfWork = _manager.Begin())
                {
                    var entity = new DictionarySerializer<TEntity>(CompareWay.ToLower)
                        .Deserialize(dictionary);
                    _repository.Add(entity);
                    unitOfWork.Complete();
                    return entity;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Action {nameof(this.Create)} Error");
                throw e;
            }

        }

        public TEntity Upgrade<TDTO>(TPrimaryKey id, TDTO dto)
        {
            try
            {
                using (var unitOfWork = _manager.Begin())
                {
                    var entity = GetById(id);
                    _repository.Update(dto.MapTo(entity));
                    unitOfWork.Complete();
                    return entity;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Action {nameof(this.Upgrade)} Error");
                throw e;
            }

        }

        public TEntity Upgrade(TPrimaryKey id, Dictionary<string, object> dictionary)
        {
            try
            {
                using (var unitOfWork = _manager.Begin())
                {
                    var entity = GetById(id);
                    entity.ChangeProperties(dictionary);
                    _repository.Update(entity);
                    unitOfWork.Complete();
                    return entity;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Action {nameof(this.Upgrade)} Error");
                throw e;
            }

        }

        public void Delete(params TPrimaryKey[] id)
        {
            try
            {
                using (var unitOfWork = _manager.Begin())
                {
                    foreach (var key in id)
                    {
                        _repository.RemoveById(key);
                    }
                    unitOfWork.Complete();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Action {nameof(this.Delete)} Error");
                throw e;
            }
        }
        #endregion
    }
}
