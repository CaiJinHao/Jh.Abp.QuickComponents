using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;

namespace Jh.Abp.EntityFrameworkCore.DmExtensions
{
    [DependsOn(
       typeof(AbpIdentityEntityFrameworkCoreModule),
       typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule)
        )]
    public class JhEntityFrameworkCoreDmExtensionsModule : AbpModule
    {

    }
}
