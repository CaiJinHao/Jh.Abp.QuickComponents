using Jh.Abp.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.Common.Objects
{
    public class ObjectExtensions
    {
        /// <summary>
        /// 获取类型枚举
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ObjectType GetObjectType(Type type)
        {
            var value = ObjectType.None;
            if (type.IsEnum)
            {
                return ObjectType.Enum;
            }
            Enum.TryParse(type.Name, out value);
            return value;
        }
    }
}
