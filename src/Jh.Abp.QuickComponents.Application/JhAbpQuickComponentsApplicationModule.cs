using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Jh.Abp.QuickComponents
{
    [DependsOn(
      typeof(JhAbpQuickComponentsApplicationContractsModule),
      typeof(AbpDddApplicationModule),
      typeof(AbpAutoMapperModule)
      )]
    public class JhAbpQuickComponentsApplicationModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<JhAbpQuickComponentsApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<JhAbpQuickComponentsApplicationModule>(validate: true);
            });
        }
    }
}
