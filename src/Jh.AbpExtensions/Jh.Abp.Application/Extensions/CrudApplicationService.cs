using Jh.Abp.Application.Contracts.Dtos;
using Jh.Abp.Application.Contracts.Extensions;
using Jh.Abp.Common.Entity;
using Jh.Abp.Common.Linq;
using Jh.Abp.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Jh.Abp.Extensions
{
    public abstract class CrudApplicationService<TEntity, TEntityDto, TPagedRetrieveOutputDto, TKey, TRetrieveInputDto, TCreateInputDto, TUpdateInputDto, TDeleteInputDto>
        : CrudAppService<TEntity, TEntityDto, TPagedRetrieveOutputDto, TKey, TRetrieveInputDto, TCreateInputDto, TUpdateInputDto>
        , ICrudApplicationService<TEntity, TEntityDto, TPagedRetrieveOutputDto, TKey, TRetrieveInputDto, TCreateInputDto, TUpdateInputDto, TDeleteInputDto>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
        where TPagedRetrieveOutputDto : IEntityDto<TKey>
    {
        public IDataFilter DataFilter { get; set; }

        public ICrudRepository<TEntity, TKey> crudRepository;

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

        public virtual async Task<TEntity[]> DeleteAsync(TDeleteInputDto deleteInputDto, string methodStringType = ObjectMethodConsts.EqualsMethod, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckDeletePolicyAsync().ConfigureAwait(false);
            var query = await CreateFilteredQueryAsync(deleteInputDto, methodStringType);
            return await crudRepository.DeleteEntitysAsync(query, autoSave, cancellationToken).ConfigureAwait(false);
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

        public virtual async Task<ListResultDto<TEntityDto>> GetEntitysAsync(TRetrieveInputDto inputDto, string methodStringType = ObjectMethodConsts.ContainsMethod, bool includeDetails = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckGetListPolicyAsync().ConfigureAwait(false);
            var query = CreateFilteredQuery(await crudRepository.GetQueryableAsync(includeDetails), inputDto, methodStringType);
            query = ApplySorting(query, inputDto);
            query = ApplyPaging(query, inputDto);
            var entities = await query.ToListAsync(cancellationToken);
            return new ListResultDto<TEntityDto>(
                 ObjectMapper.Map<List<TEntity>, List<TEntityDto>>(entities)
            );
        }

        public virtual async Task<TEntityDto> GetAsync(TKey id, bool includeDetails = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckGetPolicyAsync().ConfigureAwait(false);

            var entity = await crudRepository.GetAsync(id, includeDetails, cancellationToken);

            return await MapToGetOutputDtoAsync(entity);
        }

        [Obsolete("请使用 GetEntitysAsync includeDetails")]
        public virtual async Task<ListResultDto<TEntityDto>> GetEntitysAsync(TRetrieveInputDto inputDto, string methodStringType = ObjectMethodConsts.ContainsMethod, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckGetListPolicyAsync().ConfigureAwait(false);
            var query = await CreateFilteredQueryAsync(inputDto, methodStringType);
            query = ApplySorting(query, inputDto);
            query = ApplyPaging(query, inputDto);
            var entities = await AsyncExecuter.ToListAsync(query, cancellationToken).ConfigureAwait(false);
            return new ListResultDto<TEntityDto>(
                 ObjectMapper.Map<List<TEntity>, List<TEntityDto>>(entities)
            );
        }

        public virtual async Task<PagedResultDto<TPagedRetrieveOutputDto>> GetListAsync(TRetrieveInputDto input, string methodStringType = ObjectMethodConsts.ContainsMethod, bool includeDetails = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckGetListPolicyAsync().ConfigureAwait(false);

            var query = CreateFilteredQuery(await crudRepository.GetQueryableAsync(includeDetails), input, methodStringType);

            var totalCount = await query.LongCountAsync(cancellationToken);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync(cancellationToken);
            var entityDtos = await MapToGetListOutputDtosAsync(entities);

            return new PagedResultDto<TPagedRetrieveOutputDto>(
                totalCount,
                entityDtos
            );
        }

        [Obsolete("请使用 GetListAsync includeDetails")]
        public virtual async Task<PagedResultDto<TPagedRetrieveOutputDto>> GetListAsync(TRetrieveInputDto input, string methodStringType = ObjectMethodConsts.ContainsMethod, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckGetListPolicyAsync().ConfigureAwait(false);

            var query = await CreateFilteredQueryAsync(input, methodStringType);

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncExecuter.ToListAsync(query);
            var entityDtos = await MapToGetListOutputDtosAsync(entities);

            return new PagedResultDto<TPagedRetrieveOutputDto>(
                totalCount,
                entityDtos
            );
        }

        public virtual async Task<TEntity> UpdatePortionAsync(TKey key, TUpdateInputDto updateInput, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await CheckUpdatePolicyAsync().ConfigureAwait(false);
            var entity = await crudRepository.GetAsync(key).ConfigureAwait(false);
            EntityOperator.UpdatePortionToEntity(updateInput, entity);
            var methodDto = updateInput as IMethodDto<TEntity>;
            if (methodDto != null)
            {
                if (methodDto.MethodInput != null)
                {
                    if (methodDto.MethodInput.UpdateEntityAction != null)
                    {
                        methodDto.MethodInput.UpdateEntityAction(entity);
                    }
                }
            }
            return await crudRepository.UpdateAsync(entity, autoSave, cancellationToken).ConfigureAwait(false);
        }

        public override async Task<TEntityDto> UpdateAsync(TKey id, TUpdateInputDto updateInput)
        {
            await CheckUpdatePolicyAsync().ConfigureAwait(false);
            var entity = await GetEntityByIdAsync(id);
            await MapToEntityAsync(updateInput, entity);
            var methodDto = updateInput as IMethodDto<TEntity>;
            if (methodDto != null)
            {
                if (methodDto.MethodInput != null)
                {
                    if (methodDto.MethodInput.UpdateEntityAction != null)
                    {
                        methodDto.MethodInput.UpdateEntityAction(entity);
                    }
                }
            }
            await Repository.UpdateAsync(entity, autoSave: true);
            return await MapToGetOutputDtoAsync(entity);
        }

        protected override async Task<IQueryable<TEntity>> CreateFilteredQueryAsync(TRetrieveInputDto inputDto)
        {
            return await CreateFilteredQueryAsync(inputDto, ObjectMethodConsts.ContainsMethod);
        }

        protected virtual async Task<IQueryable<TEntity>> CreateFilteredQueryAsync<TWhere>(TWhere inputDto, string methodStringType)
        {
            return CreateFilteredQuery(await ReadOnlyRepository.GetQueryableAsync(), inputDto, methodStringType);
        }

        protected virtual IQueryable<TEntity> CreateFilteredQuery<TWhere>(IQueryable<TEntity> queryable,TWhere inputDto, string methodStringType)
        {
            var lambda = LinqExpression.ConvetToExpression<TWhere, TEntity>(inputDto, methodStringType);
            var query = queryable.Where(lambda);
            var methodDto = inputDto as IMethodDto<TEntity>;
            if (methodDto != null)
            {
                if (methodDto.MethodInput != null)
                {
                    if (methodDto.MethodInput.QueryAction != null)
                    {
                        query = methodDto.MethodInput.QueryAction(query);
                    }
                }
            }
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                var retrieveDelete = inputDto as IRetrieveDelete;
                if (retrieveDelete != null)
                {
                    if (retrieveDelete.Deleted != null)
                    {
                        switch (retrieveDelete.Deleted)
                        {
                            case 1:
                                {
                                    query = query.Where(e => ((ISoftDelete)e).IsDeleted == true);
                                }
                                break;
                            case 2:
                                {
                                    query = query.Where(e => ((ISoftDelete)e).IsDeleted == false);
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return query;
        }
    }
}
