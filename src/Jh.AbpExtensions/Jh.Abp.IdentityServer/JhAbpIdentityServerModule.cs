using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Modularity;

namespace Jh.Abp.IdentityServer
{
    public class JhAbpIdentityServerModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IIdentityServerBuilder>(identityServerBuilder =>
            {
                identityServerBuilder.AddProfileService<JhProfileServices>();
            });
        }
    }
}
