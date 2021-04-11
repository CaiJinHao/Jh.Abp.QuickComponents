using Jh.Abp.FormCustom.Localization;
using Volo.Abp.Application.Services;

namespace Jh.Abp.FormCustom
{
    public abstract class FormCustomAppService : ApplicationService
    {
        protected FormCustomAppService()
        {
            LocalizationResource = typeof(FormCustomResource);
            ObjectMapperContext = typeof(FormCustomApplicationModule);
        }
    }
}
