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
        public string icon { get; set; }
        public string parent_id { get; set; }
        public int sort { get; set; }
    }
}
