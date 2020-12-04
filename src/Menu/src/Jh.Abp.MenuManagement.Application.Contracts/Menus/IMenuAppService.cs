using Jh.Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Jh.Abp.MenuManagement.Menus
{
    public interface IMenuAppService
         : ICrudApplicationService<Menu, MenuDto, MenuDto, Guid, MenuRetrieveInputDto, MenuCreateInputDto, MenuUpdateInputDto, MenuDeleteInputDto>
    {
    }
}
