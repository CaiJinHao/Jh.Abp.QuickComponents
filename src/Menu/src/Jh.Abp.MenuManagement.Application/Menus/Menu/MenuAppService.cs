using Jh.Abp.Extensions;
using Jh.Abp.MenuManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Uow;

namespace Jh.Abp.MenuManagement.Menus
{
    public class MenuAppService
        : CrudApplicationService<Menu, MenuDto, MenuDto, Guid, MenuRetrieveInputDto, MenuCreateInputDto, MenuUpdateInputDto, MenuDeleteInputDto>
        , IMenuAppService
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMenuDapperRepository MenuDapperRepository;

        private readonly IMenuAndRoleMapRepository menuAndRoleMapRepository;

        private readonly IMenuAndRoleMapAppService menuAndRoleMapAppService;
        public IMenuDtoRepository menuDtoRepository { get; set; }

        public MenuAppService(IMenuRepository repository,
            IMenuDapperRepository menuDapperRepository, 
            IMenuAndRoleMapRepository _menuAndRoleMapRepository,
            IMenuAndRoleMapAppService _menuAndRoleMapAppService,
            IMenuAndRoleMapDomainService _menuAndRoleMapDomainService) : base(repository)
        {
            menuRepository = repository;
            MenuDapperRepository = menuDapperRepository;
            menuAndRoleMapRepository = _menuAndRoleMapRepository;
            menuAndRoleMapAppService = _menuAndRoleMapAppService;
        }

        [UnitOfWork]
        [Authorize(MenuManagementPermissions.Menus.Create)]
        public override async Task<Menu> CreateAsync(MenuCreateInputDto inputDto, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            var entity = await base.CreateAsync(inputDto, true, cancellationToken);
            if (inputDto.RoleIds != null && inputDto.RoleIds.Length > 0)
            {
                foreach (var roleid in inputDto.RoleIds)
                {
                    entity.AddMenuRoleMap(roleid);
                }
            }
            return entity;
        }

        protected virtual IEnumerable<Menu> EnumerableCreateAsync(MenuCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var item in inputDtos)
            {
                yield return this.CreateAsync(item, autoSave, cancellationToken).Result;
            }
        }

        [UnitOfWork]
        [Authorize(MenuManagementPermissions.Menus.Create)]
        public override Task<Menu[]> CreateAsync(MenuCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(EnumerableCreateAsync(inputDtos, autoSave, cancellationToken).ToArray());
        }
    }
}
