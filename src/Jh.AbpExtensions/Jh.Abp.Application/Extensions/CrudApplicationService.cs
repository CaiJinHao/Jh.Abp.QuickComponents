using Jh.Abp.Common.Entity;
using Jh.Abp.Common.Linq;
using Jh.Abp.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Jh.Abp.Extensions
{
    public abstract class CrudApplicationService<TEntity, TEntityDto, TPagedRetrieveOutputDto, TKey, TRetrieveInputDto, TCreateInputDto, TUpdateInputDto, TDeleteInputDto>
        : CrudAppService<TEntity, TEntityDto, TPagedRetrieveOutputDto, TKey, TRetrieveInputDto, TCreateInputDto, TUpdateInputDto>
        , ICrudApplicationService<TEntity, TEntityDto, TPagedRetrieveOutputDto, TKey, TRetrieveInputDto, TCreateInputDto, TUpdateInputDto, TDeleteInputDto>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
        where TPagedRetrieveOutputDto : IEntityDto<TKey>
    {
        private ICrudRepository<TEntity, TKey> crudRepository;
        public CrudApplicationService(ICrudRepository<TEntity, TKey> repository) : base(repository)
        {
            crudRepository = repository;
        }

        public virtual async Task<TEntity[]> CreateAsync(TCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckCreatePolicyAsync().ConfigureAwait(false);
            var entitys = ObjectMapper.Map<TCreateInputDto[], TEntity[]>(inputDtos);
            return await crudRepository.CreateAsync(entitys, autoSave, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> CreateAsync(TCreateInputDto inputDto, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckCreatePolicyAsync().ConfigureAwait(false);
            var entity = ObjectMapper.Map<TCreateInputDto, TEntity>(inputDto);
            return await crudRepository.CreateAsync(entity, autoSave, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<TEntity[]> DeleteAsync(TDeleteInputDto deleteInputDto, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckDeletePolicyAsync().ConfigureAwait(false);
            var lambda = LinqExpression.ConvetToExpression<TDeleteInputDto, TEntity>(deleteInputDto);
            //queryFunc(lambda);
            return await crudRepository.DeleteListAsync(lambda, autoSave, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<TEntity[]> DeleteAsync(TKey[] keys, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckDeletePolicyAsync().ConfigureAwait(false);
            return await crudRepository.DeleteListAsync(a => keys.Contains(a.Id), autoSave, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckDeletePolicyAsync().ConfigureAwait(false);
            return (await crudRepository.DeleteListAsync(a => a.Id.Equals(id), autoSave, cancellationToken).ConfigureAwait(false)).FirstOrDefault();
        }

        public virtual async Task<List<TEntityDto>> GetEntitysAsync(TRetrieveInputDto inputDto, Action<IQueryable<TEntity>> queryFunc=null, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckGetListPolicyAsync().ConfigureAwait(false);
            var query = CreateFilteredQuery(inputDto, queryFunc);
            var entities = await AsyncExecuter.ToListAsync(query, cancellationToken).ConfigureAwait(false);
            return ObjectMapper.Map<List<TEntity>, List<TEntityDto>>(entities);
        }

        public virtual async Task<TEntity> UpdatePortionAsync(TKey key, TUpdateInputDto updateInput, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckUpdatePolicyAsync().ConfigureAwait(false);
            var entity = await crudRepository.GetAsync(key).ConfigureAwait(false);
            EntityOperator.UpdatePortionToEntity(updateInput, entity);
            return await crudRepository.UpdateAsync(entity, autoSave, cancellationToken).ConfigureAwait(false);
        }

        protected override IQueryable<TEntity> CreateFilteredQuery(TRetrieveInputDto inputDto)
        {
            var lambda = LinqExpression.ConvetToExpression<TRetrieveInputDto, TEntity>(inputDto);
            return ReadOnlyRepository.Where(lambda);
        }

        protected IQueryable<TEntity> CreateFilteredQuery(TRetrieveInputDto inputDto, Action<IQueryable<TEntity>> queryFunc)
        {
            var lambda = LinqExpression.ConvetToExpression<TRetrieveInputDto, TEntity>(inputDto);
            var query = ReadOnlyRepository.Where(lambda);
            if (queryFunc != null)
            {
                queryFunc(query);
            }
            return query;
        }
    }
}
