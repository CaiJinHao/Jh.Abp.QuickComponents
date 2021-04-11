using Jh.Abp.FormCustom.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Jh.Abp.FormCustom
{
    public abstract class FormCustomController : AbpController
    {
        protected FormCustomController()
        {
            LocalizationResource = typeof(FormCustomResource);
        }
    }
}
