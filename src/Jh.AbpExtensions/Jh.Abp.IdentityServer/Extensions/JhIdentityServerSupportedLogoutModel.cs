using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.DependencyInjection;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Volo.Abp.Identity;
using Microsoft.Extensions.Configuration;

namespace Jh.Abp.IdentityServer
{
    [ExposeServices(typeof(LogoutModel))]
    public class JhIdentityServerSupportedLogoutModel : IdentityServerSupportedLogoutModel
    {
        private readonly IConfiguration _configuration;
        public JhIdentityServerSupportedLogoutModel(IIdentityServerInteractionService interaction, IConfiguration configuration) : base(interaction)
        {
            _configuration = configuration;
        }

        public override async Task<IActionResult> OnGetAsync()
        {
            await SignInManager.SignOutAsync();

            var logoutId = Request.Query["logoutId"].ToString();

            if (!string.IsNullOrEmpty(logoutId))
            {
                var logoutContext = await Interaction.GetLogoutContextAsync(logoutId);

                await SaveSecurityLogAsync(logoutContext?.ClientId);

                await SignInManager.SignOutAsync();

                HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());
                /*     var postLogoutRedirectUri = logoutContext?.PostLogoutRedirectUri;
                     if (postLogoutRedirectUri == null)
                     {
                         postLogoutRedirectUri = _configuration.GetValue<string>("AppUnifiedLoginUrl");
                     }
                     return Redirect(postLogoutRedirectUri);*/
                var vm = new LoggedOutModel()
                {
                    PostLogoutRedirectUri = logoutContext?.PostLogoutRedirectUri,
                    ClientName = logoutContext?.ClientName,
                    SignOutIframeUrl = logoutContext?.SignOutIFrameUrl
                };

                Logger.LogInformation($"Redirecting to LoggedOut Page...");

                return RedirectToPage("./LoggedOut", vm);
            }

            await SaveSecurityLogAsync();

            if (ReturnUrl != null)
            {
                return LocalRedirect(ReturnUrl);
            }

            Logger.LogInformation(
                $"IdentityServerSupportedLogoutModel couldn't find postLogoutUri... Redirecting to:/Account/Login..");
            return RedirectToPage("/Account/Login");
        }
    }
}
