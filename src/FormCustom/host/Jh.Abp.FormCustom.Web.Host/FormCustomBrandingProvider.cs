using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Jh.Abp.FormCustom
{
    [Dependency(ReplaceServices = true)]
    public class FormCustomBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "FormCustom";
    }
}
