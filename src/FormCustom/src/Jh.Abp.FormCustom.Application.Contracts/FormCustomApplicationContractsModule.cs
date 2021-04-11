using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Jh.Abp.FormCustom
{
    [DependsOn(
        typeof(FormCustomDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class FormCustomApplicationContractsModule : AbpModule
    {

    }
}
