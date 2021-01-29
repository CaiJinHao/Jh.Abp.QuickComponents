using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.GeneratorDtos
{
    public class TableDto
    {
        public string DbContext { get; }
        /// <summary>
        /// 生成.cs文件的命名空间
        /// </summary>
        public string Namespace { get; }
        public string ControllerBase { get; }
        public TableDto(string dbContext,string namespa,string controllerBase)
        {
            DbContext = dbContext;
            Namespace = namespa;
            ControllerBase = controllerBase;
        }
        /// <summary>
        /// 主键类型
        /// </summary>
        public string KeyType { get; set; } = "Guid";
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 要继承的类
        /// </summary>
        public string InheritClass { get; set; } = "FullAuditedEntityDto";
        /// <summary>
        /// 字段列表
        /// </summary>
        public IEnumerable<FieldDto> Fields { get; set; }

        //ProfileDto
        //暂时只有一个Id
        //public IEnumerable<FieldDto> CreateOrUpdateInputDtoIgnoreFields { get; set; }
        /// <summary>
        /// 要忽略的字段
        /// </summary>
        public IEnumerable<FieldDto> RetrieveInputDtoIgnoreFields { get; set; }
    }
}
