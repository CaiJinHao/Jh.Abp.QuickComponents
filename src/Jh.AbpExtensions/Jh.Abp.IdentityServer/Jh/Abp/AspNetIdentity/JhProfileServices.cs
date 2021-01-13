using IdentityModel;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using Jh.Abp.Common.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.AspNetIdentity;
using Volo.Abp.MultiTenancy;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Jh.Abp.IdentityServer.Jh.Abp.AspNetIdentity
{
    /// <summary>
    /// 添加自定义的Claims
    /// </summary>
    public class JhProfileServices :  AbpProfileService
    {
        public JhProfileServices(IdentityUserManager userManager, IUserClaimsPrincipalFactory<IdentityUser> claimsFactory, ICurrentTenant currentTenant) : base(userManager, claimsFactory, currentTenant)
        {
        }

        protected override async Task<ClaimsPrincipal> GetUserClaimsAsync(IdentityUser user)
        {
            var claimsPrincipal = await base.GetUserClaimsAsync(user);
            claimsPrincipal.Identities.First().AddClaim(new Claim(Volo.Abp.Security.Claims.AbpClaimTypes.PhoneNumber, user.PhoneNumber ?? ""));
            foreach (var item in user.Roles)
            {
                claimsPrincipal.Identities.First().AddClaim(new Claim(JhJwtClaimTypes.RoleId, item.RoleId.ToString()));
            }
            claimsPrincipal.Identities.First().AddClaim(new Claim(Volo.Abp.Security.Claims.AbpClaimTypes.Name, user.Name ?? ""));
            claimsPrincipal.Identities.First().AddClaim(new Claim(Volo.Abp.Security.Claims.AbpClaimTypes.SurName, user.Surname ?? ""));
            return claimsPrincipal;
        }
    }
}
