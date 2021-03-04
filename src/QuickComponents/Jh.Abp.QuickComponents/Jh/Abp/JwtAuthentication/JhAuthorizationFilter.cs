using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jh.Abp.QuickComponents.JwtAuthentication
{
    public class JhAuthorizationFilter : AuthorizeFilter
    {
        public IConfiguration Configuration;
        public JhAuthorizationFilter(AuthorizationPolicy policy, IConfiguration configuration) : base(policy)
        {
            Configuration = configuration;
        }

        public override Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var allowAnonymousControllerStr = Configuration["App:AllowAnonymousController"];
            if (allowAnonymousControllerStr != null)
            {
                var allowAnonymousControllers = allowAnonymousControllerStr.Split(",", StringSplitOptions.RemoveEmptyEntries);
                if (allowAnonymousControllers.Length > 0)
                {
                    var routeValues = context.ActionDescriptor?.RouteValues;
                    if (routeValues != null && routeValues.ContainsKey("controller"))
                    {
                        if (routeValues.TryGetValue("controller", out string controller) && allowAnonymousControllers.Contains(controller))
                        {
                            return Task.CompletedTask;
                        }
                    }
                }
            }

            var allowAnonymousAreaStr = Configuration["App:AllowAnonymousArea"];
            if (allowAnonymousAreaStr != null)
            {
                var allowAnonymousAreas = allowAnonymousAreaStr.Split(",", StringSplitOptions.RemoveEmptyEntries);
                if (allowAnonymousAreas.Length > 0)
                {
                    var routeValues = context.ActionDescriptor?.RouteValues;
                    if (routeValues != null && routeValues.ContainsKey("area"))
                    {
                        if (routeValues.TryGetValue("area", out string area) && allowAnonymousAreas.Contains(area))
                        {
                            return Task.CompletedTask;
                        }
                    }
                }
            }
            return base.OnAuthorizationAsync(context);
        }
    }
}
