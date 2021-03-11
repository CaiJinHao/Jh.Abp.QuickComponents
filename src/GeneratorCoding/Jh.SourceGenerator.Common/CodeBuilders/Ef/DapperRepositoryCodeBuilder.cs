using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    public class DapperRepositoryCodeBuilder : CodeBuilderAbs
    {
        public DapperRepositoryCodeBuilder(TableDto tableDto, string filePath) : base(tableDto, filePath)
        {
            this.FileName = $"{table.Name}DapperRepository";
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($@"using Jh.Abp.EntityFrameworkCore.Extensions;
using {table.Namespace}.EntityFrameworkCore;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;
using Dapper;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\tpublic class {FileName} : DapperRepository<{table.DbContext}>, I{table.Name}DapperRepository, ITransientDependency");
                builder.AppendLine("\t{");
                builder.AppendLine($"\t\tpublic {FileName}(IDbContextProvider<{table.DbContext}> dbContextProvider) : base(dbContextProvider)");
                builder.AppendLine("\t\t{");
                builder.AppendLine("\t\t}");
                builder.AppendLine("\t}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
