using Jh.Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jh.Abp.MenuManagement.Menus
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
        /// <param name="roleid"></param>
        /// <returns></returns>
        Task<IEnumerable<MenusTreeDto>> GetMenusTreesAsync(Guid roleid);
    }
}
