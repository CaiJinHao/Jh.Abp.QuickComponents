using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Jh.Abp.MenuManagement.Menus
{
    public class MenuDto: ExtensibleFullAuditedEntityDto<Guid>, IHasConcurrencyStamp
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
        /// <summary>
        /// 并发标识
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}
