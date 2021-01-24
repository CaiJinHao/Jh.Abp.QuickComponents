using System;
using Jh.Abp.MenuManagement.Menus;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;


namespace Jh.Abp.MenuManagement.EntityFrameworkCore
{
    public static class MenuManagementDbContextModelCreatingExtensions
    {
        public static void ConfigureMenuManagement(
            this ModelBuilder builder,
            Action<MenuManagementModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new MenuManagementModelBuilderConfigurationOptions(
                MenuManagementDbProperties.DbTablePrefix,
                MenuManagementDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<Menu>(b => {
                b.ConfigureByConvention();
            });

            builder.Entity<MenuAndRoleMap>(b => {
                b.ConfigureByConvention();

                b.HasIndex(c => c.RoleId).IncludeProperties(p => p.MenuId);//mysql不能使用包含列
            });
        }
    }
}