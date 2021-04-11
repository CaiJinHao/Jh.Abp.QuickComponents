using Jh.SourceGenerator.Common.GeneratorAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace FormCustom
{
	[GeneratorClass]
	[Description("表单信息表")]
	public class Form : FullAuditedAggregateRoot<Guid>, IMultiTenant
	{
        [MaxLength(512)]
		[CreateOrUpdateInputDto]
		[Description("后台接口地址")]
		public string Api
		{
			get;
			set;
		}

		[Required]
		[RetrieveDto]
		[MaxLength(100)]
		[CreateOrUpdateInputDto]
		[Description("显示名称")]
		public string DisplayName
		{
			get;
			set;
		}

		[Required]
		[MaxLength(100)]
		[CreateOrUpdateInputDto]
		[Description("表单描述")]
		public string Description
		{
			get;
			set;
		}
		
        public Guid? TenantId { get; protected set; }

		//########表结构相关###########
		[Required]
		[RetrieveDto]
		[MaxLength(100)]
		[CreateOrUpdateInputDto]
		[Description("表名称")]
		public string TableName
		{
			get;
			set;
		}

		protected Form(Guid? tenantId = null)
		{
			TenantId = tenantId;
		}

		public Form(Guid id, string tableName, string displayName, string description, Guid? tenantId = null)
            : base(id)
        {
			TenantId = tenantId;
			TableName = tableName;
            DisplayName = displayName;
            Description = description;
        }
    }
}
