using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Jh.Abp.MenuManagement.Menus
{
    /// <summary>
    /// 只存放需要查询的字段
    /// </summary>
    public class MenuRetrieveInputDto : PagedAndSortedResultRequestDto
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
        /// 上级菜单编号，顶级可为null
        /// </summary>
        public string ParentCode { get; set; }
    }
}
