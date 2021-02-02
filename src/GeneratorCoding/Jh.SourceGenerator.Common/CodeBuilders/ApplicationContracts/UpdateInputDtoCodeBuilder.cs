﻿using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    public class UpdateInputDtoCodeBuilder : CodeBuilderAbs
    {
        public UpdateInputDtoCodeBuilder(TableDto tableDto, string filePath) : base(tableDto, filePath)
        {
            this.FileName = $"{table.Name}UpdateInputDto";
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"using Jh.Abp.Application.Contracts.Dtos;
using Jh.Abp.Application.Contracts.Extensions;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\tpublic class {FileName}: IMethodDto<{table.Name}>");
                builder.AppendLine("\t{");
                {
                    foreach (var _field in table.FieldsCreateOrUpdateInput)
                    {
                        builder.AppendLine($"\t\t/// <summary>");
                        builder.AppendLine($"\t\t/// {_field.Description}");
                        builder.AppendLine($"\t\t/// <summary>");
                        builder.AppendLine($"\t\tpublic string {_field.Name} " + "{ get; set; }");
                    }
                    //IFullRetrieveDto
                    builder.AppendLine("\t\t/// <summary>");
                    builder.AppendLine("\t\t/// 是否软删除");
                    builder.AppendLine("\t\t/// <summary>");
                    builder.AppendLine("\t\tpublic int Deleted " + "{ get; set; }");

                    builder.AppendLine("\t\t/// <summary>");
                    builder.AppendLine("\t\t/// 方法参数回调");
                    builder.AppendLine("\t\t/// <summary>");
                    builder.AppendLine($"\t\tpublic MethodDto<{table.Name}> MethodInput " + "{ get; set; }");
                }
                builder.AppendLine("\t}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
