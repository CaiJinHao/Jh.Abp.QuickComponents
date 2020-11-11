using Volo.Abp.AspNetCore.Mvc;

namespace Jh.Abp.QuickComponents.HttpApi
{
    public abstract class JhAbpQuickComponentsController:AbpController
    {
        protected JhAbpQuickComponentsController()
        {
            LocalizationResource = typeof(JhAbpQuickComponentsResource);
        }
    }
}
