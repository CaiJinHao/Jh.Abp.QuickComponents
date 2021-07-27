using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Jh.Abp.QuickComponents
{
    /*
     哪个模块用就在哪个模块 添加该模块依赖
    
    找到请求的语言之后，优先使用cookies中的,cookies中未设置，才按照系统设置的默认语言
    考虑动态数据如何国际化？每个语言独立数据库。存储时使用翻译软件翻译其他语言，进行每个数据库存储。
    建立Localization类提供语言信息，根据语言信息来存储数据
     */

    public class JhSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            var defaultLanguage = context.GetOrNull(LocalizationSettingNames.DefaultLanguage);
            if (defaultLanguage != null)
            {
                defaultLanguage.DefaultValue = "zh-Hans";//默认使用中文
            }


        }
    }
}
