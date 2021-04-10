using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Uow;

namespace Jh.Abp.MenuManagement
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IAuditingStore))]
    public class JhAuditingStore : AuditingStore, IAuditingStore, ITransientDependency
    {
        public JhAuditingStore(IAuditLogRepository auditLogRepository, IGuidGenerator guidGenerator, IUnitOfWorkManager unitOfWorkManager, IOptions<AbpAuditingOptions> options) : base(auditLogRepository, guidGenerator, unitOfWorkManager, options)
        {
        }

        protected async override Task SaveLogAsync(AuditLogInfo auditInfo)
        {
            if (auditInfo.Actions.Count == 0)
            {
                return;
            }

            if (auditInfo.Url.StartsWith("/api/"))
            {
                await base.SaveLogAsync(auditInfo);
            }
        }
    }
}
