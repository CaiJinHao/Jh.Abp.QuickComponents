using Volo.Abp.Reflection;

namespace Jh.Abp.MenuManagement.Permissions
{
    public class MenuManagementPermissions
    {
        public const string GroupName = "MenuManagement";

        public static class Menus
        {
            public const string Default = GroupName + ".Menus";//分页
            public const string Export = Default + ".Export";//导出
            public const string Detail = Default + ".Detail";//详细
            public const string Create = Default + ".Create";
            public const string BatchCreate = Default + ".BatchCreate";
            public const string Update = Default + ".Update";
            public const string PortionUpdate = Default + ".PortionUpdate";//局部更新
            public const string Delete = Default + ".Delete";
            public const string BatchDelete = Default + ".BatchDelete";
            public const string Recover = Default + ".Recover";//恢复删除
            public const string ManagePermissions = Default + ".ManagePermissions";
        }

        public static class MenuAndRoleMaps
        {
            public const string Default = GroupName + ".MenuAndRoleMaps";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManagePermissions = Default + ".ManagePermissions";
        }

        public static class AuditLoggings
        {
            public const string Default = GroupName + ".AuditLoggings";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManagePermissions = Default + ".ManagePermissions";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(MenuManagementPermissions));
        }
    }
}