using Jh.Abp.QuickComponents.Application.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;
using Volo.Abp.Auditing;

namespace Jh.Abp.QuickComponents.Application
{
    [DisableAuditing]
    public class JhAbpQuickComponentsApplicationService : ApplicationService
    {
        public JhAbpQuickComponentsApplicationService()
        {
            LocalizationResource = typeof(JhAbpQuickComponentsResource);
            ObjectMapperContext = typeof(JhAbpQuickComponentsApplicationModule);
        }
    }
}
