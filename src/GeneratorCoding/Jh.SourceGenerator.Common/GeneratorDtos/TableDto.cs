using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Jh.SourceGenerator.Common.GeneratorDtos
{
    public class TableDto
    {
        public string DbContext { get; }
        public string Namespace { get; }
        public string ControllerBase { get; }
        public TableDto(string dbContext,string namespa,string controllerBase)
        {
            DbContext = dbContext;
            Namespace = namespa;
            ControllerBase = controllerBase;
        }
        /// <summary>
        /// 主键类型
        /// </summary>
        public string KeyType { get; set; } = "Guid";
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 表描述
        /// </summary>
        public string Comment { get; set; }
        private string _inheritClass = "EntityDto";
        /// <summary>
        /// 要继承的类
        /// </summary>
        public string InheritClass
        {
            get { return _inheritClass; }
            set
            {
                if (InheritClass.Contains("FullAuditedAggregateRoot"))
                {
                    _inheritClass = "FullAuditedEntityDto";
                }
                else if (InheritClass.Contains("CreationAuditedEntity"))
                {
                    _inheritClass = "CreationAuditedEntityDto";
                }
                else if (InheritClass.Contains("AuditedAggregateRoot"))
                {
                    _inheritClass = "AuditedEntityDto";
                }
                else
                {
                    _inheritClass = "EntityDto";
                }
            }
        }
        /// <summary>
        /// 所有自定义的字段
        /// </summary>
        public List<FieldDto> FieldsAll { get { return FieldsCreateOrUpdateInput; } }
        /// <summary>
        /// 输入字段
        /// </summary>
        public List<FieldDto> FieldsCreateOrUpdateInput { get; set; }
        /// <summary>
        /// 查询字段
        /// </summary>
        public List<FieldDto> FieldsRetrieve { get; set; }
        /// <summary>
        /// Dot映射排除的字段
        /// </summary>
        public List<FieldDto> FieldsIgnore { get; set; }

        /// <summary>
        /// 要忽略的字段 去FieldsAll中不包含的RetrieveInputDto
        /// </summary>
        public IEnumerable<FieldDto> GetIgnoreFieldsRetrieveInputDto()
        {
            //在两个里面都不包含的才返回
            var fields = FieldsAll.Where(a => !(FieldsRetrieve.Select(b => b.Name).Contains(a.Name)) && !(FieldsIgnore.Select(b => b.Name).Contains(a.Name)));
            return FieldsIgnore.Concat(fields);
        }
    }
}
