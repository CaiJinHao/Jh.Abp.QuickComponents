using Jh.Abp.MenuManagement.Menus;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Jh.Abp.MenuManagement.v1
{
    [RemoteService]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class UserController : ProfileController
    {
        protected IdentityUserManager UserManager { get; }
        protected IIdentityUserRepository UserRepository { get; }

        public UserController(
            IProfileAppService profileAppService,
            IIdentityUserRepository userRepository,
            IdentityUserManager userManager) : base(profileAppService)
        {
            UserRepository = userRepository;
            UserManager = userManager;
        }

        [HttpGet]
        [Route("{id}/roles")]
        public virtual async Task<dynamic> GetRolesAsync(Guid id)
        {
            var datas= await UserRepository.GetRolesAsync(id);
            return new
            {
                items = datas.Select(a => new { name = a.Name, value = a.Id })
            };
        }

        //更新启用锁定、更新启用用户
        [HttpPatch]
        [Route("{id}/lockoutEnabled")]
        public virtual async Task UpdateLockoutEnabledAsync(Guid id, [FromBody]bool lockoutEnabled)
        {
            var user= await UserManager.GetByIdAsync(id);
            (await UserManager.SetLockoutEnabledAsync(user, lockoutEnabled)).CheckErrors();
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [HttpPatch]
        [Route("{id}/Deleted")]
        public virtual async Task UpdateDeletedAsync(Guid id, [FromBody]bool isDeleted)
        {
            var user = await UserManager.GetByIdAsync(id);
            user.IsDeleted = isDeleted;
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
