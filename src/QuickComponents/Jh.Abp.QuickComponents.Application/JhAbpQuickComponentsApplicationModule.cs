using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.IdentityModel;

namespace Jh.Abp.QuickComponents
{
    [DependsOn(
      typeof(JhAbpQuickComponentsApplicationContractsModule),
      typeof(AbpDddApplicationModule),
      typeof(AbpAutoMapperModule),
        typeof(AbpIdentityModelModule)
      )]
    public class JhAbpQuickComponentsApplicationModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<JhAbpQuickComponentsApplicationModule>();
            });

            context.Services.AddAutoMapperObjectMapper<JhAbpQuickComponentsApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<JhAbpQuickComponentsApplicationModule>(validate: true);
            });
        }
    }
}
