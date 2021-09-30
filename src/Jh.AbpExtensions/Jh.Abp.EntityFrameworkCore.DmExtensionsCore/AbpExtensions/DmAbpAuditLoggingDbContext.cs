using Jh.Abp.EntityFrameworkCore.DmExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
namespace Jh.Abp.EntityFrameworkCore.DmExtensionsCore.AbpExtensions
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IAuditLoggingDbContext))]
    public class DmAbpAuditLoggingDbContext : AbpAuditLoggingDbContext, IAuditLoggingDbContext
    {
        public DmAbpAuditLoggingDbContext(DbContextOptions<AbpAuditLoggingDbContext> options) : base(options)
        {
        }

        protected override Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
        {
            return CommonFilterExpression.CreateFilterExpression<TEntity>(IsSoftDeleteFilterEnabled, IsMultiTenantFilterEnabled, CurrentTenantId, CombineExpressions);
        }
    }
}
