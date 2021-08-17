using Jh.Abp.QuickComponents.Application.Localization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Jh.Abp.QuickComponents.HttpApi
{
    [DependsOn(
        typeof(JhAbpQuickComponentsApplicationContractsModule),
        typeof(JhAbpQuickComponentsApplicationModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class JhAbpQuickComponentsHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(JhAbpQuickComponentsHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<JhAbpQuickComponentsResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
