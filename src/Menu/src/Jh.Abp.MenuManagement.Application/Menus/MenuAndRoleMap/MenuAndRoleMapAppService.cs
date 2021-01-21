﻿using Jh.Abp.Domain.Extensions;
using Jh.Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using Microsoft.EntityFrameworkCore;

namespace Jh.Abp.MenuManagement.Menus
{
    public class MenuAndRoleMapAppService
        : CrudApplicationService<MenuAndRoleMap, MenuAndRoleMapDto, MenuAndRoleMapDto, Guid, MenuAndRoleMapRetrieveInputDto, MenuAndRoleMapCreateInputDto, MenuAndRoleMapUpdateInputDto, MenuAndRoleMapDeleteInputDto>,
        IMenuAndRoleMapAppService
    {
        private IMenuRepository _menuRepository;
        protected IMenuRepository MenuRepository => LazyGetRequiredService(ref _menuRepository);

        private readonly IMenuAndRoleMapRepository MenuAndRoleMapRepository;
        public MenuAndRoleMapAppService(IMenuAndRoleMapRepository repository) : base(repository)
        {
            MenuAndRoleMapRepository = repository;

        }

        public override Task<MenuAndRoleMap> CreateAsync(MenuAndRoleMapCreateInputDto inputDto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<MenuAndRoleMap[]> CreateAsync(MenuAndRoleMapCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override async Task<MenuAndRoleMap> DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return await base.DeleteAsync(id, autoSave, cancellationToken);
        }

        public override async Task<MenuAndRoleMap[]> DeleteAsync(Guid[] keys, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return await base.DeleteAsync(keys, autoSave, cancellationToken);
        }

        public override async Task<MenuAndRoleMap[]> DeleteAsync(MenuAndRoleMapDeleteInputDto deleteInputDto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return await base.DeleteAsync(deleteInputDto, autoSave, cancellationToken);
        }

        [UnitOfWork]
        public async Task<MenuAndRoleMap[]> CreateV2Async(MenuAndRoleMapCreateInputDto inputDto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return await crudRepository.CreateAsync(GetCreateEnumerableAsync(inputDto).ToArray());
        }

        private IEnumerable<MenuAndRoleMap> GetCreateEnumerableAsync(MenuAndRoleMapCreateInputDto inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var roleid in inputDtos.RoleIds)
            {
                foreach (var menuid in inputDtos.MenuIds)
                {
                    yield return new MenuAndRoleMap(menuid, roleid, GuidGenerator.Create());
                }
            }
        }

        public async Task<IEnumerable<MenusTreeDto>> GetMenusTreesAsync(Guid roleid)
        {
            //查看CurrentUser.Roles 是的值是否为guid ,只能用一个角色的权限渲染菜单
            var auth_menus_id = crudRepository.Where(a => a.RoleId == roleid).Select(a=>a.MenuId);
            var menus = MenuRepository.Where(a => a.Use == UseType.Yes);

            //按照前端要求字段返回
            var auth_menus = await menus.Where(m => auth_menus_id.Contains(m.Id))
                .Select(a => new MenusTreeDto() { id = a.Code, icon = a.Icon, parent_id = a.ParentCode, sort = a.Sort, title = a.Name, url = a.Url}).ToListAsync();

            //返回多个根节点
            return GetMenusTreeDtosAsync(auth_menus);
        }

        public async Task<IEnumerable<MenusTreeDto>> GetAllMenusTreesAsync(Guid roleid)
        {
            var auth_menus_id = crudRepository.Where(a => a.RoleId == roleid).Select(a => a.MenuId).ToList();
            var menus = MenuRepository.Where(a => a.Use == UseType.Yes);

            var resutlMenus = await menus.Select(a =>
                new MenusTreeDto()
                {
                    id = a.Code,
                    icon = a.Icon,
                    parent_id = a.ParentCode,
                    sort = a.Sort,
                    title = a.Name,
                    url = a.Url,
                    value = a.Id.ToString(),
                    @checked = auth_menus_id.Contains(a.Id),
                    disabled = false
                }
            ).ToListAsync();

            //返回多个根节点
            return GetMenusTreeDtosAsync(resutlMenus);
        }

        
        private List<MenusTreeDto> GetMenusTreeDtosAsync(List<MenusTreeDto> menus)
        {
            //组装树
            IEnumerable<MenusTreeDto> GetChildNodes(string parentNodeId)
            {
                var childs = menus.Where(a => a.parent_id == parentNodeId);
                foreach (var item in childs)
                {
                    item.children =  GetChildNodes(item.id);
                }
                return childs;
            }

            //找到根节点
            var roots = menus.Where(a => a.parent_id == null).ToList();
            foreach (var item in roots)
            {
                item.children = GetChildNodes(item.id);
            }
            return roots;
        }
    }
}
