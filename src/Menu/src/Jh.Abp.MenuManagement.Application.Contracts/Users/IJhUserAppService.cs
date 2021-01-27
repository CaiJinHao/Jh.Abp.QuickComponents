using Jh.Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;

namespace Jh.Abp.MenuManagement.Users
{
    public interface IJhUserAppService
        : ICrudApplicationService<IdentityUser, IdentityUserDto, IdentityUserDto, Guid, UserRetrieveInputDto, IdentityUserCreateDto, IdentityUserUpdateDto, UserDeleteInputDto>
    {
    }
}
