using Jh.Abp.QuickComponents.AccessToken;
using Jh.Abp.QuickComponents.Application.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Jh.Abp.QuickComponents
{
    [DependsOn(
        typeof(AbpValidationModule),
       typeof(AbpDddApplicationContractsModule),
       typeof(AbpAuthorizationModule)
       )]
    public class JhAbpQuickComponentsApplicationContractsModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<IdentityClientOptions>(options => {
                options.Authority = configuration["AuthServer:Authority"];
                options.ClientId = configuration["AuthServer:AppClientId"];
                options.ClientSecret = configuration["AuthServer:AppClientSecret"];
                options.Scope = configuration["AuthServer:Scope"];
                options.RequireHttps = configuration.GetValue<bool>("AuthServer:RequireHttpsMetadata");
            });

            Configure<SwaggerClientOptions>(options => {
                options.UserNameOrEmailAddress = configuration["SwaggerApi:User:UserNameOrEmailAddress"];
                options.Password = configuration["SwaggerApi:User:Password"];
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                //添加指定类所在程序集的所有 嵌入式资源
                options.FileSets.AddEmbedded<JhAbpQuickComponentsApplicationContractsModule>();
            });

            //添加了一个新的本地化资源, 使用"en"（英语）作为默认的本地化.
            //用JSON文件存储本地化字符串.
            //使用虚拟文件系统 将JSON文件嵌入到程序集中.必须是嵌入的资源
            //如果不知道嵌入式资源的虚拟路径可以注入IVirtualFileProvider _virtualFileProvider查看。一般是命名空间
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<JhAbpQuickComponentsResource>("zh-Hans")
                    .AddBaseTypes(typeof(AbpValidationResource))
                     //模块资源按照项目名称+文件夹路径写
                     .AddVirtualJson("/Jh/Abp/QuickComponents/Application/Contracts/Localization/JhAbpQuickComponents");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("JhAbpQuickComponentsApplication", typeof(JhAbpQuickComponentsResource));
            });
        }
    }
}
