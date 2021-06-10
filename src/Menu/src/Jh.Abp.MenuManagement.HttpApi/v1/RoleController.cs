using Jh.Abp.MenuManagement.Menus;
using Jh.Abp.MenuManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
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
    //Using Identity requires references to Identity dependent components
    [ApiController]
    [RemoteService]
    [Route("api/v{apiVersion:apiVersion}/[controller]")]
    public class RoleController : Volo.Abp.Identity.IdentityRoleController
    {
        protected IIdentityRoleRepository RoleRepository { get; }
        public RoleController(
            IIdentityRoleAppService roleAppService,
            IIdentityRoleRepository roleRepository) : base(roleAppService)
        {
            RoleRepository = roleRepository;
        }

		[Authorize(MenuManagementPermissions.Roles.Default)]
        [HttpGet]
        [Route("tree")]
        public virtual async Task<dynamic> GetTreeAsync(string name)
        {
            var datas = await RoleRepository.GetListAsync(filter: name);
            return new
            {
                items = datas.Select(a => new { title = a.Name, id = a.Id, data = a, spread = true })
            };
        }

		[Authorize(MenuManagementPermissions.Roles.Default)]
        [HttpGet]
        [Route("select")]
        public virtual async Task<dynamic> GetSelectAsync(string name)
        {
            var datas = await RoleRepository.GetListAsync(filter: name);
            return new
            {
                items = datas.Select(a => new { name = a.Name, value = a.Id })
            };
        }
    }
}
