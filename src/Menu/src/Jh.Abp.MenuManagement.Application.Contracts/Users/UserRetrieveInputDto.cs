using Jh.Abp.Application.Contracts.Dtos;
using Jh.Abp.Application.Contracts.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;

namespace Jh.Abp.MenuManagement.Users
{
    public class UserRetrieveInputDto : GetIdentityUsersInput, IFullRetrieveDto<IdentityUser>
    {
        public MethodDto<IdentityUser> MethodInput { get; set; }
        public int Deleted { get; set; }
    }
}
