using Jh.Abp.FormCustom.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Jh.Abp.FormCustom.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class FormCustomPageModel : AbpPageModel
    {
        protected FormCustomPageModel()
        {
            LocalizationResourceType = typeof(FormCustomResource);
            ObjectMapperContext = typeof(FormCustomWebModule);
        }
    }
}