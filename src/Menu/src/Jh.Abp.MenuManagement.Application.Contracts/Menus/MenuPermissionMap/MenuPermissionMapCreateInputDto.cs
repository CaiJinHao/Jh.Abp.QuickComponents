using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;
namespace Jh.Abp.MenuManagement
{
	public class MenuPermissionMapCreateInputDto : IMultiTenant
	{
        public virtual Guid? TenantId { get; set; }
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
