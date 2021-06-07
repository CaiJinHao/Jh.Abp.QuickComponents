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
using Volo.Abp.MultiTenancy;

namespace Jh.Abp.EntityFrameworkCore.DmExtensions
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityDbContext))]
    [ConnectionStringName(AbpIdentityDbProperties.ConnectionStringName)]
    public class DmIdentityDbContext : IdentityDbContext, IIdentityDbContext, ITransientDependency
    {
        public DmIdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
        {
            return CommonFilterExpression.CreateFilterExpression<TEntity>(IsSoftDeleteFilterEnabled,IsMultiTenantFilterEnabled,CurrentTenantId,CombineExpressions);
        }
    }
}
