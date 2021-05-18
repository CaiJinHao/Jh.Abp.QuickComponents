namespace Jh.Abp.MenuManagement
{
    public static class MenuManagementDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Sys";

        public static string DbSchema { get; set; } = null;
        /// <summary>
        /// 系统管理模块
        /// </summary>
        public const string ConnectionStringName = "MenuManagement";
    }
}
