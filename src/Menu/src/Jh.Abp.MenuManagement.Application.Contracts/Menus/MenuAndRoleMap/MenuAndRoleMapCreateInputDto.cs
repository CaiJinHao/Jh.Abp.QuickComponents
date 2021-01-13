using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jh.Abp.MenuManagement.Menus
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
    }
}
