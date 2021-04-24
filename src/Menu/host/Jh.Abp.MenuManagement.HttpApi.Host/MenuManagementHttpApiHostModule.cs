using Jh.Abp.MenuManagement.MultiTenancy;
using Jh.Abp.QuickComponents;
using Jh.Abp.QuickComponents.Cors;
using Jh.Abp.QuickComponents.JwtAuthentication;
using Jh.Abp.QuickComponents.Localization;
using Jh.Abp.QuickComponents.MiniProfiler;
using Jh.Abp.QuickComponents.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System.IO;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.Threading;
using Volo.Abp.VirtualFileSystem;

namespace Jh.Abp.MenuManagement
{
    [DependsOn(
        typeof(AbpQuickComponentsModule),
        typeof(MenuManagementHttpApiModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule)
        )]
    public class MenuManagementHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<MenuManagementDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Jh.Abp.MenuManagement.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<MenuManagementDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Jh.Abp.MenuManagement.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<MenuManagementApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Jh.Abp.MenuManagement.Application.Contracts", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<MenuManagementApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Jh.Abp.MenuManagement.Application", Path.DirectorySeparatorChar)));
                });
            }

            //context.Services.AddAbpSwaggerGenWithOAuth(
            //    configuration["AuthServer:Authority"],
            //    new Dictionary<string, string>
            //    {
            //        {"MenuManagement", "MenuManagement API"}
            //    },
            //    options =>
            //    {
            //        options.SwaggerDoc("v1", new OpenApiInfo {Title = "MenuManagement API", Version = "v1"});
            //        options.DocInclusionPredicate((docName, description) => true);
            //        options.CustomSchemaIds(type => type.FullName);
            //    });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
                options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });

            //context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.Authority = configuration["AuthServer:Authority"];
            //        options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
            //        options.Audience = "MenuManagement";
            //    });

            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "MenuManagement:";
            });

            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                context.Services
                    .AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "MenuManagement-Protection-Keys");
            }

            //context.Services.AddCors(options =>
            //{
            //    options.AddPolicy(DefaultCorsPolicyName, builder =>
            //    {
            //        builder
            //            .WithOrigins(
            //                configuration["App:CorsOrigins"]
            //                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
            //                    .Select(o => o.RemovePostFix("/"))
            //                    .ToArray()
            //            )
            //            .WithAbpExposedHeaders()
            //            .SetIsOriginAllowedToAllowWildcardSubdomains()
            //            .AllowAnyHeader()
            //            .AllowAnyMethod()
            //            .AllowCredentials();
            //    });
            //});

            Configure<AbpAuditingOptions>(options =>
            {
                options.ApplicationName = "MunuManagement";
                options.IsEnabledForGetRequests = true;
                options.IsEnabledForAnonymousUsers = false;
                options.AlwaysLogOnException = false;
                //options.EntityHistorySelectors.AddAllEntities();
            });

            context.Services.Configure<AbpExceptionHandlingOptions>(options =>
            {
                options.SendExceptionsDetailsToClients = configuration.GetValue<bool>("AppSettings:SendExceptionsDetailsToClients");
            });
            
            context.Services.AddApiVersion();
            context.Services.AddSwaggerComponent(configuration);
            context.Services.AddCorsPolicy(configuration);
            context.Services.AddLocalizationComponent();
            context.Services.AddJwtAuthenticationComponent(configuration);
            context.Services.AddAuthorizeFilter(configuration);
            context.Services.Replace(ServiceDescriptor.Singleton<IPermissionChecker, AlwaysAllowPermissionChecker>());//禁用授权系统
#if !DEBUG
            context.Services.AddMiniProfilerComponent();
#endif
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorPage();
                app.UseHsts();
            }

#if !DEBUG
            app.UseMiniProfiler();
#endif
            app.UseHttpsRedirection();
            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseCors(CorsExtensions.DefaultCorsPolicyName);
            //app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();
            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }
            app.UseJhRequestLocalization();
            app.UseAuthorization();
            //app.UseSwagger();
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");

            //    var configuration = context.GetConfiguration();
            //    options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            //    options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            //});
#if !DEBUG
            app.UseJhSwagger(context.GetConfiguration(),this.GetType());
#endif
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
            SeedData(context);
        }

        private void SeedData(ApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(async () =>
            {
                using (var scope = context.ServiceProvider.CreateScope())
                {
                    var data = scope.ServiceProvider
                        .GetRequiredService<IDataSeeder>();
                    var context = new DataSeedContext();
                    context["RoleId"] = "6ABCE55B-7504-3AC3-1E69-39FC0B2D6D7E";//IdentityServerHost创建的角色ID
                    await data.SeedAsync(context);
                }
            });
        }
    }
}
