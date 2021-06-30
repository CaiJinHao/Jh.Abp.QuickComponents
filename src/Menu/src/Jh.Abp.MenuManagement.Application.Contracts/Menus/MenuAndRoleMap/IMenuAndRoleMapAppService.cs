using Jh.Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;

namespace Jh.Abp.MenuManagement
{
    public interface IMenuAndRoleMapAppService
        :ICrudApplicationService<MenuAndRoleMap, MenuAndRoleMapDto, MenuAndRoleMapDto, Guid, MenuAndRoleMapRetrieveInputDto, MenuAndRoleMapCreateInputDto, MenuAndRoleMapUpdateInputDto, MenuAndRoleMapDeleteInputDto>
    {
        Task<MenuAndRoleMap[]> CreateV2Async(MenuAndRoleMapCreateInputDto inputDto, bool autoSave = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取当前登录角色有权限的菜单树
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MenusNavDto>> GetMenusNavTreesAsync();
        /// <summary>
        /// 获取所有菜单树，有权限的自动选中
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MenusTreeDto>> GetMenusTreesAsync(MenuAndRoleMapTreeAllRetrieveInputDto input);



        Task UpdateAsync(string providerName, string providerKey, string[] PermissionNames);

        Task<IEnumerable<PermissionDefinition>> GetPermissionGrantsAsync();

        Task<dynamic> GetMenuSelectPermissionGrantsAsync();

        Task<IEnumerable<PermissionGrantedDto>> GetPermissionGrantedByNameAsync(PermissionGrantedRetrieveInputDto input);
        Task<IEnumerable<MenusTreeDto>> GetPermissionTreesAsync(string providerName, string providerKey);
    }
}
