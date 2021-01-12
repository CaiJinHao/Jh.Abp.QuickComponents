using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.AspNetIdentity;
using Volo.Abp.MultiTenancy;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Jh.Abp.MenuManagement.Extensions
{
    /// <summary>
    /// 添加自定义的Claims
    /// </summary>
    public class MyProfileServices : AbpProfileService
    {
        public MyProfileServices(IdentityUserManager userManager, IUserClaimsPrincipalFactory<IdentityUser> claimsFactory, ICurrentTenant currentTenant) : base(userManager, claimsFactory, currentTenant)
        {
        }

        protected override async Task<ClaimsPrincipal> GetUserClaimsAsync(IdentityUser user)
        {
            var claimsPrincipal = await base.GetUserClaimsAsync(user);
            claimsPrincipal.Identities
                       .First()
                       .AddClaim(new Claim("roleid", string.Join(',', user.Roles.Select(a => a.RoleId))));
            return claimsPrincipal;
        }
    }
}
