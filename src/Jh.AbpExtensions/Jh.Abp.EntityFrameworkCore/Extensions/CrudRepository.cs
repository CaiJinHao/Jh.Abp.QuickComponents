using Jh.Abp.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Jh.Abp.EntityFrameworkCore.Extensions
{
    public abstract class CrudRepository<TDbContext, TEntity, TKey> : EfCoreRepository<TDbContext, TEntity, TKey>, ICrudRepository<TEntity, TKey>
        where TDbContext : IEfCoreDbContext
        where TEntity : class, IEntity<TKey>
    {
        protected CrudRepository(IDbContextProvider<TDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public virtual async Task<TEntity[]> CreateAsync(TEntity[] entitys, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            //使用SqlBulk
            await (await GetDbSetAsync()).AddRangeAsync(entitys).ConfigureAwait(false);
            if (autoSave)
            {
                await (await GetDbContextAsync()).SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
            }
            return entitys;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await (await GetDbSetAsync()).AddAsync(entity).ConfigureAwait(false);
            if (autoSave)
            {
                await (await GetDbContextAsync()).SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
            }
            return entity;
        }

        public virtual async Task<TEntity[]> DeleteListAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var _dbSet = await GetDbSetAsync();
            var entitys = _dbSet.Where(predicate).ToArray();
            _dbSet.RemoveRange(entitys);
            if (autoSave)
            {
                await (await GetDbContextAsync()).SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
            }
            return entitys;
        }

        public virtual async Task<TEntity[]> DeleteEntitysAsync(IQueryable<TEntity> query, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entitys = query.ToArray();
            (await GetDbSetAsync()).RemoveRange(entitys);
            if (autoSave)
            {
                await (await GetDbContextAsync()).SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
            }
            return entitys;
        }

        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> propertyQuerys, 
            int maxResultCount = int.MaxValue,
            int skipCount = 0, bool includeDetails = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var queryable = includeDetails
                 ? await WithDetailsAsync()
                 : await GetDbSetAsync();

            return await queryable.WhereIf(propertyQuerys!=null, propertyQuerys)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(Expression<Func<TEntity, bool>> propertyQuerys, CancellationToken cancellationToken = default(CancellationToken))
        {
            var queryable = await GetDbSetAsync();
            //TODO:会抛出异常 测试完成之后更新CrudAppService
            return await queryable.WhereIf(propertyQuerys != null, propertyQuerys)
                .LongCountAsync(cancellationToken);
        }
    }
}
