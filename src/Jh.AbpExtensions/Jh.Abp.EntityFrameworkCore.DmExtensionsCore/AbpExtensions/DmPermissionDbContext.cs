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
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;

namespace Jh.Abp.EntityFrameworkCore.DmExtensions.AbpExtensions
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IPermissionManagementDbContext))]
    [ConnectionStringName(AbpPermissionManagementDbProperties.ConnectionStringName)]
    public class DmPermissionDbContext : PermissionManagementDbContext, IPermissionManagementDbContext, ITransientDependency
    {
        public DmPermissionDbContext(DbContextOptions<PermissionManagementDbContext> options) : base(options)
        {
        }

        protected override Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
        {
            return CommonFilterExpression.CreateFilterExpression<TEntity>(IsSoftDeleteFilterEnabled, IsMultiTenantFilterEnabled, CurrentTenantId, CombineExpressions);
        }
    }
}
