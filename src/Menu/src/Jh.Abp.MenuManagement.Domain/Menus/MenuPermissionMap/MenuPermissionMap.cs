using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Jh.SourceGenerator.Common.GeneratorAttributes;
using Volo.Abp;
using JetBrains.Annotations;
using System.Linq;

namespace Jh.Abp.MenuManagement.Menus
{
    [GeneratorClass]
    [Description("菜单和权限映射")]
    public class MenuPermissionMap : CreationAuditedEntity<Guid>
    {
        [RetrieveDto]
        [CreateOrUpdateInputDto]
        [Description("菜单外键")]
        [Required]
        public Guid MenuId { get; set; }

        [RetrieveDto]
        [CreateOrUpdateInputDto]
        [Description("权限标识")]
        [Required]
        public Guid PermissionId { get; set; }

        [ProfileIgnore]
        public virtual Menu Menu { get; set; }

        public MenuPermissionMap() { }
        public MenuPermissionMap(Guid menuid, Guid permissionId)
        {
            this.MenuId = menuid;
            this.PermissionId = permissionId;
        }
    }
}
