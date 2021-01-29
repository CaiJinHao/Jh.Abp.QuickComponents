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
                    descriptionAttr.Value?.ToString();
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
                    CreateCreateInputDto = CreateFile(new CreateInputDtoCodeBuilder(tableDto).ToString()),
                    CreateRetrieveInputDto = CreateFile(new RetrieveInputDtoCodeBuilder(tableDto).ToString()),
                    CreateDeleteInputDto = CreateFile(new DeleteInputDtoCodeBuilder(tableDto).ToString()),
                    CreateUpdateInputDto = CreateFile(new UpdateInputDtoCodeBuilder(tableDto).ToString()),
                    CreateDomainDto = CreateFile(new DomainDtoCodeBuilder(tableDto).ToString()),
                    CreateIAppService = CreateFile(new IAppServiceCodeBuilder(tableDto).ToString()),

                    CreateIRepository = CreateFile(new IRepositoryCodeBuilder(tableDto).ToString()),
                    CreateRepository = CreateFile(new RepositoryCodeBuilder(tableDto).ToString()),

                    CreateAppService = CreateFile(new AppServiceCodeBuilder(tableDto).ToString()),
                    CreateProfile = CreateFile(new ProfileCodeBuilder(tableDto).ToString()),
            
                    CreateController = CreateFile(new ControllerCodeBuilder(tableDto).ToString()),
                };
            }
        }

        public bool CreateFile(string codeSource)
        {
            Console.WriteLine(codeSource);
            return true;
        }
    }
}
