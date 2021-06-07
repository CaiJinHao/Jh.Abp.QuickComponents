using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Jh.Abp.Application.Contracts;

namespace Jh.Abp.MenuManagement
{
    [DependsOn(
        typeof(JhAbpContractsModule),
        typeof(MenuManagementDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class MenuManagementApplicationContractsModule : AbpModule
    {

    }
}
