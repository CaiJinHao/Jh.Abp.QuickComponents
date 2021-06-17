
using Jh.Abp.MenuManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization.Permissions;

namespace Jh.Abp.MenuManagement.v1
{
    [RemoteService]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class MenuAndRoleMapController : MenuManagementController
    {
        public IMenuPermissionMapAppService menuPermissionMapAppService { get; set; }
        private readonly IMenuAndRoleMapAppService menuAndRoleMapAppService;
        public MenuAndRoleMapController(IMenuAndRoleMapAppService _menuAndRoleMapAppService)
        {
            menuAndRoleMapAppService = _menuAndRoleMapAppService;
        }

        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorize(MenuManagementPermissions.MenuAndRoleMaps.Create)]
        [HttpPost]
        public virtual async Task<MenuAndRoleMap[]> CreateAsync(MenuAndRoleMapCreateInputDto input)
        {
            return await menuAndRoleMapAppService.CreateV2Async(input);
        }

        /// <summary>
        /// 根据条件删除多条
        /// </summary>
        /// <param name="deleteInputDto"></param>
        /// <returns></returns>
		[Authorize(MenuManagementPermissions.MenuAndRoleMaps.Delete)]
        [HttpDelete]
        public virtual async Task<MenuAndRoleMap[]> DeleteAsync([FromQuery] MenuAndRoleMapDeleteInputDto deleteInputDto)
        {
            return await menuAndRoleMapAppService.DeleteAsync(deleteInputDto);
        }

        /// <summary>
        /// 根据主键删除多条
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
		[Authorize(MenuManagementPermissions.MenuAndRoleMaps.Delete)]
        [Route("keys")]
        [HttpDelete]
        public virtual async Task<MenuAndRoleMap[]> DeleteAsync(Guid[] keys)
        {
            return await menuAndRoleMapAppService.DeleteAsync(keys);
        }

        /// <summary>
        /// 根据条件查询(不分页)
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
		[Authorize(MenuManagementPermissions.MenuAndRoleMaps.Default)]
        [Route("all")]
        [HttpGet]
        public virtual async Task<ListResultDto<MenuAndRoleMapDto>> GetEntitysAsync([FromQuery] MenuAndRoleMapRetrieveInputDto inputDto)
        {
            return await menuAndRoleMapAppService.GetEntitysAsync(inputDto);
        }

        /// <summary>
        /// 根据id更新部分
        /// </summary>
        /// <param name="key"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
		[Authorize(MenuManagementPermissions.MenuAndRoleMaps.Update)]
        [HttpPatch]
        public virtual async Task<MenuAndRoleMap> UpdatePortionAsync(Guid key, MenuAndRoleMapUpdateInputDto inputDto)
        {
            return await menuAndRoleMapAppService.UpdatePortionAsync(key, inputDto);
        }

        /// <summary>
        /// 根据ID更新全部
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorize(MenuManagementPermissions.MenuAndRoleMaps.Update)]
        [HttpPut("id")]
        public virtual async Task<MenuAndRoleMapDto> UpdateAsync(Guid id, MenuAndRoleMapUpdateInputDto input)
        {
            return await menuAndRoleMapAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
		[Authorize(MenuManagementPermissions.MenuAndRoleMaps.Default)]
        [HttpGet]
        public virtual async Task<PagedResultDto<MenuAndRoleMapDto>> GetListAsync([FromQuery] MenuAndRoleMapRetrieveInputDto input)
        {
            return await menuAndRoleMapAppService.GetListAsync(input);
        }

        [Authorize(MenuManagementPermissions.MenuAndRoleMaps.Default)]
        [HttpGet]
        [Route("{id}/Permissions")]
        public virtual async Task<IEnumerable<string>> GetPermissionsAsync(Guid id)
        {
            var datas = await menuPermissionMapAppService.GetEntitysAsync(new MenuPermissionMapRetrieveInputDto()
            {
                MenuId = id
            });
            return datas.Items.Select(a => a.PermissionName).ToList();
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		[Authorize(MenuManagementPermissions.MenuAndRoleMaps.Delete)]
        [HttpDelete("id")]
        public virtual async Task<MenuAndRoleMap> DeleteAsync(Guid id)
        {
            return await menuAndRoleMapAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		[Authorize(MenuManagementPermissions.MenuAndRoleMaps.Default)]
        [HttpGet("id")]
        public virtual async Task<MenuAndRoleMapDto> GetAsync(Guid id)
        {
            return await menuAndRoleMapAppService.GetAsync(id);
        }

		[Authorize(MenuManagementPermissions.MenuAndRoleMaps.Default)]
        [HttpGet("Trees")]
        public virtual async Task<dynamic> GetMenusNavTreesAsync()
        {
            var items = await menuAndRoleMapAppService.GetMenusNavTreesAsync();
            return new { items };
        }

		[Authorize(MenuManagementPermissions.MenuAndRoleMaps.Default)]
        [HttpGet("TreesAll")]
        public virtual async Task<dynamic> GetMenusTreesAsync([FromQuery]MenuAndRoleMapTreeAllRetrieveInputDto input)
        {
            var items = await menuAndRoleMapAppService.GetMenusTreesAsync(input);
            return new { items };
        }

        [HttpPost("PermissionGranted")]
        public virtual async Task<IEnumerable<PermissionGrantedDto>> GetPermissionGrantedByNameAsync([FromBody] PermissionGrantedRetrieveInputDto input)
        {
            return await menuPermissionMapAppService.GetPermissionGrantedByNameAsync(input);
        }
    }
}
