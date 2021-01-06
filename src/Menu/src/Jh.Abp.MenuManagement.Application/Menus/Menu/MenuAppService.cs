using Jh.Abp.Domain.Extensions;
using Jh.Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Jh.Abp.MenuManagement.Menus
{
    public class MenuAppService
        : CrudApplicationService<Menu, MenuDto, MenuDto, Guid, MenuRetrieveInputDto, MenuCreateInputDto, MenuUpdateInputDto, MenuDeleteInputDto>
        , IMenuAppService
    {
        private readonly IMenuRepository menuRepository;

        private readonly IMenuAndRoleMapDomainService menuAndRoleMapDomainService;
        private readonly IMenuAndRoleMapRepository menuAndRoleMapRepository;

        private IMenuAndRoleMapAppService _menuAndRoleMapAppService;
        public IMenuAndRoleMapAppService menuAndRoleMapAppService => LazyGetRequiredService(ref _menuAndRoleMapAppService);

        public MenuAppService(IMenuRepository repository, IMenuAndRoleMapRepository _menuAndRoleMapRepository, IMenuAndRoleMapDomainService _menuAndRoleMapDomainService) : base(repository)
        {
            menuRepository = repository;
            menuAndRoleMapDomainService = _menuAndRoleMapDomainService;
            menuAndRoleMapRepository = _menuAndRoleMapRepository;
        }

        public override async Task<Menu> CreateAsync(MenuCreateInputDto inputDto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entity = await base.CreateAsync(inputDto, autoSave, cancellationToken);
            await menuAndRoleMapDomainService.CreateAsync(inputDto.RoleIds, entity.Id, autoSave, cancellationToken);
            return entity;
        }

        private IEnumerable<Menu> EnumerableCreateAsync(MenuCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var item in inputDtos)
            {
                yield return this.CreateAsync(item, autoSave, cancellationToken).Result;
            }
        }

        public override Task<Menu[]> CreateAsync(MenuCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(EnumerableCreateAsync(inputDtos, autoSave, cancellationToken).ToArray());
        }

        public override async Task<Menu> DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entity = await base.DeleteAsync(id, autoSave, cancellationToken);
            await menuAndRoleMapRepository.DeleteListAsync(a => a.MenuId == entity.Id).ConfigureAwait(false);
            return entity;
        }

        public override async Task<Menu[]> DeleteAsync(Guid[] keys, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entitys = await base.DeleteAsync(keys, autoSave, cancellationToken);
            await menuAndRoleMapRepository.DeleteListAsync(a => keys.Contains(a.MenuId)).ConfigureAwait(false);
            return entitys;
        }

        public override async Task<Menu[]> DeleteAsync(MenuDeleteInputDto deleteInputDto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entitys = await base.DeleteAsync(deleteInputDto,  autoSave, cancellationToken);
            await menuAndRoleMapRepository.DeleteListAsync(a => entitys.Select(b => b.Id).Contains(a.MenuId)).ConfigureAwait(false);
            return entitys;
        }

        public async Task<IEnumerable<MenusTreeDto>> GetMenusTreesAsync(Guid roleid)
        {
            var menus = await GetListAsync(new MenuRetrieveInputDto() { Use = UseType.Yes });
            //查看CurrentUser.Roles 是的值是否为guid ,只能用一个角色的权限渲染菜单
            var auth_menus = await menuAndRoleMapRepository.Where(a => a.RoleId == roleid).ToListAsync();

            var test1 = await menuAndRoleMapAppService.GetEntitysAsync(new MenuAndRoleMapRetrieveInputDto() { RoleId = roleid });
            var expressTest = Jh.Abp.Common.Linq.LinqExpression.True<MenuAndRoleMap>();
            expressTest=expressTest.And(a => a.RoleId == roleid);
            var test2 = await menuAndRoleMapAppService.GetEntitysAsync(new MenuAndRoleMapRetrieveInputDto(), expressTest);
            //TODO:获取菜单树
            throw new Exception();
        }
    }
}
