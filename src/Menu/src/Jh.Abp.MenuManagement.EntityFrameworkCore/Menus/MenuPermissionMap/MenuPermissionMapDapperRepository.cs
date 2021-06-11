using Jh.Abp.MenuManagement;
using Jh.Abp.MenuManagement.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace MenuManagement
{
    public class MenuPermissionMapDapperRepository : DapperRepository<MenuManagementDbContext>, IMenuPermissionMapDapperRepository, ITransientDependency
	{
		public MenuPermissionMapDapperRepository(IDbContextProvider<MenuManagementDbContext> dbContextProvider) : base(dbContextProvider)
		{
		}
	}
}
