using Jh.Abp.MenuManagement.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.Auditing;

namespace Jh.Abp.MenuManagement
{
    public abstract class MenuManagementAppService : ApplicationService
    {
        protected MenuManagementAppService()
        {
            LocalizationResource = typeof(MenuManagementResource);
            ObjectMapperContext = typeof(MenuManagementApplicationModule);
        }
    }
}
