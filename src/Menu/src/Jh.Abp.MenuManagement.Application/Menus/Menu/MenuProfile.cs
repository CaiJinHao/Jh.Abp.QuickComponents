using AutoMapper;
using Volo.Abp.AutoMapper;

namespace Jh.Abp.MenuManagement
{
    public  class MenuProfile:Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, MenuDto>().MapExtraProperties();
            CreateMap<MenuCreateInputDto, Menu>().IgnoreFullAuditedObjectProperties().Ignore(a=>a.ConcurrencyStamp).Ignore(a => a.Id)
    .Ignore(a => a.MenuRoleMaps).Ignore(a=> a.MenuPermissionMaps)
    ;
            CreateMap<MenuUpdateInputDto, Menu>().IgnoreFullAuditedObjectProperties().Ignore(a => a.Id)
    .Ignore(a => a.MenuRoleMaps).Ignore(a => a.MenuPermissionMaps)
    ;
        }
    }
}
