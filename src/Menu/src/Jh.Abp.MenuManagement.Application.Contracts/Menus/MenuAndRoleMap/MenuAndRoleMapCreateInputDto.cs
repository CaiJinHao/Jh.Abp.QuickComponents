using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Authorization.Permissions;

namespace Jh.Abp.MenuManagement
{
    public class MenuAndRoleMapCreateInputDto
    {
        /// <summary>
        /// 菜单外键
        /// </summary>
        [Required]
        public Guid[] MenuIds { get; set; }

        /// <summary>
        /// 角色外键
        /// </summary>
        [Required]
        public Guid[] RoleIds { get; set; }

        /// <summary>
        /// 权限名称列表
        /// </summary>
        public string[] PermissionNames { get; set; }
        public string ProviderName { get; set; } = RolePermissionValueProvider.ProviderName;
        public string ProviderKey { get; set; }
    }
}
