using Jh.Abp.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.Common
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 获取类型枚举
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ObjectType GetObjectType(this Type type)
        {
            ObjectType value;
            if (type.IsEnum)
            {
                return ObjectType.Enum;
            }
            Enum.TryParse(type.Name, out value);
            return value;
        }

        public static IEnumerable<TSource> ToNullList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                return new List<TSource>();
            }
            return source;
        }
    }
}
