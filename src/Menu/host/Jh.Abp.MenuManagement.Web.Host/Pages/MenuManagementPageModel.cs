using Jh.Abp.MenuManagement.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Jh.Abp.MenuManagement.Pages
{
    public abstract class MenuManagementPageModel : AbpPageModel
    {
        protected MenuManagementPageModel()
        {
            LocalizationResourceType = typeof(MenuManagementResource);
        }
    }
}