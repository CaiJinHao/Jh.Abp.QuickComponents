using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    public class RetrieveInputDtoCodeBuilder : CodeBuilderAbs
    {
        public RetrieveInputDtoCodeBuilder(TableDto tableDto, string filePath) : base(tableDto, filePath)
        {
            this.FileName = $"{table.Name}RetrieveInputDto";
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"using Jh.Abp.Application.Contracts.Dtos;
using Jh.Abp.Application.Contracts.Extensions;
using System;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Application.Dtos;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\t/// <summary>");
                builder.AppendLine($"\t/// {table.Comment}");
                builder.AppendLine($"\t/// </summary>");
                builder.AppendLine($"\tpublic class {FileName}: PagedAndSortedResultRequestDto, IMethodDto<{table.Name}>");
                if (table.IsDelete)
                {
                    builder.AppendLine(", IRetrieveDelete");
                }
                builder.AppendLine($",IMultiTenant");
                builder.AppendLine("\t{");
                {
                    foreach (var _field in table.FieldsRetrieve)
                    {
                        builder.AppendLine($"\t\t/// <summary>");
                        builder.AppendLine($"\t\t/// {_field.Description}");
                        builder.AppendLine($"\t\t/// </summary>");
                        var nullable = _field.IsNullable ? "?" : "";//可空类型
                        builder.AppendLine($"\t\tpublic {_field.Type}{nullable} {_field.Name} " + "{ get; set; }");
                    }

                    if (table.IsDelete)
                    {
                        builder.AppendLine("\t\t/// <summary>");
                        builder.AppendLine("\t\t/// 是否删除");
                        builder.AppendLine("\t\t/// </summary>");
                        builder.AppendLine("\t\tpublic int? Deleted { get; set; }");
                    }

                    builder.AppendLine("\t\t/// <summary>");
                    builder.AppendLine("\t\t/// 方法参数回调");
                    builder.AppendLine("\t\t/// </summary>");
                    builder.AppendLine("\t\t[Newtonsoft.Json.JsonIgnore]");
                    builder.AppendLine($"\t\tpublic MethodDto<{table.Name}> MethodInput " + "{ get; set; }");

                    builder.AppendLine($"\t\t public virtual Guid? TenantId "+ "{ get; set; }");
                }
                builder.AppendLine("\t}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
