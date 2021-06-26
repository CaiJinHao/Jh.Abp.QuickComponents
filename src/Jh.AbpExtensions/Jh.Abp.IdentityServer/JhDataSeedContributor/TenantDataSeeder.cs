using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.TenantManagement;

namespace Jh.Abp.IdentityServer.JhDataSeedContributor
{
    public class TenantDataSeeder: ITenantDataSeeder, ITransientDependency
    {
        protected ITenantRepository TenantRepository { get; }
        protected ITenantManager TenantManager { get; }

        public TenantDataSeeder(ITenantManager tenantManager, ITenantRepository tenantRepository)
        {
            TenantManager = tenantManager;
            TenantRepository = tenantRepository;
        }

        public async Task SeedAsync(params string[] names)
        {
            foreach (var item in names)
            {
                await SeedAsync(item);
            }
        }

        public async Task<Guid> SeedAsync(string name)
        {
            var tenant = await TenantRepository.FindByNameAsync(name);
            if (tenant == null)
            {
                tenant = await TenantManager.CreateAsync(name);
                await TenantRepository.InsertAsync(tenant);
            }
            return tenant.Id;
        }
    }
}
