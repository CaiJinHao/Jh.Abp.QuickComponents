using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Jh.Abp.Common.Linq
{
    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }
        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }

    public static class LinqExpression
    {
        /// <summary>
        /// 转换为表达式
        /// </summary>
        /// <typeparam name="TWhere">要生成linq条件的类</typeparam>
        /// <typeparam name="TSource">要查询的类</typeparam>
        /// <param name="inputDto">要生成linq条件的参数</param>
        /// <param name="methodStringType">String类型的字段要使用的查询方法</param>
        /// <returns></returns>
        public static Expression<Func<TSource, bool>> ConvetToExpression<TWhere, TSource>(TWhere inputDto,string methodStringType= "Equals")
        {
            Expression resultFilters = null;
            //1.创建参数表达式
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "a");
            var sourcePropertyInfosNames = typeof(TSource).GetProperties().Select(a => a.Name);
            var inputPropertyInfos = inputDto.GetType().GetProperties();
            foreach (var item in inputPropertyInfos)
            {
                if (!sourcePropertyInfosNames.Contains(item.Name))
                {
                    continue;
                }
                var propertyVal = item.GetValue(inputDto, null);
                if (propertyVal == null)
                {
                    continue;
                }
                //a(1)=>a.Name(2).Equals(4)("val")(3);(5)
                //2.创建属性表达式
                Expression proerty = Expression.Property(parameterExpression, item.Name);
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
                            proerty = Expression.Convert(proerty, propertyType);
                        }
                        break;
                    case Enums.ObjectType.Int16:
                    case Enums.ObjectType.Int32:
                    case Enums.ObjectType.Int64:
                    case Enums.ObjectType.Float:
                    case Enums.ObjectType.Double:
                    case Enums.ObjectType.Decimal:
                        {
                            var t = propertyVal.ToString();
                            if (t.Equals("0"))
                            {
                                continue;
                            }
                            method = propertyType.GetMethod("Equals", new Type[] { propertyType });
                            proerty = Expression.Convert(proerty, propertyType);
                        }
                        break;
                    case Enums.ObjectType.Guid:
                        {
                            if (propertyVal.Equals(Guid.Empty))
                            {
                                continue;
                            }
                            method = propertyType.GetMethod("Equals", new Type[] { propertyType });
                        }
                        break;
                    case Enums.ObjectType.String:
                        {
                            if (propertyVal.Equals(string.Empty))
                            {
                                continue;
                            }
                            //method = typeof(string).GetMethod(methodStringType, new Type[] { typeof(string) });
                            method = propertyType.GetMethod(methodStringType, new Type[] { propertyType });
                        }
                        break;
                    case Enums.ObjectType.Bool:
                    case Enums.ObjectType.DateTime:
                    default:
                        //其他不添加查询条件
                        continue;
                }
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


        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)  
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first  
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression   
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }

        /*
         use:
        Expression<Func<WMS_User, bool>> Conditions = PredicateExtensions.True<WMS_User>();
if (ids != null)
   Conditions = Conditions.And(u => u.UserOrgIds.Any(o => ids.Contains(o.OrgId)));
if (Cds != null && Cds.Length > 0)
   Conditions = Conditions.And(u => u.UserName.Contains(Cds)|| u.NickName.Contains(Cds) );
         */
    }
}
