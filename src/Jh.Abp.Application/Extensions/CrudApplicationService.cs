using Jh.Abp.Common.Entity;
using Jh.Abp.Common.Linq;
using Jh.Abp.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> CreateAsync(TCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entitys = ObjectMapper.Map<TCreateInputDto[], TEntity[]>(inputDtos);
            return await crudRepository.CreateAsync(entitys, autoSave,cancellationToken);
        }

        public async Task<TEntity> CreateAsync(TCreateInputDto inputDto, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = ObjectMapper.Map<TCreateInputDto, TEntity>(inputDto);
            return await Repository.InsertAsync(entity, autoSave, cancellationToken);
        }

        public async Task DeleteAsync(TDeleteInputDto deleteInputDto, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var lambda = LinqExpression.ConvetToExpression<TDeleteInputDto, TEntity>(deleteInputDto);
            await Repository.DeleteAsync(lambda, autoSave, cancellationToken);
        }

        public async Task DeleteAsync(TKey[] keys, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Repository.DeleteAsync(a => keys.Contains(a.Id), autoSave, cancellationToken);
        }

        public async Task<List<TEntityDto>> GetEntitysAsync(TRetrieveInputDto inputDto)
        {
            var lambda = LinqExpression.ConvetToExpression<TRetrieveInputDto, TEntity>(inputDto);
            var query = ReadOnlyRepository.Where(lambda);
            var entities = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<TEntity>, List<TEntityDto>>(entities);
        }

        public async Task<TEntity> UpdatePortionAsync(TKey key, TUpdateInputDto updateInput, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await Repository.GetAsync(key);
            EntityOperator.UpdatePortionToEntity(updateInput, entity);
            return await Repository.UpdateAsync(entity,autoSave,cancellationToken);
        }
    }
}
