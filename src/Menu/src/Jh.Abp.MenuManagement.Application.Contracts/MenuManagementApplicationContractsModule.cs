using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Jh.Abp.MenuManagement
{
    [DependsOn(
        typeof(MenuManagementDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class MenuManagementApplicationContractsModule : AbpModule
    {

    }
}
