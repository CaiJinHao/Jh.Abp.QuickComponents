using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    public class IAppServiceCodeBuilder : CodeBuilderAbs
    {
        public IAppServiceCodeBuilder(TableDto tableDto, string filePath) : base(tableDto, filePath)
        {
            this.FileName = $"I{table.Name}AppService";
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"using Jh.Abp.Extensions;
using System;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\tpublic interface {FileName}: ICrudRepository<{table.Name}, {table.KeyType}>");
                builder.AppendLine($"\t\t: ICrudApplicationService<{table.Name}, {table.Name}Dto, {table.Name}Dto, {table.KeyType}, {table.Name}RetrieveInputDto, {table.Name}CreateInputDto, {table.Name}UpdateInputDto, {table.Name}DeleteInputDto>");
                builder.AppendLine("\t{");
                builder.AppendLine("\t}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
