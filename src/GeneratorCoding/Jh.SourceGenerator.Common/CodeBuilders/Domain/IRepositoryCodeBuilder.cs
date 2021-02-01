using System;
using System.Collections.Generic;
using System.Text;
using Jh.SourceGenerator.Common.GeneratorDtos;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    public class IRepositoryCodeBuilder:CodeBuilderAbs
    {
        public IRepositoryCodeBuilder(TableDto tableDto, string filePath) : base(tableDto, filePath)
        {
            this.FileName = $"I{table.Name}Repository";
        }

        public override string ToString()
        {
            var builder =new StringBuilder();
            builder.AppendLine(@"using Jh.Abp.Domain.Extensions;
using System;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\tpublic interface {FileName}: ICrudRepository<{table.Name}, {table.KeyType}>");
                builder.AppendLine("\t{");
                builder.AppendLine("\t}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
