using Microsoft.Extensions.DependencyInjection;
using Jh.Abp.FormCustom.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.WebAssembly.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Jh.Abp.FormCustom.Blazor
{
    [DependsOn(
        typeof(FormCustomHttpApiClientModule),
        typeof(AbpAutoMapperModule)
        )]
    public class FormCustomBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<FormCustomBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<FormCustomBlazorAutoMapperProfile>(validate: true);
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new FormCustomMenuContributor());
            });

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(FormCustomBlazorModule).Assembly);
            });
        }
    }
}