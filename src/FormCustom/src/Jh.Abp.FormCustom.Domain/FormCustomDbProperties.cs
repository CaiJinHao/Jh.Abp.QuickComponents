namespace Jh.Abp.FormCustom
{
    public static class FormCustomDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Fc";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "FormCustom";
    }
}
