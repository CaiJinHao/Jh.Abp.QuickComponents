using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.TenantManagement;

namespace Jh.Abp.IdentityServer.JhDataSeedContributor
{
    public class TenantDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        protected ITenantDataSeeder tenantDataSeeder { get; }

        public TenantDataSeedContributor(ITenantDataSeeder _tenantDataSeeder)
        {
            tenantDataSeeder = _tenantDataSeeder;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var Tenants = context?["Tenants"] as string[];
            if (Tenants != null && Tenants.Any())
            {
                await tenantDataSeeder.SeedAsync(Tenants);
            }
        }
    }
}
