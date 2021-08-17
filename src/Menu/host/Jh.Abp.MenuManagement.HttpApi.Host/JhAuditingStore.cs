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
        public JhAuditingStore(IAuditLogRepository auditLogRepository, IUnitOfWorkManager unitOfWorkManager, IOptions<AbpAuditingOptions> options, IAuditLogInfoToAuditLogConverter converter) : base(auditLogRepository, unitOfWorkManager, options, converter)
        {
        }

        protected async override Task SaveLogAsync(AuditLogInfo auditInfo)
        {
            //去除DisableAuditing的请求，带DisableAuditing的Actionos都是空的
            if (auditInfo.Actions.Count == 0)
            {
                return;
            }
            //去除没有必要的数据存储
            if (auditInfo.Url != null &&
                auditInfo.Url.StartsWith("/api/")//TODO:可以改为正则表达式匹配，使用appsettings配置
                && auditInfo.Actions.Where(a => a.Parameters != "{}").Any())
            {
                await base.SaveLogAsync(auditInfo);
            }
        }
    }
}
