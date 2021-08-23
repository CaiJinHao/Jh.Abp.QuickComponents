using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.Common
{
    public  class TreeDto
    {
        public string id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string href { get { return this.url; } }
        public string icon { get; set; }
        public string parent_id { get; set; }
        public bool spread { get; set; } = true;
        public bool @checked { get; set; }
        public bool disabled { get; set; }
        public object obj { get; set; }
        public string sort { get; set; }

        private IEnumerable<TreeDto> _children = new TreeDto[] { };
        /// <summary>
        /// 数组类型 子级
        /// </summary>
        public IEnumerable<TreeDto> children
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

        /// <summary>
        /// 数组类型 多选
        /// </summary>
        public IEnumerable<TreeDto> data
        {
            get
            {
                return _children;
            }
        }

        public string value { get; set; }

        public TreeDto Copy()
        {
            return (TreeDto)this.MemberwiseClone();
        }
    }
}
