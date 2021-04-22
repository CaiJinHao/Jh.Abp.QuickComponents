using System;
using System.Linq;

namespace Jh.Abp.Common.Entity
{
    public static class EntityOperator
    {
        /// <summary>
        /// 更新到实体（有值时更新）
        /// </summary>
        /// <typeparam name="TTarget">值来源对象类</typeparam>
        /// <typeparam name="TEntity">更新目标类</typeparam>
        /// <param name="target">值来源对象</param>
        /// <param name="entity">更新目标</param>
        public static void UpdatePortionToEntity<TTarget, TEntity>(TTarget target, TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            var toFields = entity.GetType().GetProperties();
            var formFields = target.GetType().GetProperties();
            foreach (var item in formFields)
            {
                var toField = toFields.Where(a => a.Name == item.Name).FirstOrDefault();
                if (toField != null)
                {
                    var propertyVal = item.GetValue(target);
                    if (!IsContinue(propertyVal))
                    {
                        continue;
                    }
                    toField.SetValue(entity, propertyVal);
                }
            }
        }

        /// <summary>
        /// 更新到实体（全部更新）
        /// </summary>
        /// <typeparam name="TTarget">值来源对象类</typeparam>
        /// <typeparam name="TEntity">更新目标类</typeparam>
        /// <param name="target">值来源对象</param>
        /// <param name="entity">更新目标</param>
        public static void UpdateToEntity<TTarget, TEntity>(TTarget target, TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            var toFields = entity.GetType().GetProperties();
            var formFields = target.GetType().GetProperties();
            foreach (var item in formFields)
            {
                var toField = toFields.Where(a => a.Name == item.Name).FirstOrDefault();
                if (toField != null)
                {
                    var _value = item.GetValue(target);
                    toField.SetValue(entity, _value);
                }
            }
        }

        public static bool IsContinue(object propertyVal)
        {
            if (propertyVal == null)
            {
                return false;
            }
            var propertyType = propertyVal.GetType();
            var valueType = ObjectExtensions.GetObjectType(propertyType);
            switch (valueType)
            {
                case Enums.ObjectType.Enum:
                    {
                        if (((int)propertyVal).Equals(0))
                        {
                            return false;
                        }
                    }
                    break;
                case Enums.ObjectType.Int16:
                case Enums.ObjectType.Int32:
                case Enums.ObjectType.Int64:
                case Enums.ObjectType.Float:
                case Enums.ObjectType.Double:
                case Enums.ObjectType.Decimal:
                    {
                        //不等于空就有效
                        //return true;
                        var t = propertyVal.ToString();
                        if (t.Equals("0"))
                        {
                            return false;
                        }
                    }break;
                case Enums.ObjectType.Guid:
                    {
                        if (propertyVal.Equals(Guid.Empty))
                        {
                            return false;
                        }
                    }
                    break;
                case Enums.ObjectType.String:
                    {
                        if (propertyVal.Equals(string.Empty))
                        {
                            return false;
                        }
                    }
                    break;
                case Enums.ObjectType.DateTime:
                    {
                        var _v = (DateTime)propertyVal;
                        if (_v <= new DateTime(1900, 1, 1))
                        {
                            return false;
                        }
                    }
                    break;
                case Enums.ObjectType.Bool:
                case Enums.ObjectType.None:
                default:
                    return false;
            }
            return true;
        }
    }
}
