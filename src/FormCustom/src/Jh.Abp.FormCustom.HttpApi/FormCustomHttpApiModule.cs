using Localization.Resources.AbpUi;
using Jh.Abp.FormCustom.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Jh.Abp.FormCustom
{
    [DependsOn(
        typeof(FormCustomApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class FormCustomHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(FormCustomHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<FormCustomResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
