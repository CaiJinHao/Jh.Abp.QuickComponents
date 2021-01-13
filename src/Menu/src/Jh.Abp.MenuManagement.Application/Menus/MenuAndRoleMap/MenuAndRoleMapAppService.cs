using Jh.Abp.Domain.Extensions;
using Jh.Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Jh.Abp.MenuManagement.Menus
{
    public class MenuAndRoleMapAppService
        : CrudApplicationService<MenuAndRoleMap, MenuAndRoleMapDto, MenuAndRoleMapDto, Guid, MenuAndRoleMapRetrieveInputDto, MenuAndRoleMapCreateInputDto, MenuAndRoleMapUpdateInputDto, MenuAndRoleMapDeleteInputDto>,
        IMenuAndRoleMapAppService
    {
        private readonly IMenuAndRoleMapRepository MenuAndRoleMapRepository;
        public MenuAndRoleMapAppService(IMenuAndRoleMapRepository repository) : base(repository)
        {
            MenuAndRoleMapRepository = repository;
        }

        public override Task<MenuAndRoleMap> CreateAsync(MenuAndRoleMapCreateInputDto inputDto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override Task<MenuAndRoleMap[]> CreateAsync(MenuAndRoleMapCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public override async Task<MenuAndRoleMap> DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return await base.DeleteAsync(id, autoSave, cancellationToken);
        }

        public override async Task<MenuAndRoleMap[]> DeleteAsync(Guid[] keys, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return await base.DeleteAsync(keys, autoSave, cancellationToken);
        }

        public override async Task<MenuAndRoleMap[]> DeleteAsync(MenuAndRoleMapDeleteInputDto deleteInputDto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return await base.DeleteAsync(deleteInputDto, autoSave, cancellationToken);
        }

        [UnitOfWork]
        public async Task<MenuAndRoleMap[]> CreateV2Async(MenuAndRoleMapCreateInputDto inputDto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return await crudRepository.CreateAsync(GetCreateEnumerableAsync(inputDto).ToArray());
        }

        private IEnumerable<MenuAndRoleMap> GetCreateEnumerableAsync(MenuAndRoleMapCreateInputDto inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var roleid in inputDtos.RoleIds)
            {
                foreach (var menuid in inputDtos.MenuIds)
                {
                    yield return new MenuAndRoleMap(menuid, roleid, GuidGenerator.Create());
                }
            }
        }
    }
}
