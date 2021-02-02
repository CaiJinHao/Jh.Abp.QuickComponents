using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Jh.SourceGenerator.Common.GeneratorAttributes;
using Jh.SourceGenerator.Common.GeneratorDtos;
using Jh.SourceGenerator.Common;

namespace Jh.SourceGenerator
{
    public class IRepositorySyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> CandidateClassCollection { get; } = new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax
                   && classDeclarationSyntax.AttributeLists.Count>0
                   && classDeclarationSyntax.AttributeLists.Any(a=>GetAttributeName(a.Attributes.First()).Equals(nameof(GeneratorClassAttribute)))
                   )
            {
                //find  Generator Class
                CandidateClassCollection.Add(classDeclarationSyntax);
            }
        }

        public TableDto GetTableDto(ClassDeclarationSyntax classDeclarationSyntax)
        {
            var tableName = GetTableName(classDeclarationSyntax);
            return new TableDto(GeneratorConsts.DbContext, GeneratorConsts.Namespace, GeneratorConsts.ControllerBase)
            {
                Name = tableName,
                FieldsCreateOrUpdateInput = GetFieldDto(GetMembers<CreateOrUpdateInputDtoAttribute>(classDeclarationSyntax)).ToList(),
                FieldsRetrieve = GetFieldDto(GetMembers<RetrieveDtoAttribute>(classDeclarationSyntax)).ToList(),
            };
        }

        public string GetTableName(ClassDeclarationSyntax classDeclarationSyntax)
        {
            return classDeclarationSyntax.Identifier.ValueText;
        }

        public IEnumerable<PropertyDeclarationSyntax> GetMembers<TAttribute>(ClassDeclarationSyntax classDeclarationSyntax)
        {
            foreach (var item in classDeclarationSyntax.Members)
            {
                var attrName = typeof(TAttribute).Name;
                if (item.AttributeLists.Any(a => GetAttributeName(a.Attributes.First()).Equals(attrName)))
                {
                    yield return item as PropertyDeclarationSyntax;
                }
            }
        }

        public IEnumerable<FieldDto> GetFieldDto(IEnumerable<PropertyDeclarationSyntax> properties)
        {
            foreach (var property in properties)
            {
                var descriptionAttr = GetAttrArgs<System.ComponentModel.DescriptionAttribute>(property).FirstOrDefault();
                var description = string.Empty;
                if (description != null)
                {
                    description = descriptionAttr.GetText().ToString();
                }
                var required = GetAttr<System.ComponentModel.DataAnnotations.RequiredAttribute>(property);
                yield return new FieldDto()
                {
                    Name = GetFiledName(property),
                    Description = description.Trim('"'),
                    IsRequired = required != null,
                };
            }
        }

        public string GetFiledName(PropertyDeclarationSyntax property)
        {
            return property.Identifier.ValueText;
        }

        /// <summary>
        /// 获取指定特性
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public AttributeSyntax GetAttr<TAttribute>(PropertyDeclarationSyntax property)
        {
            var attrName = typeof(TAttribute).Name;
            var attr = property.AttributeLists
                .Where(a => GetAttributeName(a.Attributes.First()).Equals(attrName)).FirstOrDefault();
            if (attr != null)
            {
                return attr.Attributes.First();
            }
            return null;
        }

        /// <summary>
        /// 获取特性对象的参数列表
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public IEnumerable<AttributeArgumentSyntax> GetAttrArgs<TAttribute>(PropertyDeclarationSyntax property)
        {
            var attrName = typeof(TAttribute).Name;
            var attr = property.AttributeLists
                .Where(a => GetAttributeName(a.Attributes.First()).Equals(attrName)).First()
                .Attributes.FirstOrDefault();
            if (attr == null)
            {
                return default;
            }
            return attr.ArgumentList.Arguments.AsEnumerable();
        }

        private string GetAttributeName(AttributeSyntax attributeSyntax)
        {
            return attributeSyntax.Name.GetText().ToString() + "Attribute";
        }
    }
}
