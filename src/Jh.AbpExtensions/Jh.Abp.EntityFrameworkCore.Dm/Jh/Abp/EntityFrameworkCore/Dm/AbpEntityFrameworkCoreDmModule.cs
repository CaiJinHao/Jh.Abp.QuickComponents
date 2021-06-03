using Jh.Abp.EntityFrameworkCore.DmExtensions;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Guids;
using Volo.Abp.Modularity;

namespace Jh.Abp.EntityFrameworkCore.Dm
{
    [DependsOn(
        typeof(JhEntityFrameworkCoreDmExtensionsModule),
        typeof(AbpEntityFrameworkCoreModule)
        )]
    public class AbpEntityFrameworkCoreDmModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpSequentialGuidGeneratorOptions>(options =>
            {
                if (options.DefaultSequentialGuidType == null)
                {
                    options.DefaultSequentialGuidType = SequentialGuidType.SequentialAsString;
                }
            });
        }
    }
}
