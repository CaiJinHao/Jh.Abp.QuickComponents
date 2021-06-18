using Jh.Abp.Extensions;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.EventBus.Local;
using Volo.Abp.PermissionManagement;
using Microsoft.Extensions.Localization;
using Jh.Abp.Common;
using Volo.Abp.MultiTenancy;

namespace Jh.Abp.MenuManagement
{
    public class MenuPermissionMapAppService
        : CrudApplicationService<MenuPermissionMap, MenuPermissionMapDto, MenuPermissionMapDto, System.Guid, MenuPermissionMapRetrieveInputDto, MenuPermissionMapCreateInputDto, MenuPermissionMapUpdateInputDto, MenuPermissionMapDeleteInputDto>,
        IMenuPermissionMapAppService
    {
        public IPermissionManager PermissionManager { get; set; }
        public IPermissionAppService permissionAppService { get; set; }
        public IPermissionDefinitionManager PermissionDefinitionManager { get; set; }
        private readonly IMenuPermissionMapRepository MenuPermissionMapRepository;
        private readonly IMenuPermissionMapDapperRepository MenuPermissionMapDapperRepository;
        public MenuPermissionMapAppService(IMenuPermissionMapRepository repository, IMenuPermissionMapDapperRepository menupermissionmapDapperRepository) : base(repository)
        {
            MenuPermissionMapRepository = repository;
            MenuPermissionMapDapperRepository = menupermissionmapDapperRepository;
        }

        public async Task<IEnumerable<MenusTreeDto>> GetPermissionTreesAsync(Guid menuid, string providerName, string providerKey)
        {
            var permissionNames = (await base.GetEntitysAsync(new MenuPermissionMapRetrieveInputDto()
            {
                MenuId = menuid
            })).Items.Select(a => a.PermissionName);
            var datas = await GetPermissionGrantsAsync();
            var permissions = datas.Where(a => permissionNames.Contains(a.Name));//当前菜单绑定的所有权限组
            var results = new List<MenusTreeDto>();
            foreach (var permission in permissions)
            {
                var isGranted = await GetCurentUserByPermissionName(permission.Name + ".ManagePermissions",providerName);
                if (!isGranted)
                {
                    //判断当前登录用户的角色是否可以编辑当前菜单的权限，不能编辑就不显示了
                    continue;
                }
                var parentPermission = await PermissionManager.GetAsync(permission.Name, providerName, providerKey);//获取当前权限组的选中信息
                var module = permission.DisplayName.Localize(StringLocalizerFactory);//本地化当前权限的名称
                var modulePermission = new MenusTreeDto()
                {
                    id = permission.Name,
                    title = module.Value,
                    value = permission.Name,
                    @checked = parentPermission.IsGranted,
                    disabled = false,
                };
                var childs = new List<MenusTreeDto>() { modulePermission };
                {//childs
                    foreach (var item in permission.Children)//拿到当前权限组的子列表
                    {
                        var itemPermission = await PermissionManager.GetAsync(item.Name, providerName, providerKey);
                        var a = item.DisplayName.Localize(StringLocalizerFactory);
                        childs.Add(new MenusTreeDto()
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
                results.Add(new MenusTreeDto()
                {
                    id = $"yezi{permission.Name}",
                    title = module.Value,
                    value = $"yezi{permission.Name}",
                    @checked = parentPermission.IsGranted,
                    disabled = false,
                    data = childs
                });
            }
            return results;
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

        public virtual Task<IEnumerable<PermissionDefinition>> GetPermissionGrantsAsync()
        {
            var multiTenancySide = CurrentTenant.GetMultiTenancySide();
            var datas = PermissionDefinitionManager.GetGroups()
                .SelectMany(g => g.Permissions)
                .Where(a => (a.Providers.Contains(RolePermissionValueProvider.ProviderName)|| a.Providers.Count == 0)
                &&a.IsEnabled
                && a.MultiTenancySide.HasFlag(multiTenancySide));
            return Task.FromResult(datas);
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
        protected async Task<bool> GetCurentUserByPermissionName(string permissionName,string providerName)
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
