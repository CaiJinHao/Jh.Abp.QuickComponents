using Jh.SourceGenerator.Common.GeneratorAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace FormCustom
{
	[GeneratorClass]
    [Description("内容表")]
    public class FormField : FullAuditedEntity<Guid>, IMultiTenant
    {
        public FormField(Guid? tenantId = null)
        {
            TenantId = tenantId;
        }

        public Guid? TenantId { get; protected set; }

        [Required]
        [CreateOrUpdateInputDto]
        [Description("表单名")]
        public Guid FormId { get; set; }

        [Required]
        [MaxLength(20)]
        [CreateOrUpdateInputDto]
        [Description("组件类型")]
        public string FormInputType { get; set; }

        [Required]
        [MaxLength(50)]
        [CreateOrUpdateInputDto]
        [Description("字段名称描述")]
        public string FieldNameLable { get; set; }

        [Required]
        [MaxLength(100)]
        [CreateOrUpdateInputDto]
        [Description("站位提示信息")]
        public string Placeholder { get; set; }

        [Required]
        [CreateOrUpdateInputDto]
        [Description("表单栅格")]
        public int Span { get; set; }

        [Required]
        [Description("组件宽度")]
        [CreateOrUpdateInputDto]
        public int Width { get; set; }

        [MaxLength(100)]
        [CreateOrUpdateInputDto]
        [Description("默认值")]
        public string DefaultValue { get; set; }

        [Required]
        [CreateOrUpdateInputDto]
        [Description("是否只读")]
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// 这里只控制表单必填，不做数据库控制必填
        /// </summary>
        [Required]
        [CreateOrUpdateInputDto]
        [Description("是否必填")]
        public bool IsRequired { get; set; }

        [CreateOrUpdateInputDto]
        [MaxLength(200)]
        [Description("正则校验表达式")]
        public string Regx { get; set; }

        //########表结构相关###########

        /// <summary>
        /// 0 不限长度
        /// </summary>
        [Required]
        [CreateOrUpdateInputDto]
        [Description("最大长度")]
        public int MaxLength { get; set; }

        [Required]
        [RetrieveDto]
        [MaxLength(50)]
        [CreateOrUpdateInputDto]
        [Description("字段名称")]
        public string FieldName { get; set; }

        [Required]
        [MaxLength(20)]
        [CreateOrUpdateInputDto]
        [Description("字段类型")]
        public string FieldType { get; set; }
    }
}
