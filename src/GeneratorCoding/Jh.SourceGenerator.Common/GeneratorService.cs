using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using Jh.SourceGenerator.Common.GeneratorAttributes;
using Jh.SourceGenerator.Common.GeneratorDtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Jh.SourceGenerator.Common.CodeBuilders;
using System.IO;

namespace Jh.SourceGenerator.Common
{
    public class GeneratorService
    {
        public Assembly LoadAssembly { get; }
        public GeneratorService(Assembly assembly)
        {
            LoadAssembly = assembly;
        }

        public IEnumerable<Type> GetLoadableTypes()
        { 
            return LoadAssembly.DefinedTypes.Select((TypeInfo t) => t.AsType());
        }

        public IEnumerable<Type> GetTableClass()
        {
            var classCollection = GetLoadableTypes().Where(cla=>cla.IsClass);
            var tableClass = classCollection.Where(tab=>tab.CustomAttributes.Any(a=>a.AttributeType.Equals(typeof(GeneratorClassAttribute))));
            return tableClass;
        }

        public TableDto GetTableDto(Type classType)
        {
            var tableName = GetTableName(classType);
            return new TableDto(GeneratorConsts.DbContext, GeneratorConsts.Namespace, GeneratorConsts.ControllerBase)
            {
                Name = tableName,
                FieldsCreateOrUpdateInput = GetFieldDto(GetMembers<CreateOrUpdateInputDtoAttribute>(classType)).ToList(),
                FieldsRetrieve = GetFieldDto(GetMembers<RetrieveDtoAttribute>(classType)).ToList()
            };
        }

        public string GetTableName(Type classType)
        {
            return classType.Name;
        }

        public IEnumerable<PropertyInfo> GetMembers<TAttribute>(Type classType)
        {
            foreach (var property in classType.GetProperties())
            {
                var attrName = typeof(TAttribute).Name;
                if (property.CustomAttributes.Any(a => a.AttributeType.Equals(typeof(TAttribute))))
                {
                    yield return property;
                }
            }
        }

        public IEnumerable<FieldDto> GetFieldDto(IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                var descriptionAttr = GetAttrArgs<DescriptionAttribute>(property).FirstOrDefault();
                var description = string.Empty;
                if (description != null)
                {
                    description = descriptionAttr.Value?.ToString();
                }
                var required = GetAttr<RequiredAttribute>(property);
                yield return new FieldDto()
                {
                    Name = GetFiledName(property),
                    Description = description,
                    IsRequired = required != null
                };
            }
        }

        /// <summary>
        /// 获取指定特性
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public CustomAttributeData GetAttr<TAttribute>(PropertyInfo property)
        {
            return property.CustomAttributes
                .Where(a => a.AttributeType.Equals(typeof(TAttribute))).FirstOrDefault();
        }

        public IEnumerable<CustomAttributeTypedArgument> GetAttrArgs<TAttribute>(PropertyInfo property)
        {
            var attr = property.CustomAttributes
                .Where(a => a.AttributeType.Equals(typeof(TAttribute))).FirstOrDefault();
            if (attr == null)
            {
                return default;
            }
            return attr.ConstructorArguments.AsEnumerable();
        }

        public string GetFiledName(PropertyInfo property)
        {
            return property.Name;
        }

        public IEnumerable<dynamic> GeneratorCode()
        { 
            var tableClass = GetTableClass();
            foreach (var item in tableClass)
            {
                var tableDto = GetTableDto(item);
                yield return new
                {
                    item,
                    CreateCreateInputDto = CreateFile(new CreateInputDtoCodeBuilder(tableDto)),
                    CreateRetrieveInputDto = CreateFile(new RetrieveInputDtoCodeBuilder(tableDto)),
                    CreateDeleteInputDto = CreateFile(new DeleteInputDtoCodeBuilder(tableDto)),
                    CreateUpdateInputDto = CreateFile(new UpdateInputDtoCodeBuilder(tableDto)),
                    CreateDomainDto = CreateFile(new DomainDtoCodeBuilder(tableDto)),
                    CreateIAppService = CreateFile(new IAppServiceCodeBuilder(tableDto)),

                    CreateIRepository = CreateFile(new IRepositoryCodeBuilder(tableDto)),
                    CreateRepository = CreateFile(new RepositoryCodeBuilder(tableDto)),

                    CreateAppService = CreateFile(new AppServiceCodeBuilder(tableDto)),
                    CreateProfile = CreateFile(new ProfileCodeBuilder(tableDto)),
            
                    CreateController = CreateFile(new ControllerCodeBuilder(tableDto)),
                };
            }
        }

        public bool CreateFile(CodeBuilderAbs codeBuilder)
        {
            var filePath = Path.Combine(@"E:\TEMP\ttt", codeBuilder.FileName + codeBuilder.Suffix);
            File.WriteAllText(filePath, codeBuilder.ToString());
            return true;
        }
    }
}
