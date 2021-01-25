﻿using Jh.Abp.MenuManagement.Menus;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Identity;

namespace Jh.Abp.MenuManagement.v1
{
    [RemoteService]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class UserController : MenuManagementController
    {
        public IDataFilter<ISoftDelete> dataFilter { get; set; }
        protected IIdentityUserAppService UserAppService { get; }
        protected IProfileAppService ProfileAppService { get; }
        protected IdentityUserManager UserManager { get; }
        protected IIdentityUserRepository UserRepository { get; }

        public UserController(
            IProfileAppService profileAppService,
            IIdentityUserAppService userAppService,
            IIdentityUserRepository userRepository,
            IdentityUserManager userManager)
        {
            UserAppService = userAppService;
            ProfileAppService = profileAppService;
            UserRepository = userRepository;
            UserManager = userManager;
        }

        [HttpGet]
        [Route("{id}/roles")]
        public virtual async Task<dynamic> GetRolesToSelectAsync(Guid id)
        {
            var datas = await UserRepository.GetRolesAsync(id);
            return new
            {
                items = datas.Select(a => new { name = a.Name, value = a.Id })
            };
        }

        [HttpGet]
        public async Task<PagedResultDto<IdentityUserDto>> GetListAsync(GetIdentityUsersInput input)
        {
            using (dataFilter.Disable())
            {
                return await UserAppService.GetListAsync(input);
            }
        }

        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("info")]
        public virtual Task<ProfileDto> GetLoginInfoAsync()
        {
            return ProfileAppService.GetAsync();
        }

        [HttpPost]
        [Route("change-password")]
        public virtual Task ChangePasswordAsync(ChangePasswordInput input)
        {
            return ProfileAppService.ChangePasswordAsync(input);
        }


        [HttpPatch]
        [Route("{id}/lockoutEnabled")]
        public virtual async Task UpdateLockoutEnabledAsync(Guid id, [FromBody] bool lockoutEnabled)
        {
            using (dataFilter.Disable())
            {
                var user = await UserManager.GetByIdAsync(id);
                (await UserManager.SetLockoutEnabledAsync(user, lockoutEnabled)).CheckErrors();
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        [HttpPatch]
        [Route("{id}/Deleted")]
        public virtual async Task UpdateDeletedAsync(Guid id, [FromBody] bool isDeleted)
        {
            using (dataFilter.Disable())
            {
                var user = await UserManager.GetByIdAsync(id);
                user.IsDeleted = isDeleted;
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return UserAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 根据主键删除多条
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        [Route("keys")]
        [HttpDelete]
        public async Task DeleteAsync([FromBody] Guid[] keys)
        {
            foreach (var item in keys)
            {
                await UserAppService.DeleteAsync(item);
            }
        }
    }
}
