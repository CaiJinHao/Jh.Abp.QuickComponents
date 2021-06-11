using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;
namespace Jh.Abp.MenuManagement
{
	public class MenuPermissionMapCreateInputDto
	{
		/// <summary>
		/// 菜单外键
		/// <summary>
		[Required]
		public Guid? MenuId { get; set; }
		/// <summary>
		/// 权限标识
		/// <summary>
		[Required]
		public string PermissionName { get; set; }
	}
}
