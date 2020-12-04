namespace Jh.Abp.MenuManagement
{
    public static class MenuManagementDbProperties
    {
        public const string BaseDbTablePrefix = "Sys";
        public static string DbTablePrefix { get; set; } = "MenuManagement";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "MenuManagement";
    }
}
