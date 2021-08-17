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

        public override async Task<long> GetCountAsync(
            DateTime? startTime = null,
            DateTime? endTime = null,
            string httpMethod = null,
            string url = null,
            Guid? userId= null,
            string userName = null,
            string applicationName = null,
            string correlationId = null,
            int? maxExecutionDuration = null,
            int? minExecutionDuration = null,
            bool? hasException = null,
            HttpStatusCode? httpStatusCode = null,
            CancellationToken cancellationToken = default)
        {

            var query = await GetListQueryAsync(
                startTime,
                endTime,
                httpMethod,
                url,
                userId,
                userName,
                applicationName,
                correlationId,
                maxExecutionDuration,
                minExecutionDuration,
                hasException,
                httpStatusCode
            );
#if DAMENG
            var totalCount = await query.CountAsync(GetCancellationToken(cancellationToken));
#else
            var totalCount = await query.LongCountAsync(GetCancellationToken(cancellationToken));
#endif

            return totalCount;
        }
    }
}
