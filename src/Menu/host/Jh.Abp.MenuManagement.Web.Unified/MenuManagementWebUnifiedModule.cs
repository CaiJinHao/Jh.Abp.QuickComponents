using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Jh.Abp.MenuManagement.EntityFrameworkCore;
using Jh.Abp.MenuManagement.MultiTenancy;
using Jh.Abp.MenuManagement.Web;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.Threading;
using Volo.Abp.VirtualFileSystem;
using System;
using Volo.Abp.Json;
using Volo.Abp.Authorization.Permissions;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Auditing;
using Jh.Abp.QuickComponents;
using Jh.Abp.QuickComponents.Swagger;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Jh.Abp.QuickComponents.Cors;
using Jh.Abp.QuickComponents.Localization;
using Jh.Abp.QuickComponents.JwtAuthentication;
using Microsoft.Extensions.Configuration;

namespace Jh.Abp.MenuManagement
{
    [DependsOn(
        typeof(AbpQuickComponentsModule),
        typeof(MenuManagementWebModule),
        typeof(MenuManagementApplicationModule),
        typeof(MenuManagementEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpAutofacModule),
        typeof(AbpAccountWebModule),
        typeof(AbpAccountApplicationModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpIdentityWebModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpIdentityEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementDomainIdentityModule),
        typeof(AbpFeatureManagementWebModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule),
        typeof(AbpTenantManagementWebModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule)
        )]
    public class MenuManagementWebUnifiedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<MenuManagementDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Jh.Abp.MenuManagement.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<MenuManagementDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Jh.Abp.MenuManagement.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<MenuManagementApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Jh.Abp.MenuManagement.Application.Contracts", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<MenuManagementApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Jh.Abp.MenuManagement.Application", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<MenuManagementWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Jh.Abp.MenuManagement.Web", Path.DirectorySeparatorChar)));
                });
            }

            context.Services.AddApiVersion();
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "MenuManagement API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                });


            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
                options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
                options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português (Brasil)"));
                options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });

            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });

            Configure<AbpJsonOptions>(options =>
            {
                options.DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            });

            context.Services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromSeconds(60 * 60);
            });

            /*Configure<AuthorizationOptions>(options =>
            {
                options.AddPolicy(RoleConsts.OpenAccountPolicy, policy => policy.RequireAssertion(context => {
                    var authorizationResult = context.User.Claims.Any(c =>
                        c.Type == JhJwtClaimTypes.RoleId && RoleConsts.OpenAccountPolicyRoles.Contains(c.Value)
                    );
                    if (!authorizationResult)
                    {
                        if (context.Resource is DefaultHttpContext httpContext && httpContext.Session != null)
                        {
                            httpContext.Session.SetString("PolicyName", RoleConsts.OpenAccountPolicy);
                        }
                    }
                    return authorizationResult;
                }));

                options.AddPolicy(RoleConsts.AuthenticationPolicy, policy => policy.RequireAssertion(context => {
                    var authorizationResult = context.User.Claims.Any(c =>
                        c.Type == JhJwtClaimTypes.RoleId && RoleConsts.AuthenticationPolicyRoles.Contains(c.Value)
                    );
                    if (!authorizationResult)
                    {
                        if (context.Resource is DefaultHttpContext httpContext && httpContext.Session != null)
                        {
                            httpContext.Session.SetString("PolicyName", RoleConsts.AuthenticationPolicy);
                        }
                    }
                    return authorizationResult;
                }));
            });*/

            //禁用http验证cookies xsf
            /*  Configure<AbpAntiForgeryOptions>(options => {
                  options.AutoValidate = false;
              });*/

            /* context.Services.ConfigureApplicationCookie(options =>
             {
                 options.LoginPath = "/Login";
                 options.AccessDeniedPath = "/AccessDenied";
             });*/

            //禁用审计日志
            Configure<AbpAuditingOptions>(options =>
            {
                options.IsEnabled = false;
                options.ApplicationName = "MenuManagementApplication";
                options.IsEnabledForAnonymousUsers = false;
                options.IsEnabledForGetRequests = true;
                options.AlwaysLogOnException = false;
                //options.EntityHistorySelectors.AddAllEntities();
            });

            context.Services.Replace(ServiceDescriptor.Singleton<IPermissionChecker, AlwaysAllowPermissionChecker>());//禁用授权系统
            context.Services.AddAbpIdentity().AddClaimsPrincipalFactory<JhUserClaimsPrincipalFactory>();
            context.Services.AddLocalizationComponent();
            context.Services.AddAuthorizeFilter(configuration);
            //是否将错误发送到客户端
#if DEBUG
            context.Services.Configure<AbpExceptionHandlingOptions>(options =>
            {
                options.SendExceptionsDetailsToClients = configuration.GetValue<bool>("AppSettings:SendExceptionsDetailsToClients");
            });
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
                //app.UseExceptionHandler("/Error");
                app.UseErrorPage();
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseAuthentication();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseJhRequestLocalization();
            app.UseAuthorization();

#if DEBUG
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");
            });
#endif

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints().UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //using (var scope = context.ServiceProvider.CreateScope())
            //{
            //    AsyncHelper.RunSync(async () =>
            //    {
            //        await scope.ServiceProvider
            //            .GetRequiredService<IDataSeeder>()
            //            .SeedAsync();
            //    });
            //}
        }
    }
}
