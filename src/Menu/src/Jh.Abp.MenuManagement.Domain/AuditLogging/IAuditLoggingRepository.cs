using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.AuditLogging;

namespace Jh.Abp.MenuManagement
{
    public interface IAuditLoggingRepository:IAuditLogRepository
    {
        Task<AuditLog[]> DeleteEntitysAsync(IQueryable<AuditLog> query, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));
        Task<AuditLog[]> DeleteListAsync(Expression<Func<AuditLog, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));
    }
}
