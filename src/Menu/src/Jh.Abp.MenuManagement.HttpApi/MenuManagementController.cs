using Jh.Abp.MenuManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Jh.Abp.MenuManagement
{
    public abstract class MenuManagementController : AbpController
    {
        protected MenuManagementController()
        {
            LocalizationResource = typeof(MenuManagementResource);
        }
    }
}
