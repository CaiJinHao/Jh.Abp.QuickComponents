using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Jh.Abp.MenuManagement.Menus
{
    [Description("菜单")]
    [Table(MenuManagementDbProperties.BaseDbTablePrefix + "Menu")]
    public class Menu : AuditedAggregateRoot<Guid>
    {
        [Description("菜单编号")]
        [Required]
        public string Code { get; set; }

        [Description("菜单名称")]
        [Required]
        public string Name { get; set; }

        [Description("菜单图标")]
        [Required]
        public string Icon { get; set; }

        [Description("同一级别内排序")]
        [Required]
        public int Sort { get; set; }

        [Description("上级菜单编号，顶级可为null")]
        public string ParentCode { get; set; }

        [Description("导航路径")]
        public string Url { get; set; }

        [Description("菜单描述")]
        public string Description { get; set; }

        [Description("菜单可用类型")]
        [Required]
        public UseType Use { get; set; }
        public Menu() {
            this.Use = UseType.Yes;
        }
    }
}
