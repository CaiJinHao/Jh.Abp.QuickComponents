using AutoMapper;

using Volo.Abp.AutoMapper;
namespace Jh.Abp.MenuManagement
{
	public class MenuPermissionMapProfile : Profile
	{
		public MenuPermissionMapProfile()
		{
		CreateMap<MenuPermissionMap,MenuPermissionMapDto>();
		CreateMap<MenuPermissionMapCreateInputDto, MenuPermissionMap>().IgnoreCreationAuditedObjectProperties().Ignore(a => a.Id)
.Ignore(a => a.Menu)
;
		CreateMap<MenuPermissionMapUpdateInputDto, MenuPermissionMap>().IgnoreCreationAuditedObjectProperties().Ignore(a => a.Id)
.Ignore(a => a.Menu)
;
		}
	}
}
