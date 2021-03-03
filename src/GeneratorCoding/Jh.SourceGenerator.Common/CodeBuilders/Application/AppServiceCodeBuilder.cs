using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    public class AppServiceCodeBuilder : CodeBuilderAbs
    {
        public AppServiceCodeBuilder(TableDto tableDto, string filePath) : base(tableDto, filePath)
        {
            this.FileName = $"{table.Name}AppService";
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"using Jh.Abp.Extensions;
using System;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\tpublic class {FileName}");
                builder.AppendLine($"\t\t: CrudApplicationService<{table.Name}, {table.Name}Dto, {table.Name}Dto, {table.KeyType}, {table.Name}RetrieveInputDto, {table.Name}CreateInputDto, {table.Name}UpdateInputDto, {table.Name}DeleteInputDto>,");
                builder.AppendLine($"\t\tI{table.Name}AppService");
                builder.AppendLine("\t{");
                {
                    builder.AppendLine($"\t\tprivate readonly I{table.Name}Repository {table.Name}Repository;");
                    builder.AppendLine($"\t\tprivate readonly I{table.Name}DapperRepository {table.Name}DapperRepository;");
                    builder.AppendLine($"\t\tpublic {FileName}(I{table.Name}Repository repository, I{table.Name}DapperRepository {table.Name.ToLower()}DapperRepository) : base(repository)");
                    builder.AppendLine("\t\t{");
                    builder.AppendLine($"\t\t{table.Name}Repository = repository;");
                    builder.AppendLine($"\t\t{table.Name}DapperRepository = {table.Name.ToLower()}DapperRepository;");
                    builder.AppendLine("\t\t}");
                }
                builder.AppendLine("\t}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
