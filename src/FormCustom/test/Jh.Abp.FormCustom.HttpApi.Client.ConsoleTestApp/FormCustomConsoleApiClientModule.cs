using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Jh.Abp.FormCustom
{
    [DependsOn(
        typeof(FormCustomHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class FormCustomConsoleApiClientModule : AbpModule
    {
        
    }
}
