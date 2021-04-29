using Jh.Abp.Application.Contracts.Dtos;
using Jh.Abp.Application.Contracts.Extensions;
using Volo.Abp.Application.Dtos;

namespace Jh.Abp.MenuManagement.Menus
{
    /// <summary>
    /// 只存放需要查询的字段
    /// </summary>
    public class MenuRetrieveInputDto : PagedAndSortedResultRequestDto, IMethodDto<Menu>, IRetrieveDelete
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

        public int? Deleted { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public MethodDto<Menu> MethodInput { get; set; }
    }
}
