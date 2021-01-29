﻿using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.CodeBuilders.Application
{
    public class ProfileCodeBuilder : CodeBuilderAbs
    {
        public ProfileCodeBuilder(TableDto tableDto) : base(tableDto)
        {
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"using AutoMapper;
using Volo.Abp.AutoMapper;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\tpublic class {table.Name}Profile : Profile");
                builder.AppendLine("\t{");
                { 
                    //TODO: 将对应的类中的没有的字段排除掉
                    builder.AppendLine($"\t\tCreateMap<{table.Name},{table.Name}Dto>();");
                    builder.AppendLine($"\t\tCreateMap<{table.Name}CreateInputDto, {table.Name}>().IgnoreFullAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties).Ignore(a => a.Id);");
                    builder.AppendLine($"\t\tCreateMap<{table.Name}UpdateInputDto, {table.Name}>().IgnoreFullAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties).Ignore(a => a.Id);");

                    builder.AppendLine($"\t\tCreateMap<{table.Name}DeleteInputDto, {table.Name}>().IgnoreFullAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties).Ignore(a => a.Id)");
                    foreach (var item in table.RetrieveInputDtoIgnoreFields)
                    {
                        builder.AppendLine($".Ignore(a => a.{item.Name})");
                    }
                    builder.AppendLine(";");
                    builder.AppendLine($"\t\tCreateMap<{table.Name}RetrieveInputDto, {table.Name}>().IgnoreFullAuditedObjectProperties().Ignore(a => a.ConcurrencyStamp).Ignore(a => a.ExtraProperties).Ignore(a => a.Id).Ignore(a=>a.LastModificationTime).Ignore(a=>a.LastModifierId)");
                    foreach (var item in table.RetrieveInputDtoIgnoreFields)
                    {
                        builder.AppendLine($".Ignore(a => a.{item.Name})");
                    }
                    builder.AppendLine(";");
                }
                builder.AppendLine("\t}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}