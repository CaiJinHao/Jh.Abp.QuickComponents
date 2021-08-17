using Jh.Abp.QuickComponents.Localization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.Authorization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Http;
using Volo.Abp.Json;

namespace Jh.Abp.QuickComponents
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IAuthorizationMiddlewareResultHandler))]
    public class JhAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler, ITransientDependency
    {
        public IStringLocalizerFactory StringLocalizerFactory { get; set; }
        public IStringLocalizer<JhAbpQuickComponentsResource> stringLocalizer { get; set; }
        public IPermissionDefinitionManager PermissionDefinitionManager { get; set; }
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Challenged)
            {
                if (policy.AuthenticationSchemes.Count > 0)
                {
                    foreach (var scheme in policy.AuthenticationSchemes)
                    {
                        await context.ChallengeAsync(scheme);
                    }
                }
                else
                {
                    await context.ChallengeAsync();
                }

                return;
            }
            else if (authorizeResult.Forbidden)
            {
                if (policy.AuthenticationSchemes.Count > 0)
                {
                    foreach (var scheme in policy.AuthenticationSchemes)
                    {
                        await context.ForbidAsync(scheme);
                    }
                }
                else
                {
                    var reqs = authorizeResult.AuthorizationFailure?.FailedRequirements;
                    if (reqs != null)
                    {
                        var errorArray = reqs.Select(a => PermissionDefinitionManager.Get(((PermissionRequirement)a).PermissionName).DisplayName.Localize(StringLocalizerFactory).Value).ToList();
                        var message = $"{stringLocalizer["AuthorizationFailure", string.Join(",", errorArray)]}";
                        await HandleAndWrapException(context, new Exception(message));
                    }
                    else
                    {
                        await context.ForbidAsync();
                    }
                }

                return;
            }

            await next(context);
        }

        private async Task HandleAndWrapException(HttpContext httpContext, Exception exception)
        {
            var errorInfoConverter = httpContext.RequestServices.GetRequiredService<IExceptionToErrorInfoConverter>();
            var jsonSerializer = httpContext.RequestServices.GetRequiredService<IJsonSerializer>();
            var options = httpContext.RequestServices.GetRequiredService<IOptions<AbpExceptionHandlingOptions>>().Value;

            httpContext.Response.Clear();
            httpContext.Response.StatusCode = 401;
            httpContext.Response.OnStarting(ClearCacheHeaders, httpContext.Response);
            httpContext.Response.Headers.Add(AbpHttpConsts.AbpErrorFormat, "true");
            httpContext.Response.ContentType = "application/json; charset=utf-8";

            var data = jsonSerializer.Serialize(
                    new RemoteServiceErrorResponse(
                        errorInfoConverter.Convert(exception, true)//options.SendExceptionsDetailsToClients
                    )
                );
            await httpContext.Response.WriteAsync(data);

            await httpContext
                .RequestServices
                .GetRequiredService<IExceptionNotifier>()
                .NotifyAsync(
                    new ExceptionNotificationContext(exception)
                );
        }

        private Task ClearCacheHeaders(object state)
        {
            var response = (HttpResponse)state;

            response.Headers[HeaderNames.CacheControl] = "no-cache";
            response.Headers[HeaderNames.Pragma] = "no-cache";
            response.Headers[HeaderNames.Expires] = "-1";
            response.Headers.Remove(HeaderNames.ETag);

            return Task.CompletedTask;
        }
    }
}
