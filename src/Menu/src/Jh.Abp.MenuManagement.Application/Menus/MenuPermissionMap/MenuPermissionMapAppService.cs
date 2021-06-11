using Jh.Abp.Extensions;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.EventBus.Local;
using Volo.Abp.PermissionManagement;
using Microsoft.Extensions.Localization;

namespace Jh.Abp.MenuManagement
{
    public class MenuPermissionMapAppService
        : CrudApplicationService<MenuPermissionMap, MenuPermissionMapDto, MenuPermissionMapDto, System.Guid, MenuPermissionMapRetrieveInputDto, MenuPermissionMapCreateInputDto, MenuPermissionMapUpdateInputDto, MenuPermissionMapDeleteInputDto>,
        IMenuPermissionMapAppService
    {
        protected IPermissionAppService permissionAppService => LazyServiceProvider.LazyGetRequiredService<IPermissionAppService>();
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
                foreach (var item in permission.Children)
                {
                    var a = item.DisplayName.Localize(StringLocalizerFactory);
                    result.Add(new MenusTreeDto()
                    {
                        id = a.Value,
                        parent_id = permission.Name,
                        title = a.Name,
                        value = a.Value,
                        //@checked = auth_menus_id.Contains(a.Id),
                        @checked = false,
                        disabled = false
                    });
                }
            }
            return result;
        }

        public virtual async Task UpdateAsync(string providerName, string providerKey, UpdatePermissionsDto input)
        {
            await permissionAppService.UpdateAsync(providerName, providerKey, input);
        }

        public virtual Task<List<PermissionDefinition>> GetPermissionGrantsAsync()
        {
            var modules = new List<PermissionDefinition>();
            foreach (var group in PermissionDefinitionManager.GetGroups())
            {
                modules.AddRange(group.Permissions);
            }
            return Task.FromResult(modules);
        }

        public virtual async Task<IEnumerable<LocalizedString>> GetLocalizePermissionGrantsAsync()
        {
            var datas = await GetPermissionGrantsAsync();
            return datas.Select(a => a.DisplayName.Localize(StringLocalizerFactory));
        }
    }
}
