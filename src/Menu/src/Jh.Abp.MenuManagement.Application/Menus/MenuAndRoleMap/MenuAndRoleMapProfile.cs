using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AutoMapper;

namespace Jh.Abp.MenuManagement.Menus
{
    public class MenuAndRoleMapProfile : Profile
    {
        public MenuAndRoleMapProfile()
        {
            CreateMap<MenuAndRoleMap, MenuAndRoleMapDto>();

            CreateMap<MenuAndRoleMapCreateInputDto, MenuAndRoleMap>()
                 .Ignore(a => a.Id);

            CreateMap<MenuAndRoleMapDeleteInputDto, MenuAndRoleMap>()
                 .Ignore(a => a.Id);

            CreateMap<MenuAndRoleMapRetrieveInputDto, MenuAndRoleMap>()
                 .Ignore(a => a.Id);

            CreateMap<MenuAndRoleMapUpdateInputDto, MenuAndRoleMap>()
                 .Ignore(a => a.Id);
        }
    }
}
