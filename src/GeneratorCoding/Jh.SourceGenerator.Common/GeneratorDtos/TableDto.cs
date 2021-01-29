using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.GeneratorDtos
{
    public class TableDto
    {
        /// <summary>
        /// 生成.cs文件的命名空间
        /// </summary>
        public string Namespace { get; set; } = "Jh.Abp.MenuManagement";
        /// <summary>
        /// 主键类型
        /// </summary>
        public string KeyType { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 字段列表
        /// </summary>
        public IEnumerable<FieldDto> Fields { get; set; }
    }
}
