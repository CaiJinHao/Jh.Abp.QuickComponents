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

        public async Task<IEnumerable<MenusTreeDto>> GetMenusTreesAsync(string providerName, string providerKey)
        {
            //TODO:Permission
            var datas = PermissionDefinitionManager.GetGroups();
            var _t = PermissionDefinitionManager.GetPermissions();
            await Task.CompletedTask;
            return default;
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
            return datas.Select(a=>a.DisplayName.Localize(StringLocalizerFactory));
        }
    }
}
