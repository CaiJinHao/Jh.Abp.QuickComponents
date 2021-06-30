using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.MultiTenancy;

namespace Jh.Abp.MenuManagement
{
    public class PermissionGrantedRetrieveInputDto : IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }
        public string[] PermissionNames { get; set; }
        public string ProviderName { get; set; } = RolePermissionValueProvider.ProviderName;
    }
}
