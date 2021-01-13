using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jh.Abp.MenuManagement.Menus
{
    public class MenuAndRoleMapUpdateInputDto
    {
        [Required]
        public Guid MenuId { get; set; }

        [Required]
        public Guid RoleId { get; set; }
    }
}
