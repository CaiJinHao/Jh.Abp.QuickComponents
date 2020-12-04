using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Volo.Abp.Domain.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Jh.Abp.MenuManagement.Menus
{
    public class MenuAndRoleMapDomainService: IMenuAndRoleMapDomainService,IDomainService
    {
        private readonly IMenuAndRoleMapRepository menuAndRoleMapRepository;
        public MenuAndRoleMapDomainService(IMenuAndRoleMapRepository repository)
        {
            menuAndRoleMapRepository = repository;
        }

        private IEnumerable<MenuAndRoleMap> GetEntitys(Guid[] RoleIds, Guid MenuId)
        {
            foreach (var roleid in RoleIds)
            {
                yield return new MenuAndRoleMap(MenuId, roleid);
            }
        }

        public virtual async Task<MenuAndRoleMap[]> CreateAsync(Guid[] RoleIds, Guid MenuId, bool autoSave = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entitys = GetEntitys(RoleIds, MenuId).ToArray();
            await menuAndRoleMapRepository.CreateAsync(entitys, autoSave, cancellationToken).ConfigureAwait(false);
            return entitys;
        }
    }
}
