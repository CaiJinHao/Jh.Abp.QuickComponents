using Jh.Abp.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;

namespace Jh.Abp.MenuManagement.Users
{
    public interface IUserRepository : ICrudRepository<IdentityUser, Guid>
    {
    }
}
