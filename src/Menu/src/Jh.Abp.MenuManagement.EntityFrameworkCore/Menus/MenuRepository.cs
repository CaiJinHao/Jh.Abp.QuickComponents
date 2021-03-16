using Jh.Abp.EntityFrameworkCore.Extensions;
using Jh.Abp.MenuManagement.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Dapper;
using Volo.Abp.DependencyInjection;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            throw new Exception("not use dto");
        }
    }

    /// <summary>
    /// 将带Dto的接口放到Contracts，如果已经有了一个Dto的接口，直接用就可以
    /// </summary>
    public class MenuDtoRepository : IMenuDtoRepository, ITransientDependency
    {
        public IMenuDapperRepository MenuDapperRepository { get; set; }
        public IMenuRepository menusRepository { get; set; }
        public async Task<IEnumerable<MenuDto>> GetDtoDapperListAsync()
        {
            var data = await menusRepository.ToListAsync();
            return data.Select(a => new MenuDto()
            {
                Code = a.Code,
                Id = a.Id
            });
            //return await MenuDapperRepository.GetDapperListAsync();
        }
    }
}
