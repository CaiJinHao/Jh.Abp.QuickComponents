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
            Expression<Func<TEntity, bool>> expression = null;

            //WHERE (@__ef_filter__p_0 = CAST(1 AS bit)) OR ([s].[IsDeleted] <> CAST(1 AS bit))
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)) && IsSoftDeleteFilterEnabled)
            {
                expression = e => Convert.ToInt32(EF.Property<bool>(e, "IsDeleted")) == 0;
            }

            if (typeof(IMultiTenant).IsAssignableFrom(typeof(TEntity)) && IsMultiTenantFilterEnabled)
            {
                Expression<Func<TEntity, bool>> multiTenantFilter = e => EF.Property<Guid>(e, "TenantId") == CurrentTenantId;
                expression = expression == null ? multiTenantFilter : CombineExpressions(expression, multiTenantFilter);
            }

            return expression;
        }
    }
}
