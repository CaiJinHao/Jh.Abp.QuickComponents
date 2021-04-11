using Volo.Abp.Reflection;

namespace Jh.Abp.FormCustom.Permissions
{
    public class FormCustomPermissions
    {
        public const string GroupName = "FormCustom";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(FormCustomPermissions));
        }
    }
}