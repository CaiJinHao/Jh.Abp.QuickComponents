using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Jh.Abp.MenuManagement.Web.Menus
{
    public class MenuManagementMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            //Add main menu items.
            context.Menu.AddItem(new ApplicationMenuItem(MenuManagementMenus.Prefix, displayName: "MenuManagement", "~/MenuManagement", icon: "fa fa-globe"));

            return Task.CompletedTask;
        }
    }
}