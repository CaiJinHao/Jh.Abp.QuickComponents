using System;
using System.Collections.Generic;
using System.Text;
using Jh.SourceGenerator.Common.GeneratorDtos;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    public class IRepositoryCodeBuilder:CodeBuilderAbs
    {
        public IRepositoryCodeBuilder(TableDto tableDto) : base(tableDto)
        {
        }

        public override string ToString()
        {
            var builder =new StringBuilder();
            builder.AppendLine(@"using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\tpublic class {table.Name}CreateInputDto");
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
