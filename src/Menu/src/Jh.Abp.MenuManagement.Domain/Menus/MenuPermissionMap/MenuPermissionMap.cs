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
using Volo.Abp.MultiTenancy;

namespace Jh.Abp.MenuManagement
{
    [GeneratorClass]
    [Description("菜单和权限映射")]
    public class MenuPermissionMap : CreationAuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [RetrieveDto]
        [CreateOrUpdateInputDto]
        [Description("菜单外键")]
        [Required]
        public Guid MenuId { get; set; }

        [RetrieveDto]
        [CreateOrUpdateInputDto]
        [Description("权限标识")]
        [Required]
        public string PermissionName { get; set; }

        [ProfileIgnore]
        public virtual Menu Menu { get; set; }

        public MenuPermissionMap() { }
        public MenuPermissionMap(Guid menuid, string permissionName)
        {
            this.MenuId = menuid;
            this.PermissionName = permissionName;
        }
    }
}
