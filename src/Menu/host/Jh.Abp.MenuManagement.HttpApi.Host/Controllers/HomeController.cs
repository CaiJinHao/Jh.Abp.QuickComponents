using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Jh.Abp.MenuManagement.Controllers
{
    //[AllowAnonymous]
    [Authorize]
    public class HomeController : AbpController
    {
        public ActionResult Index()
        {
            return Redirect("~/swagger");
        }

        public async Task Login()
        {
            if (!CurrentUser.IsAuthenticated)
            {
                await HttpContext.ChallengeAsync("oidc");
            }
        }
    }
}
