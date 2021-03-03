using Jh.Abp.EntityFrameworkCore.Extensions;
using Jh.Abp.MenuManagement.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Dapper;

namespace Jh.Abp.MenuManagement.Menus
{
    public class MenuRepository : CrudRepository<MenuManagementDbContext, Menu, Guid>, IMenuRepository
    {
        public IMenuDapperRepository menuDapperRepository { get; set; }

        public MenuRepository(IDbContextProvider<MenuManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<IEnumerable<Menu>> GetDapperListAsync()
        {
            return await menuDapperRepository.GetDapperListAsync();
        }
    }
}
