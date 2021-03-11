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
using Jh.Abp.Common;

namespace Jh.SourceGenerator.Common
{
    public class GeneratorService
    {
        public GeneratorOptions generatorOptions { get; set; }
        public Assembly LoadAssembly { get; }
        public GeneratorService(Assembly assembly, GeneratorOptions options)
        {
            LoadAssembly = assembly;
            generatorOptions = options;
            GeneratorConsts.DbContext = options.DbContext;
            GeneratorConsts.Namespace = options.Namespace;
            GeneratorConsts.ControllerBase = options.ControllerBase;
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
            return new TableDto(GeneratorConsts.DbContext, GeneratorConsts.Namespace, GeneratorConsts.ControllerBase)
            {
                Name = GetTableName(classType),
                Comment= GetTableDescription(classType),
                FieldsCreateOrUpdateInput = GetFieldDto(GetMembers<CreateOrUpdateInputDtoAttribute>(classType)).ToList(),
                FieldsRetrieve = GetFieldDto(GetMembers<RetrieveDtoAttribute>(classType)).ToList(),
                FieldsIgnore= GetFieldDto(GetMembers<ProfileIgnoreAttribute>(classType)).ToList(),
            };
        }

        public string GetTableName(Type classType)
        {
            return classType.Name;
        }

        public string GetTableDescription(Type classType)
        {
            var attr = classType.CustomAttributes.Where(a => a.AttributeType.Equals(typeof(DescriptionAttribute))).FirstOrDefault();
            if (attr != null)
            {
                var args = attr.ConstructorArguments.FirstOrDefault();
                if (args != null)
                {
                    return args.Value?.ToString();
                }
            }
            return default;
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
                    Type = property.PropertyType.Name,
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

        public bool GeneratorCode()
        { 
            var tableClass = GetTableClass();
            foreach (var item in tableClass)
            {
                var tableDto = GetTableDto(item);
                {//contracts
                    var CreateInputDto = CreateFile(new CreateInputDtoCodeBuilder(tableDto, generatorOptions.CreateContractsPath));
                    var CreateRetrieveInputDto = CreateFile(new RetrieveInputDtoCodeBuilder(tableDto, generatorOptions.CreateContractsPath));
                    var CreateDeleteInputDto = CreateFile(new DeleteInputDtoCodeBuilder(tableDto, generatorOptions.CreateContractsPath));
                    var CreateUpdateInputDto = CreateFile(new UpdateInputDtoCodeBuilder(tableDto, generatorOptions.CreateContractsPath));
                    var CreateDomainDto = CreateFile(new DomainDtoCodeBuilder(tableDto, generatorOptions.CreateContractsPath));
                    var CreateIAppService = CreateFile(new IAppServiceCodeBuilder(tableDto, generatorOptions.CreateContractsPath));
                }
                var CreateIDapperRepository = CreateFile(new IDapperRepositoryCodeBuilder(tableDto, generatorOptions.CreateDomainPath));
                var CreateIRepository = CreateFile(new IRepositoryCodeBuilder(tableDto, generatorOptions.CreateDomainPath));
                var CreateDapperRepository = CreateFile(new DapperRepositoryCodeBuilder(tableDto, generatorOptions.CreateEfCorePath));
                var CreateRepository = CreateFile(new RepositoryCodeBuilder(tableDto, generatorOptions.CreateEfCorePath));
                var CreateAppService = CreateFile(new AppServiceCodeBuilder(tableDto, generatorOptions.CreateApplicationPath));
                var CreateProfile = CreateFile(new ProfileCodeBuilder(tableDto, generatorOptions.CreateApplicationPath));
                var CreateController = CreateFile(new ControllerCodeBuilder(tableDto, generatorOptions.CreateHttpApiPath));
                //获取去模板列表
                foreach (var file in new DirectoryInfo(generatorOptions.CreateHtmlTemplatePath).GetFiles())
                {
                    if (file.Extension.Equals(".cshtml"))
                    {
                        var _filePath = Path.Combine(generatorOptions.CreateHtmlTemplatePath, file.FullName);
                        CreateFile(new HtmlTemplateCodeBuilder(_filePath, tableDto, generatorOptions.CreateHtmlPath));
                    }
                }
            }
            return true;
        }

        public bool CreateFile(CodeBuilderAbs codeBuilder)
        {
            new DirectoryInfo(codeBuilder.FilePath).CreateDirectoryInfo();
            var filePath = Path.Combine(codeBuilder.FilePath, codeBuilder.FileName + codeBuilder.Suffix);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            File.WriteAllText(filePath, codeBuilder.ToString());
            return true;
        }
    }
}
