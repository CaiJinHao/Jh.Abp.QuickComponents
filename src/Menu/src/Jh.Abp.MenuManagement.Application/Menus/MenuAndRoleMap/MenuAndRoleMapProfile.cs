using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AutoMapper;

namespace Jh.Abp.MenuManagement
{
    public class MenuAndRoleMapProfile : Profile
    {
        public MenuAndRoleMapProfile()
        {
            CreateMap<MenuAndRoleMap, MenuAndRoleMapDto>();

            CreateMap<MenuAndRoleMapDeleteInputDto, MenuAndRoleMap>().IgnoreCreationAuditedObjectProperties()
                 .Ignore(a => a.Id).Ignore(a => a.Menu);

            CreateMap<MenuAndRoleMapRetrieveInputDto, MenuAndRoleMap>().IgnoreCreationAuditedObjectProperties()
                 .Ignore(a => a.Id).Ignore(a => a.Menu);

            CreateMap<MenuAndRoleMapUpdateInputDto, MenuAndRoleMap>().IgnoreCreationAuditedObjectProperties()
                 .Ignore(a => a.Id).Ignore(a => a.Menu);
        }
    }
}
