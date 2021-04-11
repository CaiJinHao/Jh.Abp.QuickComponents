using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Jh.Abp.FormCustom
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(FormCustomDomainSharedModule)
    )]
    public class FormCustomDomainModule : AbpModule
    {

    }
}
