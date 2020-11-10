using Jh.Abp.QuickComponents.AccessToken;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

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
        }
    }
}
