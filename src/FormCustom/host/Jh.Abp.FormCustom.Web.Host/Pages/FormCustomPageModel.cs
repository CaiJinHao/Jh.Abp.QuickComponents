using Jh.Abp.FormCustom.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Jh.Abp.FormCustom.Pages
{
    public abstract class FormCustomPageModel : AbpPageModel
    {
        protected FormCustomPageModel()
        {
            LocalizationResourceType = typeof(FormCustomResource);
        }
    }
}