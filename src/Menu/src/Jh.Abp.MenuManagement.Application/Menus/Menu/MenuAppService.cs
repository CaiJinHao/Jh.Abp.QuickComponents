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
using Jh.Abp.Common.Linq;

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
        public override Task<Menu[]> CreateAsync(MenuCreateInputDto[] inputDtos, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(EnumerableCreateAsync(inputDtos, autoSave, cancellationToken).ToArray());
        }

        [UnitOfWork]
        public override async Task<Menu> DeleteAsync(Guid id, bool autoSave = false, bool isHard = false, CancellationToken cancellationToken = default)
        {
            /*var data = await MenuDapperRepository.GetDapperListAsync();
            if (data != null)
            {
                var c = await menuDtoRepository.GetDtoDapperListAsync();
                var d = await MenuDapperRepository.GetDtoListAsync();
            }*/
            var entity = await base.DeleteAsync(id, autoSave, isHard, cancellationToken);
            return entity;
        }

        [UnitOfWork]
        public override async Task<Menu[]> DeleteAsync(Guid[] keys, bool autoSave = false, bool isHard = false, CancellationToken cancellationToken = default)
        {
            var entitys = await base.DeleteAsync(keys, autoSave, isHard, cancellationToken);
            return entitys;
        }

        [UnitOfWork]
        public override async Task<Menu[]> DeleteAsync(MenuDeleteInputDto deleteInputDto, string methodStringType = ObjectMethodConsts.EqualsMethod, bool autoSave = false, bool isHard = false, CancellationToken cancellationToken = default)
        {
            var entitys = await base.DeleteAsync(deleteInputDto, autoSave:autoSave, isHard:isHard, cancellationToken:cancellationToken);
            return entitys;
        }
    }
}
