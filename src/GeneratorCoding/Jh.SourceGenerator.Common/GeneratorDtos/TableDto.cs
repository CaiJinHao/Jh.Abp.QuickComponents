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
        public TableDto(string dbContext, string namespa, string controllerBase)
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

        public bool IsDelete { get; set; }
        public bool IsConcurrencyStamp { get { return IsExtensibleObject; } }
        public bool IsExtensibleObject { get; set; }

        private string _inheritClass = "EntityDto";
        /// <summary>
        /// 要继承的类
        /// </summary>
        public string InheritClass
        {
            get { return _inheritClass; }
            set
            {
                switch (value)
                {
                    case "AuditedAggregateRootWithUser":
                        {
                            _inheritClass = "ExtensibleAuditedEntityWithUserDto";
                            IsExtensibleObject = true;
                        }
                        break;
                    case "AuditedEntityWithUser":
                        {
                            _inheritClass = "AuditedEntityWithUserDto";
                        }
                        break;
                    case "AuditedAggregateRoot":
                        {
                            _inheritClass = "ExtensibleAuditedEntityDto";
                            IsExtensibleObject = true;
                        }
                        break;
                    case "AuditedEntity":
                        {
                            _inheritClass = "AuditedEntityDto";
                        }
                        break;
                    case "CreationAuditedAggregateRootWithUser":
                        {
                            _inheritClass = "ExtensibleCreationAuditedEntityWithUserDto";
                            IsExtensibleObject = true;
                        }
                        break;
                    case "CreationAuditedEntityWithUser":
                        {
                            _inheritClass = "CreationAuditedEntityWithUserDto";
                        }
                        break;
                    case "CreationAuditedAggregateRoot":
                        {
                            _inheritClass = "ExtensibleCreationAuditedEntityDto";
                            IsExtensibleObject = true;
                        }
                        break;
                    case "CreationAuditedEntity":
                        {
                            _inheritClass = "CreationAuditedEntityDto";
                        }
                        break;
                    case "FullAuditedAggregateRootWithUser":
                        {
                            _inheritClass = "ExtensibleFullAuditedEntityWithUserDto";
                            IsExtensibleObject = true;
                        }
                        break;
                    case "FullAuditedEntityWithUser":
                        {
                            _inheritClass = "FullAuditedEntityWithUserDto";
                        }
                        break;
                    case "FullAuditedAggregateRoot":
                        {
                            _inheritClass = "ExtensibleFullAuditedEntityDto";
                            IsExtensibleObject = true;
                        }
                        break;
                    case "FullAuditedEntity":
                        {
                            _inheritClass = "FullAuditedEntityDto";
                        }
                        break;
                    case "AggregateRoot":
                        {
                            _inheritClass = "ExtensibleEntityDto";
                            IsExtensibleObject = true;
                        }
                        break;
                    case "BasicAggregateRoot":
                    case "Entity":
                    default:
                        {
                            _inheritClass = "EntityDto";
                        }
                        break;
                }
                SetIgnoreObjectProperties(value);
            }
        }
        public string IgnoreObjectProperties { get; set; }
        public string IgnoreObjectPropertiesCreateInputDto { get; set; }
        protected virtual void SetIgnoreObjectProperties(string tempclass)
        {
            var ignoreObjectPropertiesDefault = string.Empty;
            var ignoreObjectPropertiesCreateInputDto = string.Empty;
            switch (tempclass)
            {
                case "AuditedAggregateRootWithUser":
                case "AuditedAggregateRoot":
                    {
                        ignoreObjectPropertiesDefault = ".IgnoreAuditedObjectProperties()";
                        ignoreObjectPropertiesCreateInputDto = ".IgnoreAuditedObjectProperties()";
                        IsExtensibleObject = true;
                    }
                    break;
                case "AuditedEntityWithUser":
                case "AuditedEntity":
                    {
                        ignoreObjectPropertiesDefault = ".IgnoreAuditedObjectProperties()";
                        ignoreObjectPropertiesCreateInputDto = ".IgnoreAuditedObjectProperties()";
                    }
                    break;
                case "CreationAuditedAggregateRootWithUser":
                case "CreationAuditedAggregateRoot":
                    {
                        ignoreObjectPropertiesDefault = ".IgnoreCreationAuditedObjectProperties()";
                        ignoreObjectPropertiesCreateInputDto = ".IgnoreCreationAuditedObjectProperties()";
                        IsExtensibleObject = true;
                    }
                    break;
                case "CreationAuditedEntityWithUser":
                case "CreationAuditedEntity":
                    {
                        ignoreObjectPropertiesDefault = ".IgnoreCreationAuditedObjectProperties()";
                        ignoreObjectPropertiesCreateInputDto = ".IgnoreCreationAuditedObjectProperties()";
                    }
                    break;
                case "FullAuditedAggregateRootWithUser":
                case "FullAuditedAggregateRoot":
                    {
                        ignoreObjectPropertiesDefault = ".IgnoreFullAuditedObjectProperties()";
                        ignoreObjectPropertiesCreateInputDto = ".IgnoreFullAuditedObjectProperties()";
                        IsDelete = true;
                        IsExtensibleObject = true;
                    }
                    break;
                case "FullAuditedEntityWithUser":
                case "FullAuditedEntity":
                    {
                        ignoreObjectPropertiesDefault = ".IgnoreFullAuditedObjectProperties()";
                        ignoreObjectPropertiesCreateInputDto = ".IgnoreFullAuditedObjectProperties()";
                        IsDelete = true;
                    }
                    break;
                case "AggregateRoot":
                    {
                        IsExtensibleObject = true;
                    }
                    break;
                case "Entity":
                default:
                    {
                        //配置为自动生成的都需要在这忽略掉
                    }
                    break;
            }
            ignoreObjectPropertiesDefault += ".Ignore(a => a.Id)";
            ignoreObjectPropertiesCreateInputDto += ".Ignore(a => a.Id)";


            IgnoreObjectProperties = ignoreObjectPropertiesDefault;
            IgnoreObjectPropertiesCreateInputDto = ignoreObjectPropertiesCreateInputDto;
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
        public virtual IEnumerable<FieldDto> GetIgnoreFieldsRetrieveInputDto()
        {
            //在两个里面都不包含的才返回
            var fields = FieldsAll.Where(a => !(FieldsRetrieve.Select(b => b.Name).Contains(a.Name)) && !(FieldsIgnore.Select(b => b.Name).Contains(a.Name)));
            return FieldsIgnore.Concat(fields);
        }

        public virtual string GetGroupName()
        {
            return this.Namespace.Replace(".","");
        }
    }
}
