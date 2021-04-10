using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Expressions;
using Volo.Abp.Uow;
using Volo.Abp.DependencyInjection;
using Volo.Abp.AuditLogging;
using System.Net;
using Jh.Abp.Common.Linq;
using Jh.Abp.Application.Contracts.Extensions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace Jh.Abp.MenuManagement
{
    [DisableAuditing]
    public class AuditLoggingAppService : MenuManagementAppService,IAuditLoggingAppService, ITransientDependency
    {
        public IAuditLoggingRepository auditLogsRepository { get; set; }
        protected IReadOnlyRepository<AuditLog> ReadOnlyRepository
        {
            get;
        }
        public Task<AuditLog[]> DeleteAsync(AuditLoggingDeleteInputDto deleteInputDto, string methodStringType = ObjectMethodConsts.EqualsMethod, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var lambda = LinqExpression.ConvetToExpression<AuditLoggingDeleteInputDto, AuditLog>(deleteInputDto, methodStringType);
            var query = ReadOnlyRepository.Where(lambda);
            return auditLogsRepository.DeleteEntitysAsync(query, autoSave, cancellationToken);
        }

        public async Task<PagedResultDto<AuditLog>> GetListAsync(AuditLoggingRetrieveInputDto retrieveInputDto, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            var datas = await auditLogsRepository.GetListAsync(retrieveInputDto.Sorting, retrieveInputDto.MaxResultCount, retrieveInputDto.SkipCount
                , retrieveInputDto.startTime, retrieveInputDto.endTime, retrieveInputDto.httpMethod, retrieveInputDto.url, retrieveInputDto.userName
                , retrieveInputDto.applicationName, retrieveInputDto.correlationId, retrieveInputDto.maxExecutionDuration, retrieveInputDto.minExecutionDuration
                , retrieveInputDto.hasException, retrieveInputDto.httpStatusCode, includeDetails, cancellationToken);
            var totalCount = await auditLogsRepository.GetCountAsync(retrieveInputDto.startTime, retrieveInputDto.endTime, retrieveInputDto.httpMethod, retrieveInputDto.url, retrieveInputDto.userName
                , retrieveInputDto.applicationName, retrieveInputDto.correlationId, retrieveInputDto.maxExecutionDuration, retrieveInputDto.minExecutionDuration
                , retrieveInputDto.hasException, retrieveInputDto.httpStatusCode,  cancellationToken);

            return new PagedResultDto<AuditLog>()
            {
                Items = datas,
                TotalCount = totalCount
            };
        }

        public virtual async Task<AuditLog[]> DeleteAsync(Guid[] keys, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await auditLogsRepository.DeleteListAsync(a => keys.Contains(a.Id), autoSave, cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<AuditLog> DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await auditLogsRepository.DeleteListAsync(a => a.Id.Equals(id), autoSave, cancellationToken).ConfigureAwait(false)).FirstOrDefault();
        }

        public async Task<ListResultDto<AuditLog>> GetEntitysAsync(AuditLoggingRetrieveInputDto retrieveInputDto, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            var datas = await auditLogsRepository.GetListAsync(retrieveInputDto.Sorting, retrieveInputDto.MaxResultCount, retrieveInputDto.SkipCount
              , retrieveInputDto.startTime, retrieveInputDto.endTime, retrieveInputDto.httpMethod, retrieveInputDto.url, retrieveInputDto.userName
              , retrieveInputDto.applicationName, retrieveInputDto.correlationId, retrieveInputDto.maxExecutionDuration, retrieveInputDto.minExecutionDuration
              , retrieveInputDto.hasException, retrieveInputDto.httpStatusCode,  includeDetails, cancellationToken);
            return new ListResultDto<AuditLog>() {
                Items = datas
            };
        }
    }
}