using Jh.Abp.Common.Extensions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.Security.Claims;

namespace Jh.Abp.IdentityServer
{
    /// <summary>
    /// 解决token中不携带roleid得信息问题
    /// </summary>
    public class JhAbpClaimsPrincipalContributor : IAbpClaimsPrincipalContributor
    {
        public IdentityUserManager MyUserManager { get; set; }

        public async Task ContributeAsync(AbpClaimsPrincipalContributorContext context)
        {
            var userid = context.ClaimsPrincipal.FindUserId();
            var user = await MyUserManager.GetByIdAsync((Guid)userid);
            var claimsIdentity = context.ClaimsPrincipal.Identities.First();
            if (user.Roles != null)
            {
                foreach (var item in user.Roles)
                {
                    claimsIdentity.AddOrReplace(new Claim(JhJwtClaimTypes.RoleId, item.RoleId.ToString()));
                }
            }
            context.ClaimsPrincipal.AddIdentityIfNotContains(claimsIdentity);
        }
    }
}
