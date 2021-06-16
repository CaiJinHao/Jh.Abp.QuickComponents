using Jh.Abp.MenuManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Jh.Abp.MenuManagement.Permissions
{
    public class MenuManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var MenuManagementGroup = context.AddGroup(MenuManagementPermissions.GroupName, L("Permission:MenuManagement"));
            var MenuAndRoleMapsPermission = MenuManagementGroup.AddPermission(MenuManagementPermissions.MenuAndRoleMaps.Default, L("Permission:MenuAndRoleMapManagement"));
            MenuAndRoleMapsPermission.AddChild(MenuManagementPermissions.MenuAndRoleMaps.Create, L("Permission:Create"));
            MenuAndRoleMapsPermission.AddChild(MenuManagementPermissions.MenuAndRoleMaps.Update, L("Permission:Edit"));
            MenuAndRoleMapsPermission.AddChild(MenuManagementPermissions.MenuAndRoleMaps.Delete, L("Permission:Delete"));
            MenuAndRoleMapsPermission.AddChild(MenuManagementPermissions.MenuAndRoleMaps.ManagePermissions, L("Permission:ManagePermissions"));

            var MenusPermission = MenuManagementGroup.AddPermission(MenuManagementPermissions.Menus.Default, L("Permission:MenusManagement"));
            MenusPermission.AddChild(MenuManagementPermissions.Menus.Export, L("Permission:Export"));
            MenusPermission.AddChild(MenuManagementPermissions.Menus.Detail, L("Permission:Detail"));
            MenusPermission.AddChild(MenuManagementPermissions.Menus.Create, L("Permission:Create"));
            MenusPermission.AddChild(MenuManagementPermissions.Menus.BatchCreate, L("Permission:BatchCreate"));
            MenusPermission.AddChild(MenuManagementPermissions.Menus.Update, L("Permission:Edit"));
            MenusPermission.AddChild(MenuManagementPermissions.Menus.PortionUpdate, L("Permission:PortionEdit"));
            MenusPermission.AddChild(MenuManagementPermissions.Menus.Delete, L("Permission:Delete"));
            MenusPermission.AddChild(MenuManagementPermissions.Menus.BatchDelete, L("Permission:BatchDelete"));
            MenusPermission.AddChild(MenuManagementPermissions.Menus.Recover, L("Permission:Recover"));
            MenusPermission.AddChild(MenuManagementPermissions.Menus.ManagePermissions, L("Permission:ManagePermissions"));

            var AuditLoggingsPermission = MenuManagementGroup.AddPermission(MenuManagementPermissions.AuditLoggings.Default, L("Permission:AuditLoggingsManagement"));
            AuditLoggingsPermission.AddChild(MenuManagementPermissions.AuditLoggings.Create, L("Permission:Create"));
            AuditLoggingsPermission.AddChild(MenuManagementPermissions.AuditLoggings.Update, L("Permission:Edit"));
            AuditLoggingsPermission.AddChild(MenuManagementPermissions.AuditLoggings.Delete, L("Permission:Delete"));
            AuditLoggingsPermission.AddChild(MenuManagementPermissions.AuditLoggings.ManagePermissions, L("Permission:ManagePermissions"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MenuManagementResource>(name);
        }
    }
}