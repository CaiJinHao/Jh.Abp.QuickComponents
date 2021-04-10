using Jh.Abp.QuickComponents.Localization;
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
        protected JhAbpQuickComponentsApplicationService()
        {
            LocalizationResource = typeof(JhAbpQuickComponentsResource);
            ObjectMapperContext = typeof(JhAbpQuickComponentsApplicationModule);
        }
    }
}
