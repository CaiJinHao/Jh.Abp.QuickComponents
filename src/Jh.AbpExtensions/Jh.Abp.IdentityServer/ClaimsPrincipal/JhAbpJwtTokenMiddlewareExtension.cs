using Jh.Abp.Common.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace Jh.Abp.IdentityServer
{
    public static class JhAbpJwtTokenMiddlewareExtension
    {
        /// <summary>
        /// 解决后台api获取不到roleid得信息问题
        /// </summary>
        /// <param name="app"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseJhJwtTokenMiddleware(this IApplicationBuilder app, string schema = "Bearer")
        {
            return app.Use(async (ctx, next) =>
            {
                if (ctx.User.Identity?.IsAuthenticated != true)
                {
                    var result = await ctx.AuthenticateAsync(schema);
                    if (result.Succeeded && result.Principal != null)
                    {
                        var identity = result.Principal.Identities.First();
                        var userid = result.Principal.FindUserId();
                        var identityUserManager = ctx.RequestServices.GetRequiredService<IdentityUserManager>();
                        var user = await identityUserManager.GetByIdAsync((Guid)userid);
                        if (user.Roles != null)
                        {
                            foreach (var item in user.Roles)
                            {
                                identity.AddOrReplace(new Claim(JhJwtClaimTypes.RoleId, item.RoleId.ToString()));
                            }
                        }
                        ctx.User = result.Principal;
                    }
                }

                await next();
            });
        }
    }
}
