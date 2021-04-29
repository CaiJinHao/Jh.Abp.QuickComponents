using Jh.Abp.Application.Contracts.Dtos;
using Jh.Abp.Application.Contracts.Extensions;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ObjectExtending;

namespace Jh.Abp.MenuManagement.Menus
{
    public class MenuUpdateInputDto : ExtensibleObject, IHasConcurrencyStamp, IMethodDto<Menu>
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

        [Newtonsoft.Json.JsonIgnore]
        public MethodDto<Menu> MethodInput { get; set; }
        /// <summary>
        /// 并发检测字段 必须和数据中数据的值一样才会允许更新
        /// </summary>
        public string ConcurrencyStamp { get; set; }
    }
}
