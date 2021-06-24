using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Jh.Abp.QuickComponents
{
    public class JhSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            var defaultLanguage = context.GetOrNull(LocalizationSettingNames.DefaultLanguage);
            if (defaultLanguage != null)
            {
                defaultLanguage.DefaultValue = "zh-Hans";
            }


        }
    }
}
