using Jh.Abp.EntityFrameworkCore.Extensions;
using Jh.Abp.MenuManagement.EntityFrameworkCore;

using System;
using Volo.Abp.EntityFrameworkCore;
namespace Jh.Abp.MenuManagement
{
	public class MenuPermissionMapRepository : CrudRepository<MenuManagementDbContext, MenuPermissionMap, System.Guid>, IMenuPermissionMapRepository
	{
		public MenuPermissionMapRepository(IDbContextProvider<MenuManagementDbContext> dbContextProvider) : base(dbContextProvider)
		{
		}
	}
}
