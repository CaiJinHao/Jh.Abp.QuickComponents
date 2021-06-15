using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Jh.Abp.Domain.Localization;

namespace Jh.Abp.Domain.Shared
{
    [DependsOn(
       typeof(AbpValidationModule)
   )]
    public class JhAbpExtensionsDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<JhAbpExtensionsDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<JhAbpExtensionsResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))//继承资源
                    .AddVirtualJson("/Localization/JhAbpExtensions");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("JhAbpExtensions", typeof(JhAbpExtensionsResource));
            });
        }
    }
}
