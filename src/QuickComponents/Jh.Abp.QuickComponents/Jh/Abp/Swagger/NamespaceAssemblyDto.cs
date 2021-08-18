using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jh.Abp.QuickComponents
{
    public  class NamespaceAssemblyDto
    {
        /// <summary>
        /// 对应程序集得命名空间
        /// </summary>
        public string BaseNamespace { get; set; }
        /// <summary>
        /// xml注释程序集
        /// </summary>
        public Assembly AssemblyXmlComments { get; set; }
    }
}
