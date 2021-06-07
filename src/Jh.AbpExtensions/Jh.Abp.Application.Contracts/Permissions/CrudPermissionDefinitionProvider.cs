using Jh.Abp.Domain.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Jh.Abp.Application.Contracts.Permissions
{
    public class CrudPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var crudManagementGroup = context.AddGroup(CrudPermissions.GroupName, L("Permission: CrudManagement"));
            var crudPermission = crudManagementGroup.AddPermission(CrudPermissions.Cruds.Default, L("Permission: CrudManagement"));
            crudPermission.AddChild(CrudPermissions.Cruds.Create, L("Permission: Create"));
            crudPermission.AddChild(CrudPermissions.Cruds.Update, L("Permission: Edit"));
            crudPermission.AddChild(CrudPermissions.Cruds.Delete, L("Permission: Delete"));
            crudPermission.AddChild(CrudPermissions.Cruds.ManagePermissions, L("Permission: ManagePermissions"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<JhAbpExtensionsResource>(name);
        }
    }
}
