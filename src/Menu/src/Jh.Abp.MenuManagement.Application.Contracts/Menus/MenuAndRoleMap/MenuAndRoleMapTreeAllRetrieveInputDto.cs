using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Authorization.Permissions;

namespace Jh.Abp.MenuManagement
{
    public class MenuAndRoleMapTreeAllRetrieveInputDto
    {
        public Guid RoleId { get; set; }
        public string ProviderName { get; set; } = RolePermissionValueProvider.ProviderName;
        public string ProviderKey { get; set; }
    }
}
