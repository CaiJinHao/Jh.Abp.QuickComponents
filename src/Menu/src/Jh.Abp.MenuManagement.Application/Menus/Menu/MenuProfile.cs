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

            CreateMap<MenuCreateInputDto, Menu>().IgnoreFullAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties)
                 .Ignore(a => a.Id);

            CreateMap<MenuDeleteInputDto, Menu>().IgnoreFullAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties)
                 .Ignore(a => a.Id).Ignore(a => a.Icon).Ignore(a => a.Sort).Ignore(a => a.Url).Ignore(a => a.Description);

            CreateMap<MenuRetrieveInputDto, Menu>().IgnoreFullAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties)
                 .Ignore(a => a.Id).Ignore(a => a.Icon).Ignore(a => a.Sort).Ignore(a => a.Url).Ignore(a => a.Description).Ignore(a=>a.LastModificationTime).Ignore(a=>a.LastModifierId);

            CreateMap<MenuUpdateInputDto, Menu>().IgnoreFullAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties)
                 .Ignore(a => a.Id);
        }
    }
}
