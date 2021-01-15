using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;
using Volo.Abp.AspNetCore.Mvc.Json;

namespace Jh.Abp.QuickComponents.Jh.Abp.Json
{
    public class JhMvcJsonContractResolver : AbpMvcJsonContractResolver
    {
        public JhMvcJsonContractResolver(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override void ModifyProperty(MemberInfo member, JsonProperty property)
        {
            if (property.PropertyType == typeof(string))
            {
                property.DefaultValue = "";
                return;
            }
            base.ModifyProperty(member, property);
        }
    }

    public class JhMvcNewtonsoftJsonOptionsSetup : IConfigureOptions<MvcNewtonsoftJsonOptions>
    {
        protected IServiceProvider ServiceProvider { get; }

        public JhMvcNewtonsoftJsonOptionsSetup(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public void Configure(MvcNewtonsoftJsonOptions options)
        {
            options.SerializerSettings.ContractResolver = ServiceProvider.GetRequiredService<JhMvcJsonContractResolver>();
        }
    }
}
