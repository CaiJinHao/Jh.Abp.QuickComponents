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

        public async Task<TEntity[]> CreateAsync(TEntity[] entitys, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            //使用SqlBulk
            await DbSet.AddRangeAsync(entitys).ConfigureAwait(false);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
            }
            return entitys;
        }

        public  async Task<TEntity[]> DeleteListAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entitys = DbSet.Where(predicate).ToArray();
            DbSet.RemoveRange(entitys);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken)).ConfigureAwait(false);
            }
            return entitys;
        }
    }
}
