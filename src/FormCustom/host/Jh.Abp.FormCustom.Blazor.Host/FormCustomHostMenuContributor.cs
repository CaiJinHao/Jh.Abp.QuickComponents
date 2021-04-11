using System.Threading.Tasks;
using Jh.Abp.FormCustom.Localization;
using Volo.Abp.UI.Navigation;

namespace Jh.Abp.FormCustom.Blazor.Host
{
    public class FormCustomHostMenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if(context.Menu.DisplayName != StandardMenus.Main)
            {
                return Task.CompletedTask;
            }

            var l = context.GetLocalizer<FormCustomResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    "FormCustom.Home",
                    l["Menu:Home"],
                    "/",
                    icon: "fas fa-home"
                )
            );

            return Task.CompletedTask;
        }
    }
}
