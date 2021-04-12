using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Jh.Abp.FormCustom.EntityFrameworkCore;
using Jh.Abp.FormCustom.MultiTenancy;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.Security.Claims;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;
using Jh.Abp.QuickComponents;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Jh.Abp.QuickComponents.JwtAuthentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Authorization.Permissions;
using Jh.Abp.QuickComponents.MiniProfiler;
using Volo.Abp.Auditing;
using Jh.Abp.QuickComponents.Cors;
using Jh.Abp.QuickComponents.Localization;
using Jh.Abp.QuickComponents.Swagger;
using Volo.Abp.Threading;
using Volo.Abp.Data;
using Volo.Abp.Json;

namespace Jh.Abp.FormCustom
{
    [DependsOn(
        typeof(AbpQuickComponentsModule),
        typeof(FormCustomApplicationModule),
        typeof(FormCustomEntityFrameworkCoreModule),
        typeof(FormCustomHttpApiModule),
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
    public class FormCustomHttpApiHostModule : AbpModule
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
                    options.FileSets.ReplaceEmbeddedByPhysical<FormCustomDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Jh.Abp.FormCustom.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<FormCustomDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Jh.Abp.FormCustom.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<FormCustomApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Jh.Abp.FormCustom.Application.Contracts", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<FormCustomApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Jh.Abp.FormCustom.Application", Path.DirectorySeparatorChar)));
                });
            }

            //context.Services.AddAbpSwaggerGenWithOAuth(
            //    configuration["AuthServer:Authority"],
            //    new Dictionary<string, string>
            //    {
            //        {"FormCustom", "FormCustom API"}
            //    },
            //    options =>
            //    {
            //        options.SwaggerDoc("v1", new OpenApiInfo {Title = "FormCustom API", Version = "v1"});
            //        options.DocInclusionPredicate((docName, description) => true);
            //        options.CustomSchemaIds(type => type.FullName);
            //    });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
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
            //        options.Audience = "FormCustom";
            //    });

            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "FormCustom:";
            });

            Configure<AbpJsonOptions>(options =>
            {
                options.DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            });

            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                context.Services
                    .AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "FormCustom-Protection-Keys");
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

            context.Services.Configure<AbpExceptionHandlingOptions>(options =>
            {
                options.SendExceptionsDetailsToClients = configuration.GetValue<bool>("AppSettings:SendExceptionsDetailsToClients");
            });

            context.Services.AddAuthorizeFilter(configuration);
            context.Services.Replace(ServiceDescriptor.Singleton<IPermissionChecker, AlwaysAllowPermissionChecker>());//禁用授权系统
#if DEBUG
            context.Services.AddMiniProfilerComponent();
#endif
            Configure<AbpAuditingOptions>(options =>
            {
                options.ApplicationName = "MunuManagement";
                options.IsEnabledForGetRequests = true;
                options.IsEnabledForAnonymousUsers = false;
                options.AlwaysLogOnException = false;
                //options.EntityHistorySelectors.AddAllEntities();
            });
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
#if DEBUG
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
            //app.UseAbpRequestLocalization();
            app.UseAuthorization();
#if DEBUG
            app.UseJhSwagger(context.GetConfiguration(), this.GetType());
#endif
            //app.UseSwagger();
            //app.UseAbpSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");

            //    var configuration = context.GetConfiguration();
            //    options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            //    options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            //});
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
            //SeedData(context);
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
                    context["RoleId"] = "696C5678-881A-5AA5-4A28-39FB0497B688";//IdentityServerHost创建的角色ID
                    await data.SeedAsync(context);
                }
            });
        }
    }
}
