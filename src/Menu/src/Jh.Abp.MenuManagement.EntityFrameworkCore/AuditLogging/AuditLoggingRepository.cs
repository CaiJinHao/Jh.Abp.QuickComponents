using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Jh.Abp.MenuManagement
{
    public class AuditLoggingRepository : EfCoreAuditLogRepository, IAuditLoggingRepository, ITransientDependency
    {
        public AuditLoggingRepository(IDbContextProvider<IAuditLoggingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public virtual async Task<AuditLog[]> DeleteEntitysAsync(IQueryable<AuditLog> query, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entitys = query.ToArray();
            (await GetDbSetAsync()).RemoveRange(entitys);
            if (autoSave)
            {
                await (await GetDbContextAsync()).SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            return entitys;
        }

        public virtual async Task<AuditLog[]> DeleteListAsync(Expression<Func<AuditLog, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var _dbSet = await GetDbSetAsync();
            var entitys = _dbSet.Where(predicate).ToArray();
            _dbSet.RemoveRange(entitys);
            if (autoSave)
            {
                await (await GetDbContextAsync()).SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            return entitys;
        }
    }
}
