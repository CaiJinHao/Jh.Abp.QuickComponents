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
using Jh.Abp.Common.Linq;
using System.Linq.Dynamic.Core;


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
                await (await GetDbContextAsync()).SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            return entitys;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            await (await GetDbSetAsync()).AddAsync(entity).ConfigureAwait(false);
            if (autoSave)
            {
                await (await GetDbContextAsync()).SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            return entity;
        }

        public virtual async Task<TEntity[]> DeleteListAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var _dbSet = await GetDbSetAsync();
            var entitys = _dbSet.WhereIf(predicate != null, predicate).ToArray();
            _dbSet.RemoveRange(entitys);
            if (autoSave)
            {
                await (await GetDbContextAsync()).SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            return entitys;
        }

        public virtual async Task<TEntity[]> DeleteEntitysAsync(IQueryable<TEntity> query, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entitys = query.ToArray();
            (await GetDbSetAsync()).RemoveRange(entitys);
            if (autoSave)
            {
                await (await GetDbContextAsync()).SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            return entitys;
        }

        public virtual async Task<IQueryable<TEntity>> GetQueryableAsync(bool includeDetails = false)
        { 
            return includeDetails ? await WithDetailsAsync(): await GetDbSetAsync();
        }
    }
}
