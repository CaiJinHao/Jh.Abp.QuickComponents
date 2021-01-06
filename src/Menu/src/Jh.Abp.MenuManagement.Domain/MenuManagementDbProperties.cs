namespace Jh.Abp.MenuManagement
{
    public static class MenuManagementDbProperties
    {
        /// <summary>
        /// 隶属于系统管理模块
        /// </summary>
        public const string BaseDbTablePrefix = "Sys";
        public static string DbTablePrefix { get; set; } = BaseDbTablePrefix;

        public static string DbSchema { get; set; } = null;
        /// <summary>
        /// 系统管理模块
        /// </summary>
        public const string ConnectionStringName = "MenuManagement";
    }
}
