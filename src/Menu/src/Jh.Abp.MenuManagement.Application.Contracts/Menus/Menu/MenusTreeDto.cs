using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.MenuManagement.Menus
{
    /// <summary>
    /// 菜单树dto
    /// </summary>
    public class MenusTreeDto
    {
        public string id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string icon { get; set; }
        private IEnumerable<MenusTreeDto> _children = new MenusTreeDto[] { };

        /// <summary>
        /// 数组类型
        /// </summary>
        public IEnumerable<MenusTreeDto> children
        {
            get
            {
                return _children;
            }
            set
            {
                if (value != null)
                {
                    _children = value;
                }
            }
        }
        public string parent_id { get; set; }
        public int sort { get; set; }

        public bool @checked { get; set; }
        public UseType is_module { get; set; }
        public UseType is_leaf { get; set; }
    }
}
