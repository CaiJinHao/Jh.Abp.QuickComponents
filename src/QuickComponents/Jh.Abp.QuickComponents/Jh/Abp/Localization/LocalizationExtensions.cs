using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.QuickComponents.Localization
{
    public static class LocalizationExtensions
    {
        public static IServiceCollection AddLocalizationComponent(this IServiceCollection services)
        {
            services.Configure<CultureMapOptions>(options =>
            {
                options.CultureMaps = new List<CultureMapSettings>() {
                    new CultureMapSettings(){ TargetCulture="zh-Hans", SourceCultures=new List<string>(){ "zh-CN" } },
                    new CultureMapSettings(){ TargetCulture="zh-Hant", SourceCultures=new List<string>(){ "zh" } },
                };
                options.UiCultureMaps = new List<CultureMapSettings>() {
                    new CultureMapSettings(){ TargetCulture="zh-Hans", SourceCultures=new List<string>(){ "zh-CN" } },
                    new CultureMapSettings(){ TargetCulture="zh-Hant", SourceCultures=new List<string>(){ "zh" } },
                };
            });
            return services;
        }

        public static IApplicationBuilder UseJhRequestLocalization(this IApplicationBuilder app,
            Action<RequestLocalizationOptions> optionsAction = null)
        {
            return app.UseAbpRequestLocalization(options =>
            {
                options.RequestCultureProviders.Insert(0, new CultureMapRequestCultureProvider());
                optionsAction?.Invoke(options);
            });
        }
    }
}
