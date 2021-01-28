using System;
using System.Collections.Generic;
using System.Text;
using Jh.SourceGenerator.Common.GeneratorDtos;

namespace Jh.GeneratorCoding.CodeBuilders
{
    public class IRepositoryCodeBuilder
    {
        private TableDto table { get; }
        public IRepositoryCodeBuilder(TableDto tableDto)
        {
            //用构造函数传值
            table = tableDto;
        }

        public override string ToString()
        {
            var builder =new StringBuilder();
            //builder.AppendLine(table.Using);
            builder.AppendLine(@"using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\tpublic class {table.Name}CreateInputDto");
                builder.AppendLine("{");
                {
                    foreach (var _field in table.Fields)
                    {
                        builder.AppendLine($"\t\t/// <summary>");
                        builder.AppendLine($"\t\t/// {_field.Description}");
                        builder.AppendLine($"\t\t/// <summary>");
                        builder.AppendLine($"\t\t[Required]");
                        builder.AppendLine($"\t\tpublic string {_field} " + "{ get; set; }");
                    }
                }
                builder.AppendLine("}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
