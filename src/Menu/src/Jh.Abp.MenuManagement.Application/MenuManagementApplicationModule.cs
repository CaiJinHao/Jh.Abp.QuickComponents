using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Dapper;
using Jh.Abp.MenuManagement.EntityFrameworkCore;

namespace Jh.Abp.MenuManagement
{
    [DependsOn(
        typeof(MenuManagementDomainModule),
        typeof(MenuManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpDapperModule),
        typeof(MenuManagementEntityFrameworkCoreModule)
        )]
    public class MenuManagementApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<MenuManagementApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<MenuManagementApplicationModule>(validate: true);
            });
        }
    }
}
