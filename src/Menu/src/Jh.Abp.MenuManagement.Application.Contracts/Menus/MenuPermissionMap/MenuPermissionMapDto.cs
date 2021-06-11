using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace Jh.Abp.MenuManagement
{
	public class MenuPermissionMapDto: CreationAuditedEntityDto<System.Guid>
	{
		/// <summary>
		/// 菜单外键
		/// <summary>
		public Guid? MenuId { get; set; }
		/// <summary>
		/// 权限标识
		/// <summary>
		public string PermissionName { get; set; }
	}
}
