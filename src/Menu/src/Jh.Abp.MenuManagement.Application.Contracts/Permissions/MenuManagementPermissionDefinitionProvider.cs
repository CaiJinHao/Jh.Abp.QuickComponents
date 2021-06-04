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
            var MenusPermission = MenuManagementGroup.AddPermission(MenuManagementPermissions.Menus.Default, L("Permission: MenuAndRoleMapManagement"));
            MenusPermission.AddChild(MenuManagementPermissions.Menus.Create, L("Permission: Create"));
            MenusPermission.AddChild(MenuManagementPermissions.Menus.Update, L("Permission: Edit"));
            MenusPermission.AddChild(MenuManagementPermissions.Menus.Delete, L("Permission: Delete"));
            MenusPermission.AddChild(MenuManagementPermissions.Menus.ManagePermissions, L("Permission: ManagePermissions"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MenuManagementResource>(name);
        }
    }
}