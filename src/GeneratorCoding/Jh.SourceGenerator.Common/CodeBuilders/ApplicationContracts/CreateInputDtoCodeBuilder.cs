using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    public class CreateInputDtoCodeBuilder : CodeBuilderAbs
    {
        public CreateInputDtoCodeBuilder(TableDto tableDto) : base(tableDto)
        {
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"using System;
using Volo.Abp.Application.Dtos;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\tpublic class {table.Name}Dto: {table.InheritClass}<{table.KeyType}>");
                builder.AppendLine("\t{");
                {
                    foreach (var _field in table.Fields)
                    {
                        builder.AppendLine($"\t\t/// <summary>");
                        builder.AppendLine($"\t\t/// {_field.Description}");
                        builder.AppendLine($"\t\t/// <summary>");
                        if (_field.IsRequired)
                        {
                            builder.AppendLine($"\t\t[Required]");
                        }
                        builder.AppendLine($"\t\tpublic string {_field.Name} " + "{ get; set; }");
                    }
                }
                builder.AppendLine("\t}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
