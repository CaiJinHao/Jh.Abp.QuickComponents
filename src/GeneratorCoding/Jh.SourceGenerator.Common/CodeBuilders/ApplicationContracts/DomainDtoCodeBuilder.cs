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
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Domain.Entities;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\t/// <summary>");
                builder.AppendLine($"\t/// {table.Comment}");
                builder.AppendLine($"\t/// </summary>");
                builder.AppendLine($"\tpublic class {FileName}: {table.InheritClass}<{table.KeyType}>");
                if (table.IsConcurrencyStamp)
                {
                    builder.AppendLine($",IHasConcurrencyStamp");
                }
                builder.AppendLine($",IMultiTenant");
                builder.AppendLine("\t{");
                {
                    foreach (var _field in table.FieldsCreateOrUpdateInput)
                    {
                        builder.AppendLine($"\t\t/// <summary>");
                        builder.AppendLine($"\t\t/// {_field.Description}");
                        builder.AppendLine($"\t\t/// </summary>");
                        var nullable = _field.IsNullable ? "?" : "";//可空类型
                        builder.AppendLine($"\t\tpublic {_field.Type}{nullable} {_field.Name} " + "{ get; set; }");
                    }

                    if (table.IsConcurrencyStamp)
                    {
                        builder.AppendLine("\t\t/// <summary>");
                        builder.AppendLine("\t\t/// 并发标识");
                        builder.AppendLine("\t\t/// </summary>");
                        builder.AppendLine("\t\tpublic string ConcurrencyStamp { get; set; }");
                    }

                    builder.AppendLine($"\t\t public virtual Guid? TenantId " + "{ get; set; }");
                }
                builder.AppendLine("\t}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
