using Jh.Abp.MenuManagement.Menus;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Microsoft.EntityFrameworkCore;

namespace Jh.Abp.MenuManagement.EntityFrameworkCore
{
    [DependsOn(
        typeof(MenuManagementDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class MenuManagementEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MenuManagementDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddDefaultRepositories<IMenuManagementDbContext>(true);

                //get的时候加载detail
                //options.Entity<Menu>(opt =>
                //{
                //    opt.DefaultWithDetailsFunc = q => q.Include(a => a.MenuRoleMaps);
                //});
            });
        }
    }
}