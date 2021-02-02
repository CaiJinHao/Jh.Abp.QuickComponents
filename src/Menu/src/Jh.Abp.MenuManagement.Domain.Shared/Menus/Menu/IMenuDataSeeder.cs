using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jh.Abp.MenuManagement
{
    public interface IMenuDataSeeder
    {
        Task SeedAsync(Guid roleid);
    }
}
