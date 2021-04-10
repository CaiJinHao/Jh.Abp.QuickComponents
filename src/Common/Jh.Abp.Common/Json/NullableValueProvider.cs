using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;
using Newtonsoft.Json.Utilities;
using System.Globalization;
using Newtonsoft.Json;

namespace Jh.Abp.Common.Json
{
    public class NullableValueProvider : IValueProvider
    {
        private readonly MemberInfo _memberInfo;

        private Func<object, object?>? _getter;

        private Action<object, object?>? _setter;

       
        public NullableValueProvider(MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException(nameof(memberInfo));
            }
            _memberInfo = memberInfo;
        }

        public void SetValue(object target, object? value)
        {
            try
            {
                if (_setter == null)
                {
                    _setter = CreateSet<object>((PropertyInfo)_memberInfo);
                }
                _setter!(target, value);
            }
            catch (Exception innerException)
            {
                throw new JsonSerializationException(string.Format("Error setting value to '{0}' on '{1}'.", _memberInfo.Name, target.GetType()), innerException);
            }
        }

        public object? GetValue(object target)
        {
            try
            {
                if (_getter == null)
                {
                    _getter = CreateGet<object>(_memberInfo);
                }
                return GetTypeValue((PropertyInfo)_memberInfo, _getter!(target));
            }
            catch (Exception innerException)
            {
                throw new JsonSerializationException(string.Format("Error getting value from '{0}' on '{1}'.", _memberInfo.Name, target.GetType()), innerException);
            }
        }

        public Func<T, object?> CreateGet<T>(MemberInfo memberInfo)
        {
            if (memberInfo==null)
            {
                throw new ArgumentNullException(nameof(memberInfo));
            }
            PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
            if (propertyInfo != null)
            {
                if (propertyInfo.PropertyType.IsByRef)
                {
                    throw new InvalidOperationException(string.Format("Could not create getter for {0}. ByRef return values are not supported.", propertyInfo.Name));
                }
                return CreateGet<T>(propertyInfo);
            }
            FieldInfo fieldInfo = (FieldInfo)memberInfo;
            if (fieldInfo != null)
            {
                return CreateGet<T>(fieldInfo);
            }
            throw new Exception(string.Format("Could not create getter for {0}.", memberInfo.Name));
        }

        public  Func<T, object?> CreateGet<T>(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }
            PropertyInfo propertyInfo2 = propertyInfo;
            return (T o) => propertyInfo2.GetValue(o, null);
        }

        public  Func<T, object?> CreateGet<T>(FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
            {
                throw new ArgumentNullException(nameof(fieldInfo));
            }
            FieldInfo fieldInfo2 = fieldInfo;
            return (T o) => fieldInfo2.GetValue(o);
        }

        public  Action<T, object?> CreateSet<T>(FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
            {
                throw new ArgumentNullException(nameof(fieldInfo));
            }
            FieldInfo fieldInfo2 = fieldInfo;
            return delegate (T o, object? v)
            {
                fieldInfo2.SetValue(o, v);
            };
        }

        public  Action<T, object?> CreateSet<T>(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }
            PropertyInfo propertyInfo2 = propertyInfo;
            return delegate (T o, object? v)
            {
                propertyInfo2.SetValue(o, v, null);
            };
        }

        public object? GetTypeValue(PropertyInfo propertyInfo, object? value)
        {
            if (propertyInfo.PropertyType == typeof(string) && value == null)
            {
                return "";
            }
            return value;
        }
    }
}
