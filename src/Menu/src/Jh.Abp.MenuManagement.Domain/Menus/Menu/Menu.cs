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
    [Description("菜单")]
    [Table(MenuManagementDbProperties.BaseDbTablePrefix + "Menu")]
    public class Menu : FullAuditedAggregateRoot<Guid>
    {
        [RetrieveDto]
        [CreateOrUpdateInputDto]
        [Description("菜单编号")]
        [Required]
        public string Code { get; set; }

        [RetrieveDto]
        [CreateOrUpdateInputDto]
        [Description("菜单名称")]
        [Required]
        public string Name { get; set; }

        [CreateOrUpdateInputDto]
        [Description("菜单图标")]
        [Required]
        public string Icon { get; set; }

        [CreateOrUpdateInputDto]
        [Description("同一级别内排序")]
        [Required]
        public int Sort { get; set; }

        [RetrieveDto]
        [CreateOrUpdateInputDto]
        [Description("上级菜单编号，顶级可为null")]
        public string ParentCode { get; set; }

        [CreateOrUpdateInputDto]
        [Description("导航路径")]
        public string Url { get; set; }

        [CreateOrUpdateInputDto]
        [Description("菜单描述")]
        public string Description { get; set; }

        public virtual IList<MenuAndRoleMap> MenuRoleMaps { get; protected set; }

        public Menu()
        {
            MenuRoleMaps = new List<MenuAndRoleMap>();
        }

        public virtual void AddMenuRoleMap([NotNull]Guid roleid)
        {
            Check.NotNull(roleid, nameof(roleid));
            MenuRoleMaps.Add(new MenuAndRoleMap(Id, roleid));
        }

        public virtual void RemoveMenuRoleMap()
        {
            MenuRoleMaps.RemoveAll(r => r.MenuId == Id);
        }
    }
}
