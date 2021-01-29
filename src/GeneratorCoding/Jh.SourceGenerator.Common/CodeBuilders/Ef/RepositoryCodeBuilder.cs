﻿using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.CodeBuilders.Ef
{
    public class RepositoryCodeBuilder : CodeBuilderAbs
    {
        public RepositoryCodeBuilder(TableDto tableDto) : base(tableDto)
        {
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"using Jh.Abp.EntityFrameworkCore.Extensions;
using Jh.Abp.MenuManagement.EntityFrameworkCore;
using System;
using Volo.Abp.EntityFrameworkCore;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\tpublic class {table.Name}Repository : CrudRepository<{table.DbContext}, {table.Name}, {table.KeyType}>, I{table.Name}Repository");
                builder.AppendLine("\t{");
                builder.AppendLine("\t}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}