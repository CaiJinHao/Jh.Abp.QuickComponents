using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.MenuManagement
{
    public class MenusTree
    {
        public string id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string icon { get; set; }
        public string parent_id { get; set; }
        public int sort { get; set; }
    }

    /// <summary>
    /// 菜单树dto
    /// </summary>
    public class MenusTreeDto: MenusTree
    {
        public bool @checked { get; set; }
        public bool disabled { get; set; }
        public string value { get; set; }

        private IEnumerable<MenusTreeDto> _data = new MenusTreeDto[] { };
        /// <summary>
        /// 数组类型
        /// </summary>
        public IEnumerable<MenusTreeDto> data
        {
            get
            {
                return _data;
            }
            set
            {
                if (value != null)
                {
                    _data = value;
                }
            }
        }
    }
}
