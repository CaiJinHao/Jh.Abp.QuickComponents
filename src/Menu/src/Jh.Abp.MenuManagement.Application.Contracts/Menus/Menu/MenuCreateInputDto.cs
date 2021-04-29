using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace Jh.Abp.MenuManagement.Menus
{
    /// <summary>
    /// 只存放需要传值的
    /// </summary>
    public class MenuCreateInputDto: ExtensibleObject
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [Required]
        public string Icon { get; set; }

        /// <summary>
        /// 同一级别内排序
        /// </summary>
        [Required]
        public int Sort { get; set; }

        /// <summary>
        /// 上级菜单编号，顶级可为null
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 导航路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 菜单描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 角色ID列表
        /// </summary>
        public Guid[] RoleIds { get; set; }
    }
}
