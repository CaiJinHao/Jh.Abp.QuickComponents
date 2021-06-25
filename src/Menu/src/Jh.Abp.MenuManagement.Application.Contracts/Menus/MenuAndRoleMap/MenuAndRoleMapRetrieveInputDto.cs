using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Jh.Abp.MenuManagement
{
    public class MenuAndRoleMapRetrieveInputDto : PagedAndSortedResultRequestDto, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }
        /// <summary>
        /// 菜单外键
        /// </summary>
        public Guid MenuId { get; set; }

        /// <summary>
        /// 角色外键
        /// </summary>
        public Guid RoleId { get; set; }
    }
}
