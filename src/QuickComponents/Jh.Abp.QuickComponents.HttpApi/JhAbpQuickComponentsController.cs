using Jh.Abp.QuickComponents.Application.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;

namespace Jh.Abp.QuickComponents.HttpApi
{
    [DisableAuditing]
    [ApiController]
    public abstract class JhAbpQuickComponentsController:AbpController
    {
        protected JhAbpQuickComponentsController()
        {
            LocalizationResource = typeof(JhAbpQuickComponentsResource);
        }
    }
}
