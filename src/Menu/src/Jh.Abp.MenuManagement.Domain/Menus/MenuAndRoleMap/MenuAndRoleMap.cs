using Jh.SourceGenerator.Common.GeneratorAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jh.Abp.MenuManagement.Menus
{
    //[GeneratorClass]
    [Description("菜单和角色映射表")]
    public class MenuAndRoleMap : CreationAuditedEntity<Guid>
    {
        [RetrieveDto]
        [CreateOrUpdateInputDto]
        [Description("菜单外键")]
        [Required]
        public Guid MenuId { get; set; }

        //[NotMapped]
        [RetrieveDto]
        [CreateOrUpdateInputDto]
        [Description("角色外键")]
        [Required]
        public Guid RoleId { get; set; }

        [ProfileIgnore]
        public virtual Menu Menu { get; set; }

        public MenuAndRoleMap() { }

        public MenuAndRoleMap(Guid menuid, Guid roleid)
        {
            this.MenuId = menuid;
            this.RoleId = roleid;
        }
    }
}
