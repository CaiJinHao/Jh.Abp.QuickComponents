using Jh.Abp.Common;
using Jh.Abp.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.GeneratorDtos
{
    public class FieldDto
    {
        public bool IsRequired { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// domain 必须带Description特性
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 字段的类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 是否为可空类型 ?
        /// </summary>
        public bool IsNullable { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public ObjectType FieldType { get; set; }

        /// <summary>
        /// 字段默认是否是可空
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldDefaultNullable"></param>
        public virtual bool GetIsNullable()
        {
            if (IsNullable)
            {
                return true;
            }
            switch (FieldType)
            {
                case ObjectType.Int16:
                case ObjectType.Int32:
                case ObjectType.Int64:
                case ObjectType.Float:
                case ObjectType.Double:
                case ObjectType.Decimal:
                case ObjectType.DateTime:
                case ObjectType.Boolean:
                case ObjectType.Guid:
                case ObjectType.Enum:
                    {
                        return true;
                    }
                case ObjectType.String:
                case ObjectType.None:
                default:
                    {
                        return false;
                    }
            }
        }
    }
}
