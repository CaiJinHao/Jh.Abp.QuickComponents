using Jh.Abp.Domain.Extensions;
using Jh.Abp.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;

namespace Jh.Abp.MenuManagement.Users
{
    public class JhUserAppService
          : CrudApplicationService<IdentityUser, IdentityUserDto, IdentityUserDto, Guid, UserRetrieveInputDto, IdentityUserCreateDto, IdentityUserUpdateDto, UserDeleteInputDto>
        , IJhUserAppService
    {
        public JhUserAppService(ICrudRepository<IdentityUser, Guid> repository) : base(repository)
        {
        }
    }
}
