using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Jh.Abp.FormCustom.Web.Menus
{
    public class FormCustomMenuContributor : IMenuContributor
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
            context.Menu.AddItem(new ApplicationMenuItem(FormCustomMenus.Prefix, displayName: "FormCustom", "~/FormCustom", icon: "fa fa-globe"));

            return Task.CompletedTask;
        }
    }
}