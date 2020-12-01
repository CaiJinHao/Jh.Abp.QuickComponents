using Jh.Abp.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jh.Abp.EntityFrameworkCore.Extensions
{
    public abstract class CrudRepository<TDbContext, TEntity, TKey> : EfCoreRepository<TDbContext, TEntity, TKey>, ICrudRepository<TEntity, TKey>
        where TDbContext : IEfCoreDbContext
        where TEntity : class, IEntity<TKey>
    {
        protected CrudRepository(IDbContextProvider<TDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<int> CreateAsync(TEntity[] entitys, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            //使用SqlBulk
            await DbSet.AddRangeAsync(entitys);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync(GetCancellationToken(cancellationToken));
            }
            return entitys.Length;
        }
    }
}
