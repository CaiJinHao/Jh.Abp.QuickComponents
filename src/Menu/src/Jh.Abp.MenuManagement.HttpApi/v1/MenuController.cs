using Jh.Abp.MenuManagement.Menus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using System.Linq;

namespace Jh.Abp.MenuManagement.v1
{
    [RemoteService]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class MenuController: MenuManagementController
    {
        private readonly IMenuAppService menuAppService;
        public MenuController(IMenuAppService _menuAppService)
        {
            menuAppService = _menuAppService;
        }

        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Menu> CreateAsync(MenuCreateInputDto input)
        {
            return await menuAppService.CreateAsync(input);
        }

        /// <summary>
        /// 创建多个实体
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("list")]
        [HttpPost]
        public async Task<Menu[]> CreateAsync(MenuCreateInputDto[] input)
        {
            return await menuAppService.CreateAsync(input);
        }

        /// <summary>
        /// 根据条件删除多条
        /// </summary>
        /// <param name="deleteInputDto"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<Menu[]> DeleteAsync(MenuDeleteInputDto deleteInputDto)
        {
            return await menuAppService.DeleteAsync(deleteInputDto);
        }

        /// <summary>
        /// 根据主键删除多条
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [Route("keys")]
        [HttpDelete]
        public async Task<Menu[]> DeleteAsync(Guid[] keys)
        {
            return await menuAppService.DeleteAsync(keys);
        }

        /// <summary>
        /// 根据条件查询(不分页)
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [Route("list")]
        [HttpGet]
        public async Task<List<MenuDto>> GetEntitysAsync(MenuRetrieveInputDto inputDto)
        {
            var expressTest = Jh.Abp.Common.Linq.LinqExpression.True<Menu>();
            expressTest = expressTest.And(a=>a.Code== "A0101");
            return await menuAppService.GetEntitysAsync(inputDto, expressTest);
        }

        /// <summary>
        /// 根据id更新部分
        /// </summary>
        /// <param name="key"></param>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<Menu> UpdatePortionAsync(Guid key, MenuUpdateInputDto inputDto)
        {
            return await menuAppService.UpdatePortionAsync(key, inputDto);
        }

        /// <summary>
        /// 根据ID更新全部
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("id")]
        public async Task<MenuDto> UpdateAsync(Guid id, MenuUpdateInputDto input)
        {
            return await menuAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<MenuDto>> GetListAsync(MenuRetrieveInputDto input)
        {
            return await menuAppService.GetListAsync(input);
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("id")]
        public async Task<Menu> DeleteAsync(Guid id)
        {
            return await menuAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<MenuDto> GetAsync(Guid id)
        {
            return await menuAppService.GetAsync(id);
        }

        [HttpGet("Trees")]
        public async Task<IEnumerable<MenusTreeDto>> GetMenusTreesAsync()
        {
            var id = CurrentUser.Roles[0];
            return await menuAppService.GetMenusTreesAsync(new Guid(id));
        }

        [Route("claims")]
        [HttpGet]
        public dynamic GetClaimsAsync()
        {
            return HttpContext.User.Claims;
        }
    }
}
