using Volo.Abp.Modularity;

namespace Jh.Abp.MenuManagement
{
    [DependsOn(
        typeof(MenuManagementApplicationModule),
        typeof(MenuManagementDomainTestModule)
        )]
    public class MenuManagementApplicationTestModule : AbpModule
    {

    }
}
