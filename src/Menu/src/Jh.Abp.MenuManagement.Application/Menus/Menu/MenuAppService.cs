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
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Uow;
using Jh.Abp.Application.Contracts.Extensions;

namespace Jh.Abp.MenuManagement.Menus
{
    public class MenuAppService
        : CrudApplicationService<Menu, MenuDto, MenuDto, Guid, MenuRetrieveInputDto, MenuCreateInputDto, MenuUpdateInputDto, MenuDeleteInputDto>
        , IMenuAppService
    {
        private readonly IMenuRepository menuRepository;

        private readonly IMenuAndRoleMapDomainService menuAndRoleMapDomainService;
        private readonly IMenuAndRoleMapRepository menuAndRoleMapRepository;

        private IMenuAndRoleMapAppService _menuAndRoleMapAppService;
        public IMenuAndRoleMapAppService menuAndRoleMapAppService => LazyGetRequiredService(ref _menuAndRoleMapAppService);

        public MenuAppService(IMenuRepository repository, IMenuAndRoleMapRepository _menuAndRoleMapRepository, IMenuAndRoleMapDomainService _menuAndRoleMapDomainService) : base(repository)
        {
            menuRepository = repository;
            menuAndRoleMapDomainService = _menuAndRoleMapDomainService;
            menuAndRoleMapRepository = _menuAndRoleMapRepository;
        }

        [UnitOfWork]
        public override async Task<Menu> CreateAsync(MenuCreateInputDto inputDto, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entity = await base.CreateAsync(inputDto, autoSave, cancellationToken);
            await menuAndRoleMapDomainService.CreateAsync(inputDto.RoleIds, entity.Id, autoSave, cancellationToken);
            return entity;
        }

        private IEnumerable<Menu> EnumerableCreateAsync(MenuCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var item in inputDtos)
            {
                yield return this.CreateAsync(item, autoSave, cancellationToken).Result;
            }
        }

        [UnitOfWork]
        public override Task<Menu[]> CreateAsync(MenuCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(EnumerableCreateAsync(inputDtos, autoSave, cancellationToken).ToArray());
        }

        [UnitOfWork]
        public override async Task<Menu> DeleteAsync(Guid id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entity = await base.DeleteAsync(id, autoSave, cancellationToken);
            await menuAndRoleMapRepository.DeleteListAsync(a => a.MenuId == entity.Id).ConfigureAwait(false);
            return entity;
        }

        [UnitOfWork]
        public override async Task<Menu[]> DeleteAsync(Guid[] keys, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entitys = await base.DeleteAsync(keys, autoSave, cancellationToken);
            await menuAndRoleMapRepository.DeleteListAsync(a => keys.Contains(a.MenuId)).ConfigureAwait(false);
            return entitys;
        }

        [UnitOfWork]
        public override async Task<Menu[]> DeleteAsync(MenuDeleteInputDto deleteInputDto, string methodStringType = ObjectMethodConsts.Equals, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entitys = await base.DeleteAsync(deleteInputDto, autoSave:autoSave, cancellationToken:cancellationToken);
            await menuAndRoleMapRepository.DeleteListAsync(a => entitys.Select(b => b.Id).Contains(a.MenuId)).ConfigureAwait(false);
            return entitys;
        }
    }
}
