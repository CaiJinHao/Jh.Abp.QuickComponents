using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AspNetCore.Mvc;

namespace Jh.Abp.QuickComponents.HttpApi
{
    public abstract class JhAbpQuickComponentsController:AbpController
    {
        protected JhAbpQuickComponentsController()
        {
            LocalizationResource = typeof(JhAbpQuickComponentsResource);
        }
    }
}
