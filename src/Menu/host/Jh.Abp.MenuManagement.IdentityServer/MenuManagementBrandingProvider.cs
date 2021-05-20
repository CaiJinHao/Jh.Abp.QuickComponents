using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Jh.Abp.MenuManagement
{
    [Dependency(ReplaceServices = true)]
    public class MenuManagementBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "你的项目名称";
    }
}
