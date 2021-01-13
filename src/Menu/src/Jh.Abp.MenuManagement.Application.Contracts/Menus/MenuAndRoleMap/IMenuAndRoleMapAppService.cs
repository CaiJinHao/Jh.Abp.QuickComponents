using Jh.Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Jh.Abp.MenuManagement.Menus
{
    public interface IMenuAndRoleMapAppService
        :ICrudApplicationService<MenuAndRoleMap, MenuAndRoleMapDto, MenuAndRoleMapDto, Guid, MenuAndRoleMapRetrieveInputDto, MenuAndRoleMapCreateInputDto, MenuAndRoleMapUpdateInputDto, MenuAndRoleMapDeleteInputDto>
    {
        Task<MenuAndRoleMap[]> CreateV2Async(MenuAndRoleMapCreateInputDto inputDto, bool autoSave = false, CancellationToken cancellationToken = default);
    }
}
