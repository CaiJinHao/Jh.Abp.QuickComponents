using Microsoft.EntityFrameworkCore;
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
        /// 该查询值对表字段相同都会查询、否则使用自定义字段不能和表字段相投，自己添加查询条件
        /// </summary>
        /// <typeparam name="TWhere">要生成linq条件的类</typeparam>
        /// <typeparam name="TSource">要查询的类</typeparam>
        /// <param name="inputDto">要生成linq条件的参数</param>
        /// <param name="methodStringType">String类型的字段要使用的查询方法</param>
        /// <returns></returns>
        public static Expression<Func<TSource, bool>> ConvetToExpression<TWhere, TSource>(TWhere inputDto,string methodStringType= "Equals")
        {
            Expression<Func<TSource, bool>> expression = e => true;
            if (inputDto == null)
            {
                throw new ArgumentNullException(nameof(inputDto));
            }
            Expression? resultFilters = null;
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
                MethodInfo? method = null;
                Expression proerty = Expression.Property(parameterExpression, item.Name);
                var propertyType = propertyVal.GetType();
                var valueType = ObjectExtensions.GetObjectType(propertyType);
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
                    case Enums.ObjectType.Boolean:
                        {
                            //只要不是null就添加查询条件
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
                            method = propertyType.GetMethod(methodStringType, new Type[] { propertyType });
                        }
                        break;
                    case Enums.ObjectType.DateTime:
                    default:
                        //其他不添加查询条件
                        continue;
                }
                //3.创建常数表达式
                ConstantExpression constantExpression = Expression.Constant(propertyVal, propertyType);
                //4.创建方法调用表达式
                var currentFilter = Expression.Call(proerty, method, new Expression[] { constantExpression });
                //5.创建Lambda表达式
                var right = Expression.Lambda<Func<TSource, bool>>(resultFilters, new ParameterExpression[] { parameterExpression });
                expression = CombineExpressions(expression, right);
            }
            return expression;
        }

        public static Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expression1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expression2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        class ReplaceExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                {
                    return _newValue;
                }

                return base.Visit(node);
            }
        }

        /// <summary>
        /// 将IQueryable转为Expression<Func<TSource, bool>>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="inputQuery">不支持分页和排序</param>
        /// <returns></returns>
        public static Expression<Func<TSource, bool>> ToExpression<TSource>(this IQueryable<TSource> inputQuery)
        {
            return inputQuery.Expression != null && inputQuery.Expression is MethodCallExpression methodCallExpression ? GetMethodCallExpression<TSource>(methodCallExpression) : f => true;
        }

        /// <summary>
        /// 获取查询表达式
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="methodCallExpression"></param>
        /// <returns></returns>
        private static Expression<Func<TSource, bool>> GetMethodCallExpression<TSource>(MethodCallExpression methodCallExpression)
        {
            if (methodCallExpression.Arguments.Any(a => a.NodeType == ExpressionType.Extension))
            {
                var unary = methodCallExpression.Arguments.LastOrDefault(a => a.NodeType == ExpressionType.Quote);
                var exp = unary != null && unary is UnaryExpression unaryExpression ? unaryExpression.Operand : null;
                return exp != null && exp is Expression<Func<TSource, bool>> _exp ? _exp : f => true;
            }
            var _methodExpression = methodCallExpression.Arguments.FirstOrDefault(a => a.NodeType == ExpressionType.Call);
            return _methodExpression != null && _methodExpression is MethodCallExpression _mexp ? GetMethodCallExpression<TSource>(_mexp) : f => true;
        }
    }
}
