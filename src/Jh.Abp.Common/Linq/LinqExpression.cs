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
                var propertyName = item.Name;
                var propertyVal = item.GetValue(inputDto, null);
                if (propertyVal == null)
                {
                    continue;
                }
                var propertyType = propertyVal.GetType();
                MethodInfo method = null;
                if (propertyType.IsEnum)
                {
                    if ((int)propertyVal == 0)
                    {
                        continue;
                    }
                    method = propertyType.GetMethod("Equals", new Type[] { propertyType });
                }
                else
                {
                    switch (propertyType.Name)
                    {
                        case "Int16":
                            {
                                if ((Int16)propertyVal == 0)
                                {
                                    continue;
                                }
                                method = propertyType.GetMethod("Equals", new Type[] { propertyType });
                            }
                            break;
                        case "Int32":
                            {
                                if ((Int32)propertyVal == 0)
                                {
                                    continue;
                                }
                                method = propertyType.GetMethod("Equals", new Type[] { propertyType });
                            }
                            break;
                        case "Int64":
                            {
                                if ((Int64)propertyVal == 0)
                                {
                                    continue;
                                }
                                method = propertyType.GetMethod("Equals", new Type[] { propertyType });
                            }
                            break;
                        case "Decimal":
                            {
                                if ((Decimal)propertyVal == 0)
                                {
                                    continue;
                                }
                                method = propertyType.GetMethod("Equals", new Type[] { propertyType });
                            }
                            break;
                        case "String":
                            {
                                if (string.IsNullOrEmpty((string)propertyVal))
                                {
                                    continue;
                                }
                                method = propertyType.GetMethod("Contains", new Type[] { propertyType });
                            }
                            break;
                        case "Guid":
                            {
                                if ((Guid)propertyVal == Guid.Empty)
                                {
                                    continue;
                                }
                                method = propertyType.GetMethod("Contains", new Type[] { propertyType });
                            }
                            break;
                        default:
                            {
                                continue;
                            }
                    }
                }
                //a(1)=>a.Name(2).Equals(4)("val")(3);(5)
                //2.创建属性表达式
                Expression proerty = Expression.Property(parameterExpression, propertyName);
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
            //5.创建Lambda表达式
            var lambda = Expression.Lambda<Func<TSource, bool>>(resultFilters, new ParameterExpression[] { parameterExpression });
            return lambda;
        }
    }
}
