using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.MenuManagement
{
    /// <summary>
    /// 导航菜单
    /// </summary>
    public class MenusNavDto : MenusTree
    {

        private IEnumerable<MenusNavDto> _children = new MenusNavDto[] { };
        /// <summary>
        /// 数组类型
        /// </summary>
        public IEnumerable<MenusNavDto> children
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
    }
}
