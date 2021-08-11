using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.Common
{
    /// <summary>
    /// 导航树
    /// </summary>
    public class NavTreeDto: TreeDto
    {
        private IEnumerable<NavTreeDto> _children = new NavTreeDto[] { };
        /// <summary>
        /// 数组类型
        /// </summary>
        public IEnumerable<NavTreeDto> children
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
