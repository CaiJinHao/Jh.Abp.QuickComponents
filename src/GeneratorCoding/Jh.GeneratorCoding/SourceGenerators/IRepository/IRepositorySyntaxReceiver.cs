﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Jh.SourceGenerator.Common.GeneratorAttributes;
using Jh.SourceGenerator.Common.GeneratorDtos;

namespace Jh.GeneratorCoding.SourceGenerators
{
    public class IRepositorySyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> CandidateClassCollection { get; } = new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            //var classDeclarationSyntax = syntaxNode is ClassDeclarationSyntax;
            if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax
                   && classDeclarationSyntax.AttributeLists.Count>0
                   && classDeclarationSyntax.AttributeLists.Any(a=>a.Attributes.First().Name.GetText().ToString().Equals("Table"))
                   )
            {
                //找到domain table类
                //CandidateClassCollection.Add(classDeclarationSyntax);
                var tableName = GetTableName(classDeclarationSyntax);
            }
        }

        public string GetTableName(ClassDeclarationSyntax  classDeclarationSyntax)
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
            //foreach (var item in classDeclarationSyntax.AttributeLists)
            //{
            //    var attr = item.Attributes.First();
            //    var attrArgs = attr.ArgumentList;
            //    var attrName = attr.Name.GetText().ToString();
            //}
        }

        public TableDto GetTableDto(ClassDeclarationSyntax classDeclarationSyntax)
        {
            var tableName = GetTableName(classDeclarationSyntax);
            var members = GetMembers<CreateInputDtoAttribute>(classDeclarationSyntax);
            return new TableDto()
            {
                Name = tableName,
                Fields = GetFieldDto(members)
            };
        }

        public IEnumerable<FieldDto> GetFieldDto(IEnumerable<PropertyDeclarationSyntax> properties)
        {
            foreach (var property in properties)
            {
                yield return new FieldDto()
                {
                    Name = GetFiledName(property),
                    Description = GetAttrArgs<System.ComponentModel.DescriptionAttribute>(property).First().GetText().ToString(),
                    IsRequired = GetAttr<System.ComponentModel.DataAnnotations.RequiredAttribute>(property) != null
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
            return property.AttributeLists
                .Where(a => GetAttributeName(a.Attributes.First()).Equals(attrName)).First()
                .Attributes.First();
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
            var args = property.AttributeLists
                .Where(a => GetAttributeName(a.Attributes.First()).Equals(attrName)).First()
                .Attributes.First()
                .ArgumentList.Arguments;
            foreach (var item in args)
            {
                yield return item;
            }
            //return args.First().GetText().ToString();
            //获取指定特性的对象
            /*foreach (var _attrs in property.AttributeLists)
            {
                var attr = _attrs.Attributes.First();
                //var attrArgs = attr.ArgumentList;
                //var attrName = attr.Name.GetText().ToString();
                //var attrNameAttr = GetAttributeName(attr);
                if (GetAttributeName(attr).Equals(nameof(CreateInputDtoAttribute)))
                {
                    System.Diagnostics.Debugger.Launch();
                    return attr;
                }
            }*/
        }

        private string GetAttributeName(AttributeSyntax attributeSyntax)
        {
            return attributeSyntax.Name.GetText().ToString() + "Attribute";
        }
    }
}
