using Volo.Abp.Modularity;

namespace Jh.Abp.FormCustom
{
    [DependsOn(
        typeof(FormCustomApplicationModule),
        typeof(FormCustomDomainTestModule)
        )]
    public class FormCustomApplicationTestModule : AbpModule
    {

    }
}
