using Jh.Abp.MenuManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Jh.Abp.MenuManagement.Permissions
{
    public class MenuManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var MenuManagementGroup = context.AddGroup(MenuManagementPermissions.GroupName, L("Permission: MenuManagement"));

            var MenuAndRoleMapsPermission = MenuManagementGroup.AddPermission(MenuManagementPermissions.MenuAndRoleMaps.Default, L("Permission: MenuAndRoleMapManagement"));
            MenuAndRoleMapsPermission.AddChild(MenuManagementPermissions.MenuAndRoleMaps.Create, L("Permission: Create"));
            MenuAndRoleMapsPermission.AddChild(MenuManagementPermissions.MenuAndRoleMaps.Update, L("Permission: Edit"));
            MenuAndRoleMapsPermission.AddChild(MenuManagementPermissions.MenuAndRoleMaps.Delete, L("Permission: Delete"));
            MenuAndRoleMapsPermission.AddChild(MenuManagementPermissions.MenuAndRoleMaps.ManagePermissions, L("Permission: ManagePermissions"));

            var RolesPermission = MenuManagementGroup.AddPermission(MenuManagementPermissions.Roles.Default, L("Permission: RolesManagement"));
            RolesPermission.AddChild(MenuManagementPermissions.Roles.Create, L("Permission: Create"));
            RolesPermission.AddChild(MenuManagementPermissions.Roles.Update, L("Permission: Edit"));
            RolesPermission.AddChild(MenuManagementPermissions.Roles.Delete, L("Permission: Delete"));
            RolesPermission.AddChild(MenuManagementPermissions.Roles.ManagePermissions, L("Permission: ManagePermissions"));

            var UsersPermission = MenuManagementGroup.AddPermission(MenuManagementPermissions.Users.Default, L("Permission: UsersManagement"));
            UsersPermission.AddChild(MenuManagementPermissions.Users.Create, L("Permission: Create"));
            UsersPermission.AddChild(MenuManagementPermissions.Users.Update, L("Permission: Edit"));
            UsersPermission.AddChild(MenuManagementPermissions.Users.Delete, L("Permission: Delete"));
            UsersPermission.AddChild(MenuManagementPermissions.Users.ChangePassword, L("Permission: ChangePassword"));
            UsersPermission.AddChild(MenuManagementPermissions.Users.LockoutEnabled, L("Permission: LockoutEnabled"));
            UsersPermission.AddChild(MenuManagementPermissions.Users.ManagePermissions, L("Permission: ManagePermissions"));

            var AuditLoggingsPermission = MenuManagementGroup.AddPermission(MenuManagementPermissions.AuditLoggings.Default, L("Permission: AuditLoggingsManagement"));
            AuditLoggingsPermission.AddChild(MenuManagementPermissions.AuditLoggings.Create, L("Permission: Create"));
            AuditLoggingsPermission.AddChild(MenuManagementPermissions.AuditLoggings.Update, L("Permission: Edit"));
            AuditLoggingsPermission.AddChild(MenuManagementPermissions.AuditLoggings.Delete, L("Permission: Delete"));
            AuditLoggingsPermission.AddChild(MenuManagementPermissions.AuditLoggings.ManagePermissions, L("Permission: ManagePermissions"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MenuManagementResource>(name);
        }
    }
}