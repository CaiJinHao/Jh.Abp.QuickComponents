using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Jh.Abp.MenuManagement.Menus
{
    public interface IMenuAndRoleMapDomainService
    {
        Task<MenuAndRoleMap[]> CreateAsync(Guid[] RoleIds, Guid MenuId, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken));
    }
}
