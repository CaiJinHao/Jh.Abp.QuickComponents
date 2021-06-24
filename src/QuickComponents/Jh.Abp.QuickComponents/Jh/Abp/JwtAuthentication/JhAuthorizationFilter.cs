using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var allowAnonymousRegexStr = Configuration["App:AllowAnonymousRegex"];
            if (allowAnonymousRegexStr != null)
            {
                var allowAnonymousRegexs = allowAnonymousRegexStr.Split(",", StringSplitOptions.RemoveEmptyEntries);
                if (allowAnonymousRegexs.Length > 0)
                {
                    var path = context.HttpContext.Request.Path.Value;
                    if (path != null && path.Length > 0)
                    {
                        if (allowAnonymousRegexs.Any(a => new Regex(a).IsMatch(path)))
                        {
                            return Task.CompletedTask;
                        }
                    }
                }
            }
            //TODO: 添加对url得过滤，nginx转发挡再外面了
            return base.OnAuthorizationAsync(context);
        }
    }
}
