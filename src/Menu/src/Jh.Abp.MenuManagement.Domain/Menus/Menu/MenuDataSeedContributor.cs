using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Jh.Abp.MenuManagement
{
    public class MenuDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        protected IMenuDataSeeder MenuDataSeeder { get; }

        public MenuDataSeedContributor(IMenuDataSeeder menuDataSeeder)
        {
            MenuDataSeeder = menuDataSeeder;
        }
        public Task SeedAsync(DataSeedContext context)
        {
            var roleid = context?["RoleId"] as string;
            if (string.IsNullOrEmpty(roleid))
            {
                throw new Exception("请传入admin角色id");
            }
            return MenuDataSeeder.SeedAsync(new Guid(roleid));
        }
    }
}
