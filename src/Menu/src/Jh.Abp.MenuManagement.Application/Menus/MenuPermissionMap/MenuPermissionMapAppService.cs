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

namespace Jh.Abp.MenuManagement
{
    public class MenuPermissionMapAppService
        : CrudApplicationService<MenuPermissionMap, MenuPermissionMapDto, MenuPermissionMapDto, System.Guid, MenuPermissionMapRetrieveInputDto, MenuPermissionMapCreateInputDto, MenuPermissionMapUpdateInputDto, MenuPermissionMapDeleteInputDto>,
        IMenuPermissionMapAppService
    {
        public IPermissionAppService permissionAppService { get; set; }
        //protected IPermissionAppService permissionAppService => LazyServiceProvider.LazyGetRequiredService<IPermissionAppService>();
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
            var permissions = datas.Where(a => permissionNames.Contains(a.Name));
            var result = new List<MenusTreeDto>();
            foreach (var permission in permissions)
            {
                var module= permission.DisplayName.Localize(StringLocalizerFactory);
                result.Add(new MenusTreeDto()
                {
                    id = permission.Name,
                    title = module.Value,
                    value = permission.Name,
                    //@checked = auth_menus_id.Contains(a.Id),
                    @checked = false,
                    disabled = false
                });
                foreach (var item in permission.Children)
                {
                    var a = item.DisplayName.Localize(StringLocalizerFactory);
                    result.Add(new MenusTreeDto()
                    {
                        id = item.Name,
                        parent_id = permission.Name,
                        title = a.Value,
                        value = item.Name,
                        //@checked = auth_menus_id.Contains(a.Id),
                        @checked = false,
                        disabled = false
                    });
                }
            }
            return result;
        }

        public virtual async Task UpdateAsync(string providerName, string providerKey, string[] permissionNames)
        {
            var updatePermissionDtos = PermissionDefinitionManager.GetPermissions()
                .Select(p => new UpdatePermissionDto
                {
                    Name = p.Name,
                    IsGranted = permissionNames.ToNullList().Contains(p.Name)
                }).ToArray();
            await permissionAppService.UpdateAsync(providerName, providerKey, new UpdatePermissionsDto() { Permissions = updatePermissionDtos });
        }

        public virtual Task<IEnumerable<PermissionDefinition>> GetPermissionGrantsAsync()
        {
            var datas = PermissionDefinitionManager.GetGroups().SelectMany(g => g.Permissions);
            return Task.FromResult(datas);
        }

        public virtual async Task<dynamic> GetMenuSelectPermissionGrantsAsync()
        {
            var result = new List<dynamic>();
            var datas = await GetPermissionGrantsAsync();
            foreach (var item in PermissionDefinitionManager.GetGroups().SelectMany(g => g.Permissions))
            {
                var module = item.DisplayName.Localize(StringLocalizerFactory);
                result.Add(new { name = module.Value, value = item.Name });
            }
            return result;
        }
    }
}
