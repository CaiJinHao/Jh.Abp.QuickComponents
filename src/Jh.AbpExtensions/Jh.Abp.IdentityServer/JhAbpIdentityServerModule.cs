using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Jh.Abp.IdentityServer
{
    public class JhAbpIdentityServerModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<JhAbpIdentityServerResource>("zh-Hans")
                    .AddBaseTypes(typeof(AbpIdentityServerResource))
                    //指定嵌入式资源的虚拟路径
                    .AddVirtualJson("/Localization/JhAbpIdentityServer");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("JhAbpIdentityServer", typeof(JhAbpIdentityServerResource));
            });
        }
    }
}
