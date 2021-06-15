using Volo.Abp.Reflection;

namespace Jh.Abp.MenuManagement.Permissions
{
    public class MenuManagementPermissions
    {
        public const string GroupName = "MenuManagement";

        public static class Menus
        {
            public const string Default = GroupName + ".Menus";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string Recover = Default + ".Recover";
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