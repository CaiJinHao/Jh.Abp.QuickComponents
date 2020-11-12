using Jh.Abp.QuickComponents.Cors;
using Jh.Abp.QuickComponents.JwtAuthentication;
using Jh.Abp.QuickComponents.Localization;
using Jh.Abp.QuickComponents.MiniProfiler;
using Jh.Abp.QuickComponents.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Jh.Abp.QuickComponents
{
    public class AbpQuickComponentsModule: AbpModule
    {
        private IConfiguration configuration { get; set; }
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            configuration = context.Services.GetConfiguration();

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AbpQuickComponentsModule>();
            });

            context.Services.AddMiniProfilerComponent();
            context.Services.AddSwaggerComponent(configuration);
            context.Services.AddLocalizationComponent();
            context.Services.AddJwtAuthenticationComponent(configuration);

#if DEBUG
            Configure<AbpExceptionHandlingOptions>(options =>
            {
                //是否发送异常详情到客户端
                options.SendExceptionsDetailsToClients = false;
            });
#endif
        }

        public override void OnApplicationInitialization(Volo.Abp.ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseMiniProfiler();
            app.UseCors(CorsExtensions.DefaultCorsPolicyName);
        }
    }
}
