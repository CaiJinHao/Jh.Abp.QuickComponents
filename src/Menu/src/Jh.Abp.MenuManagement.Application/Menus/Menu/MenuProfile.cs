using AutoMapper;
using Volo.Abp.AutoMapper;

namespace Jh.Abp.MenuManagement.Menus
{
    public  class MenuProfile:Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu,MenuDto>();
            CreateMap<MenuCreateInputDto, Menu>().IgnoreFullAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties).Ignore(a => a.Id).Ignore(a=>a.MenuRoleMaps);
            CreateMap<MenuUpdateInputDto, Menu>().IgnoreFullAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties).Ignore(a => a.Id).Ignore(a => a.MenuRoleMaps);
            CreateMap<MenuDeleteInputDto, Menu>().IgnoreFullAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties).Ignore(a => a.Id).Ignore(a => a.Icon).Ignore(a => a.Sort).Ignore(a => a.Url).Ignore(a => a.Description).Ignore(a => a.MenuRoleMaps);
            CreateMap<MenuRetrieveInputDto, Menu>().IgnoreFullAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties).Ignore(a => a.Id).Ignore(a => a.Icon).Ignore(a => a.Sort).Ignore(a => a.Url).Ignore(a => a.Description).Ignore(a=>a.LastModificationTime).Ignore(a=>a.LastModifierId).Ignore(a => a.MenuRoleMaps);
        }
    }
}
