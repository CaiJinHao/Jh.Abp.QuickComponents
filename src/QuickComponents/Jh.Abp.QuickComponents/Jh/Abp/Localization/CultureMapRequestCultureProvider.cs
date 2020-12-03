using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.RequestLocalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Jh.Abp.QuickComponents.Localization
{
    /*
     哪个模块用就在哪个模块调用
    app.UseAbpRequestLocalization(options =>
            {
                options.RequestCultureProviders.Insert(0, new Jh.Abp.QuickComponents.Localization.CultureMapRequestCultureProvider());
            });
    
    找到请求的语言之后，优先使用cookies中的,cookies中未设置，才按照系统设置的默认语言
    考虑动态数据如何国际化？每个语言独立数据库。存储时使用翻译软件翻译其他语言，进行每个数据库存储。
    建立Localization类提供语言信息，根据语言信息来存储数据
     */
    public class CultureMapRequestCultureProvider : RequestCultureProvider
    {
        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            var cultureMapOptions = httpContext.RequestServices.GetRequiredService<IOptions<CultureMapOptions>>().Value;

            var responseCulture = new HashSet<StringSegment>();
            var responseUiCulture = new HashSet<StringSegment>();
            //获取请求本地化的Provider
            var requestLocalizationOptionsProvider =
                httpContext.RequestServices.GetRequiredService<IAbpRequestLocalizationOptionsProvider>();
            //获取请求的language
            var languageHeaderRequestCultureProvider =
                (await requestLocalizationOptionsProvider.GetLocalizationOptionsAsync())
                .RequestCultureProviders
                .Where(a => a != null && a != this);//不包含当前的Provider
            foreach (var requestCultureProviders in languageHeaderRequestCultureProvider)
            {
                //获取到请求的语言
                var requestCultureProvider = await requestCultureProviders.DetermineProviderCultureResult(httpContext);
                if (requestCultureProvider != null)
                {
                    //找出系统内置的映射语言
                    requestCultureProvider.Cultures.Where(x => x.HasValue).ForEach(culture =>
                    {
                        var map = cultureMapOptions.CultureMaps.FirstOrDefault(x =>
                            x.SourceCultures.Contains(culture.Value, StringComparer.OrdinalIgnoreCase));
                        responseCulture.Add(new StringSegment(map?.TargetCulture ?? culture.Value));
                    });

                   requestCultureProvider.UICultures.Where(x => x.HasValue).ForEach(uiCulture =>
                   {
                       var map = cultureMapOptions.UiCultureMaps.FirstOrDefault(x =>
                           x.SourceCultures.Contains(uiCulture.Value, StringComparer.OrdinalIgnoreCase));
                       responseUiCulture.Add(new StringSegment(map?.TargetCulture ?? uiCulture.Value));
                   });
                }
            }
            //谁在索引0的位置用的就是谁
            return new ProviderCultureResult(responseCulture.ToList(), responseUiCulture.ToList());
        }
    }
}
