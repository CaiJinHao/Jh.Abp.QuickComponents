using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Jh.Abp.MenuManagement
{
    [DependsOn(
        typeof(MenuManagementHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class MenuManagementConsoleApiClientModule : AbpModule
    {
        
    }
}
