using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.Common
{
   public  class CheckTreeDto: TreeDto
    {
        public bool @checked { get; set; }
        public bool disabled { get; set; }
        public string value { get; set; }

        private IEnumerable<CheckTreeDto> _data = new CheckTreeDto[] { };
        /// <summary>
        /// 数组类型
        /// </summary>
        public IEnumerable<CheckTreeDto> data
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

        public CheckTreeDto Copy()
        {
            return (CheckTreeDto)this.MemberwiseClone();
        }
    }
}
