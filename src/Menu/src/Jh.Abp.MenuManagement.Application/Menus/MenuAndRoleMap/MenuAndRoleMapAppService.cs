using Jh.Abp.Common;
using Jh.Abp.Extensions;
using Jh.Abp.MenuManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace Jh.Abp.MenuManagement
{
    public class MenuAndRoleMapAppService
        : CrudApplicationService<MenuAndRoleMap, MenuAndRoleMapDto, MenuAndRoleMapDto, Guid, MenuAndRoleMapRetrieveInputDto, MenuAndRoleMapCreateInputDto, MenuAndRoleMapUpdateInputDto, MenuAndRoleMapDeleteInputDto>,
        IMenuAndRoleMapAppService
    {
        public IPermissionManager PermissionManager { get; set; }
        protected readonly IMenuRepository MenuRepository;
        public IPermissionDefinitionManager PermissionDefinitionManager { get; set; }
        public IdentityUserManager MyUserManager { get; set; }

        private readonly IMenuAndRoleMapRepository MenuAndRoleMapRepository;
        public MenuAndRoleMapAppService(IMenuAndRoleMapRepository repository, IMenuRepository menuRepository) : base(repository)
        {
            MenuAndRoleMapRepository = repository;
            MenuRepository = menuRepository;
        }

        public override Task<MenuAndRoleMap> CreateAsync(MenuAndRoleMapCreateInputDto inputDto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<MenuAndRoleMap[]> CreateAsync(MenuAndRoleMapCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<MenuAndRoleMap[]> CreateV2Async(MenuAndRoleMapCreateInputDto inputDto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return await crudRepository.CreateAsync(GetCreateEnumerableAsync(inputDto).ToArray());
        }

        protected virtual IEnumerable<MenuAndRoleMap> GetCreateEnumerableAsync(MenuAndRoleMapCreateInputDto inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var roleid in inputDtos.RoleIds)
            {
                //删除所有角色的权限
                DeleteAsync(new MenuAndRoleMapDeleteInputDto() { RoleId = roleid }).Wait();
                foreach (var menuid in inputDtos.MenuIds)
                {
                    yield return new MenuAndRoleMap(menuid, roleid);
                }
            }
        }

        public virtual async Task<IEnumerable<TreeDto>> GetMenusNavTreesAsync()
        {
            //查看CurrentUser.Roles 是的值是否为guid ,只能用一个角色的权限渲染菜单
            //var roles = CurrentUser.FindClaims(Common.Extensions.JhJwtClaimTypes.RoleId).Select(a => new Guid(a.Value)).ToList();
            var roles = await GetRolesAsync();
            //查看CurrentUser.Roles 是的值是否为guid ,只能用一个角色的权限渲染菜单
            var auth_menus_id = crudRepository.Where(a => roles.Contains(a.RoleId)).Select(a => a.MenuId).ToList();

            //按照前端要求字段返回
            var auth_menus = await MenuRepository.Where(m => auth_menus_id.Contains(m.Id))
                .Select(a => new TreeDto() { id = a.Code, icon = a.Icon, parent_id = a.ParentCode, sort = a.Sort, title = a.Name, url = a.Url,obj=a}).ToListAsync();

            //返回多个根节点
            return await UtilTree.GetMenusTreeAsync(auth_menus);
        }

        protected virtual async Task<IEnumerable<Guid>> GetRolesAsync()
        {
            var userid = CurrentUser.Id;
            var user = await MyUserManager.GetByIdAsync((Guid)userid);
            return user.Roles.Select(a => a.RoleId);
        }

        private MenuAndRoleMapTreeAllRetrieveInputDto menuAndRoleMapTreeAllRetrieveInputDto { get; set; }
        public virtual async Task<IEnumerable<TreeDto>> GetMenusTreesAsync(MenuAndRoleMapTreeAllRetrieveInputDto input)
        {
            menuAndRoleMapTreeAllRetrieveInputDto = input;
            var auth_menus_id = crudRepository.Where(a => a.RoleId == input.RoleId).Select(a => a.MenuId).ToList();

            var resutlMenus = await MenuRepository.Select(a =>
                new TreeDto()
                {
                    id = a.Code,
                    icon = a.Icon,
                    parent_id = a.ParentCode,
                    sort = a.Sort,
                    title = a.Name,
                    url = a.Url,
                    value = a.Id.ToString(),
                    @checked = auth_menus_id.Contains(a.Id),
                    disabled = false,
                    obj=a
                }
            ).ToListAsync();

            //返回多个根节点
            return await UtilTree.GetMenusTreeAsync(resutlMenus);
        }


        public virtual async Task<IEnumerable<TreeDto>> GetPermissionTreesAsync(string providerName, string providerKey)
        {
            var datas = await GetPermissionGrantsAsync();
            var results = new List<TreeDto>();
            foreach (var permission in datas)
            {
                var isGranted = await GetCurentUserByPermissionName(permission.Name + ".ManagePermissions", providerName);
                if (!isGranted)
                {
                    //判断当前登录用户的角色是否可以编辑当前菜单的权限，不能编辑就不显示了
                    continue;
                }
                var parentPermission = await PermissionManager.GetAsync(permission.Name, providerName, providerKey);//获取当前权限组的选中信息
                var module = permission.DisplayName.Localize(StringLocalizerFactory);//本地化当前权限的名称
                var modulePermission = new TreeDto()
                {
                    id = permission.Name,
                    title = module.Value,
                    value = permission.Name,
                    @checked = parentPermission.IsGranted,
                    disabled = false,
                };
                var childs = new List<TreeDto>() { modulePermission };
                {//childs
                    foreach (var item in permission.Children)//拿到当前权限组的子列表
                    {
                        var itemPermission = await PermissionManager.GetAsync(item.Name, providerName, providerKey);
                        var a = item.DisplayName.Localize(StringLocalizerFactory);
                        childs.Add(new TreeDto()
                        {
                            id = item.Name,
                            parent_id = permission.Name,
                            title = a.Value,
                            value = item.Name,
                            @checked = itemPermission.IsGranted,
                            disabled = false
                        });
                    }
                }
                results.Add(new TreeDto()
                {
                    id = $"yezi{permission.Name}",
                    title = module.Value,
                    value = $"yezi{permission.Name}",
                    @checked = parentPermission.IsGranted,
                    disabled = false,
                    children = childs
                });
            }
            return results;
        }

        public virtual Task<IEnumerable<PermissionDefinition>> GetPermissionGrantsAsync()
        {
            var multiTenancySide = CurrentTenant.GetMultiTenancySide();
            var datas = PermissionDefinitionManager.GetGroups()
                .SelectMany(g => g.Permissions)
                .Where(a => (a.Providers.Contains(RolePermissionValueProvider.ProviderName) || a.Providers.Count == 0)
                && a.IsEnabled
                && a.MultiTenancySide.HasFlag(multiTenancySide));
            return Task.FromResult(datas);
        }

        public virtual async Task UpdateAsync(string providerName, string providerKey, string[] permissionNames)
        {
            var multiTenancySide = CurrentTenant.GetMultiTenancySide();
            var permissions = PermissionDefinitionManager.GetPermissions()
                .Where(a => (a.Providers.Contains(RolePermissionValueProvider.ProviderName) || a.Providers.Count == 0)
                && a.IsEnabled
                && a.MultiTenancySide.HasFlag(multiTenancySide));
            var updatePermissionDtos = permissions.Select(p => new UpdatePermissionDto
            {
                Name = p.Name,
                IsGranted = permissionNames.ToNullList().Contains(p.Name)
            }).ToArray();
            //await permissionAppService.UpdateAsync(providerName, providerKey, new UpdatePermissionsDto() { Permissions = updatePermissionDtos });
            foreach (var permissionDto in updatePermissionDtos)
            {
                await PermissionManager.SetAsync(permissionDto.Name, providerName, providerKey, permissionDto.IsGranted);
            }
        }

        public virtual async Task<dynamic> GetMenuSelectPermissionGrantsAsync()
        {
            var result = new List<dynamic>();
            var datas = await GetPermissionGrantsAsync();
            foreach (var item in datas)
            {
                var module = item.DisplayName.Localize(StringLocalizerFactory);
                result.Add(new { name = module.Value, value = item.Name });
            }
            return result;
        }

        public virtual async Task<IEnumerable<PermissionGrantedDto>> GetPermissionGrantedByNameAsync(PermissionGrantedRetrieveInputDto input)
        {
            var result = new List<PermissionGrantedDto>();
            foreach (var permissionName in input.PermissionNames)
            {
                var isGranted = await GetCurentUserByPermissionName(permissionName, input.ProviderName);
                result.Add(new PermissionGrantedDto() { Name = permissionName, Granted = isGranted });
            }
            return result;
        }
        /// <summary>
        /// 只要当前用户有一个角色有权限就返回true
        /// </summary>
        /// <param name="permissionName"></param>
        /// <param name="providerName"></param>
        /// <returns></returns>
        protected async Task<bool> GetCurentUserByPermissionName(string permissionName, string providerName)
        {
            var isGranted = false;
            foreach (var providerKey in CurrentUser.Roles)
            {
                var managePermission = await PermissionManager.GetAsync(permissionName, providerName, providerKey);
                isGranted = managePermission.IsGranted;
                if (isGranted)
                {
                    break;
                }
            }
            return isGranted;
        }
    }
}
