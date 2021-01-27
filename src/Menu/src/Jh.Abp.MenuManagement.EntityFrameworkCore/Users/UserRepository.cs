using Jh.Abp.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Jh.Abp.MenuManagement.Users
{
    public class UserRepository : CrudRepository<IIdentityDbContext, IdentityUser, Guid>, IUserRepository
    {
        public UserRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
