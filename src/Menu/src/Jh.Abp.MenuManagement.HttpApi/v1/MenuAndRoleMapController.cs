using Jh.Abp.MenuManagement.Menus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Jh.Abp.MenuManagement.v1
{
    [RemoteService]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class MenuAndRoleMapController : MenuManagementController
    {
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
        [HttpPost]
        public async Task<MenuAndRoleMap[]> CreateAsync(MenuAndRoleMapCreateInputDto input)
        {
            return await menuAndRoleMapAppService.CreateV2Async(input);
        }

        /// <summary>
        /// 根据条件删除多条
        /// </summary>
        /// <param name="deleteInputDto"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MenuAndRoleMap[]> DeleteAsync([FromQuery] MenuAndRoleMapDeleteInputDto deleteInputDto)
        {
            return await menuAndRoleMapAppService.DeleteAsync(deleteInputDto);
        }

        /// <summary>
        /// 根据主键删除多条
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [Route("keys")]
        [HttpDelete]
        public async Task<MenuAndRoleMap[]> DeleteAsync(Guid[] keys)
        {
            return await menuAndRoleMapAppService.DeleteAsync(keys);
        }

        /// <summary>
        /// 根据条件查询(不分页)
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [Route("list")]
        [HttpGet]
        public async Task<ListResultDto<MenuAndRoleMapDto>> GetEntitysAsync([FromQuery] MenuAndRoleMapRetrieveInputDto inputDto)
        {
            return await menuAndRoleMapAppService.GetEntitysAsync(inputDto);
        }

        /// <summary>
        /// 根据id更新部分
        /// </summary>
        /// <param name="key"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<MenuAndRoleMap> UpdatePortionAsync(Guid key, MenuAndRoleMapUpdateInputDto inputDto)
        {
            return await menuAndRoleMapAppService.UpdatePortionAsync(key, inputDto);
        }

        /// <summary>
        /// 根据ID更新全部
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("id")]
        public async Task<MenuAndRoleMapDto> UpdateAsync(Guid id, MenuAndRoleMapUpdateInputDto input)
        {
            return await menuAndRoleMapAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<MenuAndRoleMapDto>> GetListAsync([FromQuery] MenuAndRoleMapRetrieveInputDto input)
        {
            return await menuAndRoleMapAppService.GetListAsync(input);
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("id")]
        public async Task<MenuAndRoleMap> DeleteAsync(Guid id)
        {
            return await menuAndRoleMapAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<MenuAndRoleMapDto> GetAsync(Guid id)
        {
            return await menuAndRoleMapAppService.GetAsync(id);
        }

        [HttpGet("Trees")]
        public async Task<dynamic> GetMenusNavTreesAsync()
        {
            var roleid = CurrentUser.FindClaim(Common.Extensions.JhJwtClaimTypes.RoleId);
            var items = await menuAndRoleMapAppService.GetMenusNavTreesAsync(new Guid(roleid.Value));
            return new { items };
        }

        [HttpGet("TreesAll")]
        public async Task<dynamic> GetMenusTreesAsync(Guid roleId)
        {
            var items = await menuAndRoleMapAppService.GetMenusTreesAsync(roleId);
            return new { items };
        }
    }
}
