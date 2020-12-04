using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Jh.Abp.MenuManagement
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(MenuManagementDomainSharedModule)
    )]
    public class MenuManagementDomainModule : AbpModule
    {

    }
}
