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
        private readonly IMenuDapperRepository MenuDapperRepository;

        public MenuRepository(IDbContextProvider<MenuManagementDbContext> dbContextProvider
            , IMenuDapperRepository menuDapperRepository) : base(dbContextProvider)
        {
            MenuDapperRepository = menuDapperRepository;
        }

        public async Task<IEnumerable<Menu>> GetDapperListAsync()
        {
            //return await MenuDapperRepository.GetDapperListAsync();
            throw new Exception("not use dto");
        }
    }
}
