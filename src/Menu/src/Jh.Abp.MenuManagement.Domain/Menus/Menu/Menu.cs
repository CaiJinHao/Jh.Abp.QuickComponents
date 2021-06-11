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

namespace Jh.Abp.MenuManagement
{
    //[GeneratorClass]
    [Description("菜单")]
    public class Menu : FullAuditedAggregateRoot<Guid>
    {
        [RetrieveDto]
        [CreateOrUpdateInputDto]
        [Description("菜单编号")]
        [Required]
        [MaxLength(64)]
        public string Code { get; set; }

        [RetrieveDto]
        [CreateOrUpdateInputDto]
        [Description("菜单名称")]
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [CreateOrUpdateInputDto]
        [Description("菜单图标")]
        [Required]
        [MaxLength(200)]
        public string Icon { get; set; }

        [CreateOrUpdateInputDto]
        [Description("同一级别内排序")]
        [Required]
        public int Sort { get; set; }

        [RetrieveDto]
        [CreateOrUpdateInputDto]
        [Description("上级菜单编号，顶级可为null")]
        [MaxLength(64)]
        public string ParentCode { get; set; }

        [CreateOrUpdateInputDto]
        [Description("导航路径")]
        [MaxLength(500)]
        public string Url { get; set; }

        [CreateOrUpdateInputDto]
        [Description("菜单描述")]
        [MaxLength(500)]
        public string Description { get; set; }

        [ProfileIgnore]
        public virtual IList<MenuAndRoleMap> MenuRoleMaps { get; protected set; }

        [ProfileIgnore]
        public virtual IList<MenuPermissionMap> MenuPermissionMaps { get; protected set; }
        

        public Menu()
        {
            MenuRoleMaps = new List<MenuAndRoleMap>();
            MenuPermissionMaps = new List<MenuPermissionMap>();
        }

        public virtual void AddMenuRoleMap(Guid roleid)
        {
            Check.NotNull(roleid, nameof(roleid));
            MenuRoleMaps.Add(new MenuAndRoleMap(Id, roleid));
        }

        public virtual void AddMenuPermissionMap(string permissionName)
        {
            Check.NotNull(permissionName, nameof(permissionName));
            MenuPermissionMaps.Add(new MenuPermissionMap(Id, permissionName));
        }
    }
}
