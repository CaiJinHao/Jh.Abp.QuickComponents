using Jh.Abp.Application.Contracts.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;

namespace Jh.Abp.MenuManagement
{
    public interface IAuditLoggingAppService
    {
        Task<ListResultDto<AuditLog>> GetEntitysAsync(AuditLoggingRetrieveInputDto inputDto, bool includeDetails = false, CancellationToken cancellationToken = default(CancellationToken));
        Task<PagedResultDto<AuditLog>> GetListAsync(AuditLoggingRetrieveInputDto retrieveInputDto, bool includeDetails = false, CancellationToken cancellationToken = default);
        Task<AuditLog[]> DeleteAsync(AuditLoggingDeleteInputDto deleteInputDto, string methodStringType = ObjectMethodConsts.EqualsMethod, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));
        Task<AuditLog[]> DeleteAsync(Guid[] keys, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));
        Task<AuditLog> DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));
    }
}
