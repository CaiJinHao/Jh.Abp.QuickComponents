using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Jh.Abp.EntityFrameworkCore.DmExtensions
{
    [DependsOn(
       typeof(AbpIdentityEntityFrameworkCoreModule)
        )]
    public class JhEntityFrameworkCoreDmExtensionsModule : AbpModule
    {

    }
}
