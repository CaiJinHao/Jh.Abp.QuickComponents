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
        public DomainDtoCodeBuilder(TableDto tableDto) : base(tableDto)
        {
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\tpublic class {table.Name}DeleteInputDto");
                builder.AppendLine("\t{");
                {
                    foreach (var _field in table.Fields)
                    {
                        builder.AppendLine($"\t\t/// <summary>");
                        builder.AppendLine($"\t\t/// {_field.Description}");
                        builder.AppendLine($"\t\t/// <summary>");
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
