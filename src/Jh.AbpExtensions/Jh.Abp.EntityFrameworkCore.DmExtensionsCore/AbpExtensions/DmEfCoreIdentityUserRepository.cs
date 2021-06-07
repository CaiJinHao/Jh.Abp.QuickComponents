using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Jh.Abp.EntityFrameworkCore.DmExtensions.AbpExtensions
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityUserRepository))]
    public class DmEfCoreIdentityUserRepository : EfCoreIdentityUserRepository, IIdentityUserRepository
    {
        public DmEfCoreIdentityUserRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<List<string>> GetRoleNamesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();
            var query = from userRole in dbContext.Set<IdentityUserRole>()
                        join role in dbContext.Roles on userRole.RoleId equals role.Id
                        where userRole.UserId == id
                        select role.Name;
            var organizationUnitIds = dbContext.Set<IdentityUserOrganizationUnit>().Where(q => q.UserId == id).Select(q => q.OrganizationUnitId).ToArray();

            if (organizationUnitIds.Any())
            {
                var querytemp = from ouRole in dbContext.Set<OrganizationUnitRole>()
                                join ou in dbContext.Set<OrganizationUnit>() on ouRole.OrganizationUnitId equals ou.Id
                                where organizationUnitIds.Contains(ouRole.OrganizationUnitId)
                                select ouRole.RoleId;
                var organizationRoleIds = await(
                    querytemp
                ).ToListAsync(GetCancellationToken(cancellationToken));

                var orgUnitRoleNameQuery = dbContext.Roles.Where(r => organizationRoleIds.Contains(r.Id)).Select(n => n.Name);
                var resultQuery = query.Union(orgUnitRoleNameQuery);
                return await resultQuery.ToListAsync(GetCancellationToken(cancellationToken));
            }
            else
            {
                return new List<string>();
            }
        }
    }
}
