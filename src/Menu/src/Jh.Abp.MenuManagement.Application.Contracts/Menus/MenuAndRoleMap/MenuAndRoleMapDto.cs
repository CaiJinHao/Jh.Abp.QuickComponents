using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jh.Abp.MenuManagement
{
    public class MenuAndRoleMapDto : CreationAuditedEntityDto<Guid>
    {
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
