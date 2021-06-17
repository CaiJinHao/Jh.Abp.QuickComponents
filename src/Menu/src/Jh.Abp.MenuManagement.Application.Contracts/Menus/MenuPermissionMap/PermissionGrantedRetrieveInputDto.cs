using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Authorization.Permissions;

namespace Jh.Abp.MenuManagement
{
    public class PermissionGrantedRetrieveInputDto
    {
        public string[] PermissionNames { get; set; }
        public string ProviderName { get; set; } = RolePermissionValueProvider.ProviderName;
    }
}
