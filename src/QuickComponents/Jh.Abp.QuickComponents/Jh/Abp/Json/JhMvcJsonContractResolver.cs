using Jh.Abp.Common.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;
using Volo.Abp.AspNetCore.Mvc.Json;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json.Newtonsoft;
using Volo.Abp.Reflection;
using Volo.Abp.Timing;

namespace Jh.Abp.QuickComponents.Json
{
    public class JhMvcJsonContractResolver : DefaultContractResolver, ITransientDependency
    {
        private readonly IServiceCollection _services;
        private  Lazy<AbpJsonIsoDateTimeConverter> _dateTimeConverter;
        public JhMvcJsonContractResolver(IServiceCollection services)
        {
            _services = services;
            NamingStrategy = new CamelCaseNamingStrategy();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            ModifyProperty(member, property);

            return property;
        }

        protected virtual void ModifyProperty(MemberInfo member, JsonProperty property)
        {
            if (property.PropertyType == typeof(string))
            {
                property.ValueProvider = new NullableValueProvider(member);
                return;
            }

            if (property.PropertyType != typeof(DateTime) && property.PropertyType != typeof(DateTime?))
            {
                return;
            }

            if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault<DisableDateTimeNormalizationAttribute>(member) == null)
            {
                if (_dateTimeConverter == null)
                {
                    _dateTimeConverter = _services.GetRequiredServiceLazy<AbpJsonIsoDateTimeConverter>();
                }
                property.Converter = _dateTimeConverter.Value;
            }
        }
    }
}
