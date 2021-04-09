using Jh.Abp.MenuManagement.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;

namespace Jh.Abp.MenuManagement
{
    [DisableAuditing]
    [ApiController]
    public abstract class MenuManagementController : AbpController
    {
        protected MenuManagementController()
        {
            LocalizationResource = typeof(MenuManagementResource);
        }
    }
}
