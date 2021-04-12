using Jh.Abp.FormCustom.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Jh.Abp.FormCustom
{
    [Route("[controller]")]
    public abstract class FormCustomWebController : AbpController
    {
        public FormCustomWebController()
        {
            LocalizationResource = typeof(FormCustomResource);
        }
    }
}
