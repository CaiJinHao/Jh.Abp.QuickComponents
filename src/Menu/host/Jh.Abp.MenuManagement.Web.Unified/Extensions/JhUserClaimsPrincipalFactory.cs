using Jh.Abp.Common.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Security.Claims;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Jh.Abp.MenuManagement
{
    //context.Services.AddAbpIdentity().AddClaimsPrincipalFactory<MyUserClaimsPrincipalFactory>();
    /// <summary>
    /// 添加自定义的Claims
    /// </summary>
    public class JhUserClaimsPrincipalFactory : AbpUserClaimsPrincipalFactory
    {
        public IdentityUserManager MyUserManager { get; set; }
        public JhUserClaimsPrincipalFactory(UserManager<IdentityUser> userManager, RoleManager<Volo.Abp.Identity.IdentityRole> roleManager, IOptions<IdentityOptions> options, ICurrentPrincipalAccessor currentPrincipalAccessor, IAbpClaimsPrincipalFactory abpClaimsPrincipalFactory) : base(userManager, roleManager, options, currentPrincipalAccessor, abpClaimsPrincipalFactory)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(IdentityUser _user)
        {
            var principal = await base.CreateAsync(_user);
            var identity = principal.Identities.First();

            var user = await MyUserManager.GetByIdAsync(_user.Id);
            if (user.Roles != null)
            {
                foreach (var item in user.Roles)
                {
                    identity.AddClaim(new Claim(JhJwtClaimTypes.RoleId, item.RoleId.ToString()));
                }
            }
            //identity.AddIfNotContains(new Claim(IdentityRoleExtension.PlatformType, platformType.ToString()));
            return principal;
        }
    }
}
