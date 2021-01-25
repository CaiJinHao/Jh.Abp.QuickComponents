using Jh.Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.MenuManagement.Menus
{
    public class MenuUpdateInputDto: IUpdateDeleted<Menu>
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 同一级别内排序
        /// </summary>
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

        public bool IsDeleted { get; set; }
        public Action<Menu> OtherUpdate { get; set; }
    }
}
