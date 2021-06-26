using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jh.Abp.IdentityServer.JhDataSeedContributor
{
    public interface ITenantDataSeeder
    {
        Task SeedAsync(params string[] names);
        Task<Guid> SeedAsync(string name);
    }
}
