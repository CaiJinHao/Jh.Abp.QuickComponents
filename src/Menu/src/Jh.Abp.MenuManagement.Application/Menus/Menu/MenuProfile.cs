using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AutoMapper;

namespace Jh.Abp.MenuManagement.Menus
{
    public  class MenuProfile:Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu,MenuDto>();

            CreateMap<MenuCreateInputDto, Menu>().IgnoreAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties)
                 .Ignore(a => a.Id).Ignore(a => a.Use);

            CreateMap<MenuDeleteInputDto, Menu>().IgnoreAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties)
                 .Ignore(a => a.Id).Ignore(a => a.Icon).Ignore(a => a.Sort).Ignore(a => a.Url).Ignore(a => a.Description);

            CreateMap<MenuRetrieveInputDto, Menu>().IgnoreAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties)
                 .Ignore(a => a.Id).Ignore(a => a.Icon).Ignore(a => a.Sort).Ignore(a => a.Url).Ignore(a => a.Description);

            CreateMap<MenuUpdateInputDto, Menu>().IgnoreAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties)
                 .Ignore(a => a.Id).Ignore(a => a.Use);
        }
    }
}
