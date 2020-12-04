using Jh.Abp.EntityFrameworkCore.Extensions;
using Jh.Abp.MenuManagement.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;

namespace Jh.Abp.MenuManagement.Menus
{
    public class MenuAndRoleMapRepository : CrudRepository<MenuManagementDbContext, MenuAndRoleMap, Guid>, IMenuAndRoleMapRepository
    {
        public MenuAndRoleMapRepository(IDbContextProvider<MenuManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
