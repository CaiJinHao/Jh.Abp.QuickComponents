using Microsoft.Extensions.DependencyInjection;

namespace Jh.Abp.QuickComponents.MiniProfiler
{
    public static class MiniProfilerExtensions
    {
        public static IServiceCollection AddMiniProfilerComponent(this IServiceCollection services)
        {
            services.OnRegistred(MiniProfilerInterceptorRegistrar.RegisterIfNeeded);

            services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";
                options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.Left;
                options.PopupShowTimeWithChildren = true;

                // 可以增加权限
                //options.ResultsAuthorize = request => request.HttpContext.User.Claims.Where(a=>a.Type=="rolename").First().Value== "jinhao";
                //options.UserIdProvider = request => request.HttpContext.User.Identity.Name;

                options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
                options.EnableServerTimingHeader = true;

                options.IgnoredPaths.Add("/wwwroot");
            }).AddEntityFramework();
            return services;
        }
    }
}
