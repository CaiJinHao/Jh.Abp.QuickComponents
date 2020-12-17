using Localization.Resources.AbpUi;
using Jh.Abp.MenuManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Jh.Abp.MenuManagement.EntityFrameworkCore;

namespace Jh.Abp.MenuManagement
{
    [DependsOn(
        typeof(MenuManagementApplicationModule),
        typeof(MenuManagementEntityFrameworkCoreModule),
        typeof(MenuManagementApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class MenuManagementHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(MenuManagementHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<MenuManagementResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
