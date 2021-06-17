using Jh.Abp.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.IdentityResources;

namespace Jh.Abp.IdentityServer
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityResourceDataSeeder))]
    public class JhIdentityResourceDataSeeder : IdentityResourceDataSeeder, ITransientDependency
    {
        public JhIdentityResourceDataSeeder(IIdentityResourceRepository identityResourceRepository, IGuidGenerator guidGenerator, IIdentityClaimTypeRepository claimTypeRepository) : base(identityResourceRepository, guidGenerator, claimTypeRepository)
        {
        }

        public override async Task CreateStandardResourcesAsync()
        {
            //只有这里添加了才能被添加到Claims中
            var resources = new[]
             {
                new IdentityServer4.Models.IdentityResources.OpenId(),
                new IdentityServer4.Models.IdentityResources.Profile(),
                new IdentityServer4.Models.IdentityResources.Email(),
                new IdentityServer4.Models.IdentityResources.Address(),
                new IdentityServer4.Models.IdentityResources.Phone(),
                new IdentityServer4.Models.IdentityResource("role", "Roles of the user", new[] {"role",JhJwtClaimTypes.RoleId})
            };

            foreach (var resource in resources)
            {
                foreach (var claimType in resource.UserClaims)
                {
                    await AddClaimTypeIfNotExistsAsync(claimType);
                }

                await AddIdentityResourceIfNotExistsAsync(resource);
            }
        }
    }
}
