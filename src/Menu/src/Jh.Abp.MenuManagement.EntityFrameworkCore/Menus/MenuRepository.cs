using Jh.Abp.EntityFrameworkCore.Extensions;
using Jh.Abp.MenuManagement.EntityFrameworkCore;
using System;
using Volo.Abp.EntityFrameworkCore;

namespace Jh.Abp.MenuManagement.Menus
{
    public class MenuRepository : CrudRepository<MenuManagementDbContext, Menu, Guid>, IMenuRepository
    {
        public MenuRepository(IDbContextProvider<MenuManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
