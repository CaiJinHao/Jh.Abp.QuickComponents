using Jh.Abp.Common.Objects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Jh.Abp.Common.Linq
{
    public static class LinqExpression
    {
        /// <summary>
        /// 转换为表达式
        /// </summary>
        /// <typeparam name="TWhere">要生成linq条件的类</typeparam>
        /// <typeparam name="TSource">要查询的类</typeparam>
        /// <param name="inputDto">要生成linq条件的参数</param>
        /// <returns></returns>
        public static Expression<Func<TSource, bool>> ConvetToExpression<TWhere, TSource>(TWhere inputDto)
        {
            Expression resultFilters = null;
            //1.创建参数表达式
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "a");
            var propertyInfos = inputDto.GetType().GetProperties();
            foreach (var item in propertyInfos)
            {
                var propertyVal = item.GetValue(inputDto, null);
                if (propertyVal == null)
                {
                    continue;
                }
                var propertyType = propertyVal.GetType();
                var valueType = ObjectExtensions.GetObjectType(propertyType);
                MethodInfo method = null;
                switch (valueType)
                {
                    case Enums.ObjectType.Enum:
                        {
                            propertyType = typeof(Int32);
                            propertyVal = (int)propertyVal;
                            if (propertyVal.Equals(0))
                            {
                                continue;
                            }
                            method = propertyType.GetMethod("Equals", new Type[] { propertyType });
                        }
                        break;
                    case Enums.ObjectType.Int16:
                    case Enums.ObjectType.Int32:
                    case Enums.ObjectType.Int64:
                    case Enums.ObjectType.Float:
                    case Enums.ObjectType.Double:
                    case Enums.ObjectType.Decimal:
                        {
                            if (propertyVal.Equals(0))
                            {
                                continue;
                            }
                            method = propertyType.GetMethod("Equals", new Type[] { propertyType });
                        }
                        break;
                    case Enums.ObjectType.Guid:
                        {
                            if (propertyVal.Equals(Guid.Empty))
                            {
                                continue;
                            }
                            method = propertyType.GetMethod("Contains", new Type[] { propertyType });
                        }
                        break;
                    case Enums.ObjectType.String:
                        {
                            if (propertyVal.Equals(string.Empty))
                            {
                                continue;
                            }
                            method = propertyType.GetMethod("Contains", new Type[] { propertyType });
                        }
                        break;
                    case Enums.ObjectType.Bool:
                    case Enums.ObjectType.DateTime:
                    default:
                        continue;
                }
                //a(1)=>a.Name(2).Equals(4)("val")(3);(5)
                //2.创建属性表达式
                Expression proerty = Expression.Property(parameterExpression, item.Name);
                //3.创建常数表达式
                ConstantExpression constantExpression = Expression.Constant(propertyVal, propertyType);
                //4.创建方法调用表达式
                var currentFilter = Expression.Call(proerty, method, new Expression[] { constantExpression });
                if (resultFilters == null)
                {
                    resultFilters = currentFilter;
                }
                else
                {
                    resultFilters = Expression.And(resultFilters, currentFilter);
                }
            }
            if (resultFilters == null)
            {
                resultFilters = Expression.Equal(Expression.Constant(true), Expression.Constant(true));
            }
            //5.创建Lambda表达式
            var lambda = Expression.Lambda<Func<TSource, bool>>(resultFilters, new ParameterExpression[] { parameterExpression });
            return lambda;
        }
    }
}
