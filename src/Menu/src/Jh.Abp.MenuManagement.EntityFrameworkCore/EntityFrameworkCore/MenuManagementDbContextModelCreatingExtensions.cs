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

                b.Property(p => p.Code).HasComment("菜单编号");
                b.Property(p => p.Name).HasComment("菜单名称");
                b.Property(p => p.Icon).HasComment("菜单图标");
                b.Property(p => p.Sort).HasComment("同一级别内排序");
                b.Property(p => p.ParentCode).HasComment("上级菜单编号");
                b.Property(p => p.Url).HasComment("导航路径");
                b.Property(p => p.Description).HasComment("菜单描述");
            });

            builder.Entity<MenuAndRoleMap>(b => {
                b.ConfigureByConvention();
                b.Property(p => p.Id).ValueGeneratedOnAdd();

                b.HasOne(mrm => mrm.Menu).WithMany(menu => menu.MenuRoleMaps).HasForeignKey(menu => menu.MenuId);
                b.HasIndex(c => c.RoleId).IncludeProperties(p => p.MenuId);//mysql不能使用包含列
            });
        }
    }
}