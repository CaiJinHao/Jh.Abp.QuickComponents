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
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
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
using Jh.Abp.Extensions;
using Jh.Abp.EntityFrameworkCore.DmExtensions;
using Jh.Abp.EntityFrameworkCore.Dm;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement;
using Jh.Abp.QuickComponents.HttpApi;

namespace Jh.Abp.MenuManagement
{
    [DependsOn(
        typeof(AbpQuickComponentsModule),
        typeof(JhAbpQuickComponentsHttpApiModule),
        typeof(MenuManagementHttpApiModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        //typeof(AbpEntityFrameworkCoreDmModule),
        //typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpPermissionManagementDomainIdentityModule),//add
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreSerilogModule),
        //typeof(JhEntityFrameworkCoreDmExtensionsModule),
        typeof(AbpSwashbuckleModule)
        )]
    public class MenuManagementHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";
        private IConfiguration configuration;
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            configuration = context.Services.GetConfiguration();

            Configure<AbpDbContextOptions>(options =>
            {
                //options.UseSqlServer();
                //options.UseDm();
                options.UseMySQL();
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

            context.Services.AddJhAbpSwagger(configuration,
                new Dictionary<string, string>
                {
                    {"MenuManagement", "MenuManagement API"}
                });
            /*context.Services.AddAbpSwaggerGenWithOAuth(
                configuration["AuthServer:Authority"],
                new Dictionary<string, string>
                {
                    {"MenuManagement", "MenuManagement API"}
                },
                options =>
                {
                    options.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Title = configuration["SwaggerApi:OpenApiInfo:Title"],
                            Version = configuration["SwaggerApi:OpenApiInfo:Version"],
                            Description = configuration["SwaggerApi:OpenApiInfo:Description"],
                        });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                });*/

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

            context.Services.AddJwtAuthentication(configuration);
            //context.Services.AddOidcAuthentication(configuration);
            /*context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.Audience = "MenuManagement";
                });*/

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

            context.Services.AddCorsPolicy(configuration);
            /*context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });*/

            //审计日志配置
            Configure<AbpAuditingOptions>(options =>
            {
                options.ApplicationName = "MunuManagement";//如果有多个应用程序保存审计日志到单一的数据库,使用此属性设置为你的应用程序名称区分不同的应用程序日志.
                options.IsEnabled = true;// (默认值: true): 启用或禁用审计系统的总开关. 如果值为 false,则不使用其他选项
                options.HideErrors = true;// (默认值: true): 在保存审计日志对象时如果发生任何错误,审计日志系统会将错误隐藏并写入常规日志. 如果保存审计日志对系统非常重要那么将其设置为 false 以便在隐藏错误时抛出异常.
                options.IsEnabledForAnonymousUsers = true;//(默认值: true): 如果只想为经过身份验证的用户记录审计日志,请设置为 false.如果为匿名用户保存审计日志,你将看到这些用户的 UserId 值为 null.
                options.AlwaysLogOnException = false;//(默认值: true): 如果设置为 true,将始终在异常/错误情况下保存审计日志,不检查其他选项(IsEnabled 除外,它完全禁用了审计日志).
                options.IsEnabledForGetRequests = false;// (默认值: false): HTTP GET请求通常不应该在数据库进行任何更改,审计日志系统不会为GET请求保存审计日志对象. 将此值设置为 true 可为GET请求启用审计日志系统.
                options.EntityHistorySelectors.AddAllEntities();    //选择器列表,用于确定是否选择了用于保存实体更改的实体类型
                //options.IgnoredTypes = []; //审计日志系统忽略的 Type 列表. 如果它是实体类型,则不会保存此类型实体的更改. 在序列化操作参数时也使用此列表.
            });

            Configure<AbpAntiForgeryOptions>(options =>
            {
                //禁用csrf-xsrf（跨站请求伪造保护）
                options.AutoValidate = false;
            });

            context.Services.Configure<AbpExceptionHandlingOptions>(options =>
            {
                options.SendExceptionsDetailsToClients = configuration.GetValue<bool>("AppSettings:SendExceptionsDetailsToClients");
            });

            context.Services.AddApiVersion();
            context.Services.AddLocalizationComponent();
            context.Services.AddAuthorizeFilter(configuration);
            //context.Services.Replace(ServiceDescriptor.Singleton<IPermissionChecker, AlwaysAllowPermissionChecker>());//禁用授权系统
#if DEBUG
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
                //app.UseHsts();
            }

#if DEBUG
            app.UseMiniProfiler();
#endif
            //app.UseHttpsRedirection();
            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(CorsExtensions.DefaultCorsPolicyName);
            //app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();
            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }
            app.UseJhRequestLocalization();
            //app.UseAbpRequestLocalization();
            app.UseAuthorization();
#if DEBUG
            app.UseJhAbpSwagger(configuration);
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
                    context["RoleId"] = "75a8f151-d69a-c5ba-05cd-39fd0c6ac115";//IdentityServerHost创建的角色ID
                    await data.SeedAsync(context);
                }
            });
        }
    }
}
