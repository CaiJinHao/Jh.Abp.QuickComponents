using Jh.Abp.QuickComponents.Cors;
using Jh.Abp.QuickComponents.HttpApi;
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
    [DependsOn(typeof(JhAbpQuickComponentsHttpApiModule))]
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
            context.Services.AddCorsPolicy(configuration);
        }

        public override void OnApplicationInitialization(Volo.Abp.ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseMiniProfiler();
            app.UseCors(CorsExtensions.DefaultCorsPolicyName);
        }
    }
}
