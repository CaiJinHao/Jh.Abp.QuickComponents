using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Jh.Abp.Domain.Shared;

namespace Jh.Abp.Application.Contracts
{
    [DependsOn(
       typeof(JhAbpExtensionsDomainSharedModule),
       typeof(AbpDddApplicationContractsModule),
       typeof(AbpAuthorizationModule)
       )]
    public class JhAbpContractsModule : AbpModule
    {
    }
}
