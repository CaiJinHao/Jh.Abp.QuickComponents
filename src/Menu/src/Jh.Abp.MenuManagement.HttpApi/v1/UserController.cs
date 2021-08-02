
using Jh.Abp.MenuManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace Jh.Abp.MenuManagement.v1
{
    [ApiController]
    [RemoteService]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class UserController : IdentityUserController
    {
        public IPermissionDefinitionManager PermissionDefinitionManager { get; set; }
        public IDataFilter<ISoftDelete> dataFilter { get; set; }
        protected IProfileAppService ProfileAppService { get; }
        protected IdentityUserManager UserManager { get; }
        protected IIdentityUserRepository UserRepository { get; }

        public UserController(IIdentityUserAppService userAppService,
            IProfileAppService profileAppService,
            IIdentityUserRepository userRepository,
            IdentityUserManager userManager) : base(userAppService)
        {
            ProfileAppService = profileAppService;
            UserRepository = userRepository;
            UserManager = userManager;
        }

		[Authorize(IdentityPermissions.Users.Default)]
        [HttpGet]
        [Route("{id}/selectroles")]
        public virtual async Task<dynamic> GetRolesToSelectAsync(Guid id)
        {
            var datas = await UserRepository.GetRolesAsync(id);
            return new
            {
                items = datas.Select(a => new { name = a.Name, value = a.Id })
            };
        }

        [Authorize(IdentityPermissions.Users.Default)]
        [HttpGet]
        public override async Task<PagedResultDto<IdentityUserDto>> GetListAsync([FromQuery] GetIdentityUsersInput input)
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
		[Authorize(IdentityPermissions.Users.Default)]
        [HttpGet("info")]
        public virtual Task<ProfileDto> GetLoginInfoAsync()
        {
            return ProfileAppService.GetAsync();
        }

        [Authorize(IdentityPermissions.Users.Default)]
        [HttpGet("{id}/Permissions")]
        public virtual IEnumerable<string> GePermissionsAsync()
        {
            var datas = PermissionDefinitionManager.GetPermissions();
            return datas.Select(a => a.Name).ToList();
        }

        //由于每个人都需要改密码所以注销权限
        //[Authorize(IdentityPermissions.Users.Update)]
        [HttpPost]
        [Route("change-password")]
        public virtual Task ChangePasswordAsync(ChangePasswordInput input)
        {
            return ProfileAppService.ChangePasswordAsync(input);
        }


		[Authorize(IdentityPermissions.Users.Update)]
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

		[Authorize(IdentityPermissions.Users.Update)]
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
        /// 根据主键删除多条
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
		[Authorize(IdentityPermissions.Users.Delete)]
        [Route("keys")]
        [HttpDelete]
        public virtual async Task DeleteAsync([FromBody] Guid[] keys)
        {
            foreach (var item in keys)
            {
                await UserAppService.DeleteAsync(item);
            }
        }

    }
}
