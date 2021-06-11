using Jh.Abp.Application.Contracts.Extensions;
using Jh.Abp.Common;
using Jh.Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jh.Abp.MenuManagement
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

        public override async Task<Menu> CreateAsync(MenuCreateInputDto inputDto, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            inputDto.MethodInput = new MethodDto<Menu>()
            {
                CreateOrUpdateEntityAction = entity =>
                {
                    foreach (var roleid in inputDto.RoleIds.ToNullList())
                    {
                        entity.AddMenuRoleMap(roleid);
                    }
                }
            };
            return await base.CreateAsync(inputDto, true, cancellationToken);
        }

        protected virtual IEnumerable<Menu> EnumerableCreateAsync(MenuCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var item in inputDtos)
            {
                yield return this.CreateAsync(item, autoSave, cancellationToken).Result;
            }
        }

        public override Task<Menu[]> CreateAsync(MenuCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(EnumerableCreateAsync(inputDtos, autoSave, cancellationToken).ToArray());
        }

        public override Task<MenuDto> UpdateAsync(Guid id, MenuUpdateInputDto updateInput)
        {
            updateInput.MethodInput = new MethodDto<Menu>()
            {
                CreateOrUpdateEntityAction = (entity) =>
                {
                    foreach (var item in updateInput.PermissionNames.ToNullList())
                    {
                        entity.AddMenuPermissionMap(item);
                    }
                }
            };
            return base.UpdateAsync(id, updateInput);
        }
    }
}
