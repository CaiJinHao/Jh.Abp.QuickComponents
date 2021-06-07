using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Reflection;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;

namespace Jh.Abp.EntityFrameworkCore.DmExtensions.AbpExtensions
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityServerDbContext))]
    [ConnectionStringName(AbpIdentityServerDbProperties.ConnectionStringName)]
    public class DmIdentityServerDbContext : IdentityServerDbContext, IIdentityServerDbContext, ITransientDependency
    {
        public DmIdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : base(options)
        {
        }

        protected override Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
        {
            return CommonFilterExpression.CreateFilterExpression<TEntity>(IsSoftDeleteFilterEnabled, IsMultiTenantFilterEnabled, CurrentTenantId, CombineExpressions);
        }
    }
}
