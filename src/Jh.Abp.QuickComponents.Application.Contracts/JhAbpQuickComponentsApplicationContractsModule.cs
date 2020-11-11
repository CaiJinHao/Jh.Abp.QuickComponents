using Jh.Abp.QuickComponents.AccessToken;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Jh.Abp.QuickComponents
{
    [DependsOn(
       typeof(AbpDddApplicationContractsModule),
       typeof(AbpAuthorizationModule)
       )]
    public class JhAbpQuickComponentsApplicationContractsModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<IdentityClientOptions>(options => {
                options.Authority = configuration["IdentityServer:Clients:WebApi:Authority"];
                options.ClientId = configuration["IdentityServer:Clients:WebApi:ClientId"];
                options.ClientSecret = configuration["IdentityServer:Clients:WebApi:ClientSecret"];
                options.Scope = configuration["IdentityServer:Clients:WebApi:Scope"];
                options.RequireHttps = configuration.GetValue<bool>("IdentityServer:Clients:WebApi:RequireHttps");
            });

            Configure<SwaggerClientOptions>(options => {
                options.UserNameOrEmailAddress = configuration["SwaggerApi:User:UserNameOrEmailAddress"];
                options.Password = configuration["SwaggerApi:User:Password"];
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<JhAbpQuickComponentsApplicationContractsModule>();
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
