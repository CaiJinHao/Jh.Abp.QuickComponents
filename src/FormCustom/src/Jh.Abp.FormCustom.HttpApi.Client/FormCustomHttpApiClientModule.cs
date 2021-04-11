using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Jh.Abp.FormCustom
{
    [DependsOn(
        typeof(FormCustomApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class FormCustomHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "FormCustom";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(FormCustomApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
