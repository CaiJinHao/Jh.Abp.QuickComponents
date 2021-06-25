using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Jh.Abp.MenuManagement
{
	public class MenuPermissionMapDto: CreationAuditedEntityDto<System.Guid>, IMultiTenant
	{
        public virtual Guid? TenantId { get; set; }
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
