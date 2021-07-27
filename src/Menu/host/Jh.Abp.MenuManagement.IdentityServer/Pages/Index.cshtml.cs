using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Jh.Abp.MenuManagement.Pages
{
    public class IndexModel : AbpPageModel
    {
        public AppSettingsDto AppSettingsDto;
        public IndexModel(IOptions<AppSettingsDto> optionsAppSettingsDto) {
            AppSettingsDto = optionsAppSettingsDto.Value;
        }
        public void OnGet()
        {
        }
    }
}