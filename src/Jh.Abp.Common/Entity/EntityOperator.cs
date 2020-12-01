using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var toFields = entity.GetType().GetProperties();
            var formFields = target.GetType().GetProperties();
            foreach (var item in formFields)
            {
                var toField = toFields.Where(a => a.Name == item.Name).FirstOrDefault();
                if (toField != null)
                {
                    var _value = item.GetValue(target);
                    if (_value != null)
                    {
                        var fieldType = _value.GetType();
                        if (fieldType.IsEnum)
                        {
                            var _v = (int)_value;
                            if (_v > 0)
                            {
                                toField.SetValue(entity, _value);
                            }
                        }
                        else
                        {
                            switch (fieldType.Name)
                            {
                                case "String":
                                    {
                                        if (!string.IsNullOrEmpty((string)_value))
                                        {
                                            toField.SetValue(entity, _value);
                                        }
                                    }
                                    break;
                                case "DateTime":
                                    {
                                        var _v = (DateTime)_value;
                                        if (_v > new DateTime(1900, 1, 1))
                                        {
                                            toField.SetValue(entity, _value);
                                        }
                                    }
                                    break;
                                case "Int16":
                                    {
                                        var _v = (Int16)_value;
                                        if (_v > 0)
                                        {
                                            toField.SetValue(entity, _value);
                                        }
                                    }
                                    break;
                                case "Int32":
                                    {
                                        var _v = (Int32)_value;
                                        if (_v > 0)
                                        {
                                            toField.SetValue(entity, _value);
                                        }
                                    }
                                    break;
                                case "Int64":
                                    {
                                        var _v = (Int64)_value;
                                        if (_v > 0)
                                        {
                                            toField.SetValue(entity, _value);
                                        }
                                    }
                                    break;
                                case "Decimal":
                                    {
                                        var _v = (decimal)_value;
                                        if (_v > 0)
                                        {
                                            toField.SetValue(entity, _value);
                                        }
                                    }
                                    break;
                                case "Boolean":
                                    {
                                        toField.SetValue(entity, _value);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
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
    }
}
