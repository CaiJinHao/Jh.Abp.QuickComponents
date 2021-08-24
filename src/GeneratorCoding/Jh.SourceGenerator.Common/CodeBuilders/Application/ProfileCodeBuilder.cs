using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    public class ProfileCodeBuilder : CodeBuilderAbs
    {
        public ProfileCodeBuilder(TableDto tableDto, string filePath) : base(tableDto, filePath)
        {
            FileName = $"{table.Name}Profile";
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"using AutoMapper;
using Volo.Abp.AutoMapper;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\tpublic class {FileName} : Profile");
                builder.AppendLine("\t{");
                {
                    builder.AppendLine($"\t\tpublic {FileName}()");
                    builder.AppendLine("\t\t{");
                    {
                        builder.Append($"\t\tCreateMap<{table.Name},{table.Name}Dto>()");
                        //if (table.IsExtensibleObject)
                        //{
                        //    builder.Append(".MapExtraProperties()");
                        //}
                        builder.Append(";");
                        builder.AppendLine();
                        builder.AppendLine($"\t\tCreateMap<{table.Name}CreateInputDto, {table.Name}>(){table.IgnoreObjectPropertiesCreateInputDto}");
                        foreach (var item in table.FieldsIgnore)
                        {
                            builder.AppendLine($".Ignore(a => a.{item.Name})");
                        }
                        builder.Append(";");
                        builder.AppendLine();

                        builder.AppendLine($"\t\tCreateMap<{table.Name}UpdateInputDto, {table.Name}>(){table.IgnoreObjectProperties}");
                        foreach (var item in table.FieldsIgnore)
                        {
                            builder.AppendLine($".Ignore(a => a.{item.Name})");
                        }
                        builder.Append(";");
                        builder.AppendLine();
                        /*
                         * 没有用到DeleteInputDto、RetrieveInputDto的数据映射
                                                builder.AppendLine($"\t\tCreateMap<{table.Name}DeleteInputDto, {table.Name}>(){table.IgnoreObjectProperties}");
                                                foreach (var item in table.GetIgnoreFieldsRetrieveInputDto())
                                                {
                                                    builder.AppendLine($".Ignore(a => a.{item.Name})");
                                                }
                                                builder.AppendLine(";");

                                                builder.AppendLine($"\t\tCreateMap<{table.Name}RetrieveInputDto, {table.Name}>(){table.IgnoreObjectProperties}");
                                                foreach (var item in table.GetIgnoreFieldsRetrieveInputDto())
                                                {
                                                    builder.AppendLine($".Ignore(a => a.{item.Name})");
                                                }
                                                builder.AppendLine(";");*/
                    }
                    builder.AppendLine("\t\t}");
                }
                builder.AppendLine("\t}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
