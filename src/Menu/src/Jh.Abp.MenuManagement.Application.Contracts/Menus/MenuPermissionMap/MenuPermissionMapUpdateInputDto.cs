using Jh.Abp.Application.Contracts.Dtos;
using Jh.Abp.Application.Contracts.Extensions;

using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ObjectExtending;
namespace Jh.Abp.MenuManagement
{
	public class MenuPermissionMapUpdateInputDto: 
IMethodDto<MenuPermissionMap>
	{
		/// <summary>
		/// 菜单外键
		/// <summary>
		public Guid? MenuId { get; set; }
		/// <summary>
		/// 权限标识
		/// <summary>
		public string PermissionName { get; set; }
		/// <summary>
		/// 方法参数回调
		/// <summary>
		public MethodDto<MenuPermissionMap> MethodInput { get; set; }
	}
}
