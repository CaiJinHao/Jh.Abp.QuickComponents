using System.Threading.Tasks;
using Jh.Abp.MenuManagement.Localization;
using Volo.Abp.UI.Navigation;

namespace Jh.Abp.MenuManagement.Blazor.Host
{
    public class MenuManagementHostMenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if(context.Menu.DisplayName != StandardMenus.Main)
            {
                return Task.CompletedTask;
            }

            var l = context.GetLocalizer<MenuManagementResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    "MenuManagement.Home",
                    l["Menu:Home"],
                    "/",
                    icon: "fas fa-home"
                )
            );

            return Task.CompletedTask;
        }
    }
}
