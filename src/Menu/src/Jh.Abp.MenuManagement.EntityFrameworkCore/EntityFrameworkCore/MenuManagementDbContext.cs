﻿using Jh.Abp.MenuManagement.Menus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;

namespace Jh.Abp.MenuManagement.EntityFrameworkCore
{
    [ConnectionStringName(MenuManagementDbProperties.ConnectionStringName)]
    public class MenuManagementDbContext : AbpDbContext<MenuManagementDbContext>, IMenuManagementDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Menu> Menus { get; set; }

        public DbSet<MenuAndRoleMap> MenuAndRoleMaps { get; set; }
        public MenuManagementDbContext(DbContextOptions<MenuManagementDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureMenuManagement();
        }

        //达梦兼容
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