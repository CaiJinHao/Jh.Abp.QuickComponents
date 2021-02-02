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
        /// <summary>
        /// 要继承的类
        /// </summary>
        public string InheritClass { get; set; } = "FullAuditedEntityDto";
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

        //ProfileDto
        //暂时只有一个Id
        //public IEnumerable<FieldDto> CreateOrUpdateInputDtoIgnoreFields { get; set; }
        /// <summary>
        /// 要忽略的字段 去FieldsAll中不包含的RetrieveInputDto
        /// </summary>
        public IEnumerable<FieldDto> GetIgnoreFieldsRetrieveInputDto()
        {
            return FieldsAll.Where(a => !(FieldsRetrieve.Select(b => b.Name).Contains(a.Name)));
        }
    }
}
