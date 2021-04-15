using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.Abp.QuickComponents.JwtAuthentication
{
    public static class JwtAuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthenticationComponent(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication("Bearer")
                      .AddIdentityServerAuthentication(options =>
                      {
                          options.Authority = configuration["AuthServer:Authority"];
                          options.RequireHttpsMetadata = configuration.GetValue<bool>("AuthServer:RequireHttps");
                          options.ApiName = configuration["AuthServer:ApiName"];
                      });

            return services;
        }

        /// <summary>
        /// 为所有Action添加权限验证，使用之后页面都会进行验证
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthorizeFilter(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options => {
                var policy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                         .RequireAuthenticatedUser()
                         .Build();
                //options.Filters.Add(new AuthorizeFilter(policy));//添加权限过滤器
                options.Filters.Add(new JhAuthorizationFilter(policy,configuration));
            });
            return services;
        }
    }
}
