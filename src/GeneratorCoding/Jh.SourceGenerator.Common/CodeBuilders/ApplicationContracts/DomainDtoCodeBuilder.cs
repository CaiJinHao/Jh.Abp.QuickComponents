using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    /// <summary>
    /// 所欲Domain的字段都要
    /// </summary>
    public class DomainDtoCodeBuilder : CodeBuilderAbs
    {
        public DomainDtoCodeBuilder(TableDto tableDto, string filePath) : base(tableDto, filePath)
        {
            this.FileName = $"{table.Name}Dto";
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\tpublic class {FileName}: {table.InheritClass}<{table.KeyType}>");
                builder.AppendLine("\t{");
                {
                    foreach (var _field in table.FieldsCreateOrUpdateInput)
                    {
                        builder.AppendLine($"\t\t/// <summary>");
                        builder.AppendLine($"\t\t/// {_field.Description}");
                        builder.AppendLine($"\t\t/// <summary>");
                        builder.AppendLine($"\t\tpublic {_field.Type} {_field.Name} " + "{ get; set; }");
                    }
                }
                builder.AppendLine("\t}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
