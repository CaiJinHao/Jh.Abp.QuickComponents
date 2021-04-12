using Jh.Abp.MenuManagement.Menus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using System.Linq;
using Volo.Abp.Application.Dtos;

namespace Jh.Abp.MenuManagement
{
    public class MenuDataSeeder : ITransientDependency, IMenuDataSeeder
    {
        private readonly IMenuAppService menuAppService;
        public MenuDataSeeder(IMenuAppService appService)
        {
            menuAppService = appService;
        }

        public async Task SeedAsync(Guid roleid)
        {
            var entitys = await menuAppService.GetEntitysAsync(new MenuRetrieveInputDto() { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount });
            if (!entitys.Items.Any())
            {
                await menuAppService.CreateAsync(new MenuCreateInputDto()
                {
                    Code = "A01",
                    Name = "云数据中心",
                    Icon = "fa fa-bars",
                    Sort = 1,
                    RoleIds=new Guid[] { roleid }
                });
                await menuAppService.CreateAsync(new MenuCreateInputDto()
                {
                    Code = "A0101",
                    Name = "设备组管理",
                    Icon = "fa fa-bars",
                    Sort = 1,
                    ParentCode = "A01",
                    Url = "/main/view/equipmentgroup/index.html",
                    RoleIds=new Guid[] { roleid }
                });
                await menuAppService.CreateAsync(new MenuCreateInputDto()
                {
                    Code = "A02",
                    Name = "系统设置",
                    Icon = "fa fa-bars",
                    Sort = 2,
                    RoleIds=new Guid[] { roleid }
                });
                await menuAppService.CreateAsync(new MenuCreateInputDto()
                {
                    Code = "A0201",
                    Name = "菜单管理",
                    Icon = "fa fa-bars",
                    Sort = 1,
                    ParentCode = "A02",
                    Url = "/main/view/menu/index.html",
                    RoleIds=new Guid[] { roleid }
                });
                await menuAppService.CreateAsync(new MenuCreateInputDto()
                {
                    Code = "A0202",
                    Name = "角色权限管理",
                    Icon = "fa fa-bars",
                    Sort = 2,
                    ParentCode = "A02",
                    Url = "/main/view/rolemenuand/index.html",
                    RoleIds = new Guid[] { roleid }
                });
                await menuAppService.CreateAsync(new MenuCreateInputDto()
                {
                    Code = "A0203",
                    Name = "用户管理",
                    Icon = "fa fa-bars",
                    Sort = 3,
                    ParentCode = "A02",
                    Url = "/main/view/user/index.html",
                    RoleIds = new Guid[] { roleid }
                });
                await menuAppService.CreateAsync(new MenuCreateInputDto()
                {
                    Code = "A0204",
                    Name = "系统审计日志",
                    Icon = "fa fa-bars",
                    Sort = 4,
                    ParentCode = "A02",
                    Url = "/main/view/auditLogging/index.html",
                    RoleIds = new Guid[] { roleid }
                });
            }
        }
    }
}
