using IdentityServer4.AspNetIdentity;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.AspNetIdentity;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.Security.Claims;

namespace Jh.Abp.MenuManagement.Extensions
{
    public class CustomAbpResourceOwnerPasswordValidator : AbpResourceOwnerPasswordValidator
    {
        public CustomAbpResourceOwnerPasswordValidator(UserManager<Volo.Abp.Identity.IdentityUser> userManager, SignInManager<Volo.Abp.Identity.IdentityUser> signInManager, IdentitySecurityLogManager identitySecurityLogManager, ILogger<ResourceOwnerPasswordValidator<Volo.Abp.Identity.IdentityUser>> logger, IStringLocalizer<AbpIdentityServerResource> localizer, IOptions<AbpIdentityOptions> abpIdentityOptions, IHybridServiceScopeFactory serviceScopeFactory, IOptions<IdentityOptions> identityOptions) : base(userManager, signInManager, identitySecurityLogManager, logger, localizer, abpIdentityOptions, serviceScopeFactory, identityOptions)
        {
        }

        protected override Task AddCustomClaimsAsync(List<Claim> customClaims, Volo.Abp.Identity.IdentityUser user, ResourceOwnerPasswordValidationContext context)
        {
            customClaims.Add(new Claim(AbpClaimTypes.Name, user.Name?.ToString()));
            customClaims.Add(new Claim(AbpClaimTypes.SurName, user.Surname?.ToString()));
            customClaims.Add(new Claim(AbpClaimTypes.PhoneNumber, user.PhoneNumber?.ToString()));
            return base.AddCustomClaimsAsync(customClaims, user, context);
        }
    }
}
