using Jh.Abp.MenuManagement.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;

namespace Jh.Abp.MenuManagement
{
    [ApiController]
    public abstract class MenuManagementController : AbpController
    {
        protected MenuManagementController()
        {
            LocalizationResource = typeof(MenuManagementResource);
        }
    }
}
