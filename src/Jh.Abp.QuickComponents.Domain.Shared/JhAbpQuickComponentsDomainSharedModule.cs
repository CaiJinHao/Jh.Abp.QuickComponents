using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Jh.Abp.QuickComponents.Domain.Shared
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class JhAbpQuickComponentsDomainSharedModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<JhAbpQuickComponentsDomainSharedModule>();
            });

            //添加了一个新的本地化资源, 使用"en"（英语）作为默认的本地化.
            //用JSON文件存储本地化字符串.
            //使用虚拟文件系统 将JSON文件嵌入到程序集中.必须是嵌入的资源
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<JhAbpQuickComponentsResource>("zh-Hans")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/JhAbpQuickComponents");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("JhAbpQuickComponents", typeof(JhAbpQuickComponentsResource));
            });
        }
    }
}
