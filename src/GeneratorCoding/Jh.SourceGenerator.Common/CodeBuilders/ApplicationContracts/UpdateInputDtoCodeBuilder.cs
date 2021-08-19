using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    public class UpdateInputDtoCodeBuilder : CodeBuilderAbs
    {
        public UpdateInputDtoCodeBuilder(TableDto tableDto, string filePath) : base(tableDto, filePath)
        {
            this.FileName = $"{table.Name}UpdateInputDto";
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"using Jh.Abp.Application.Contracts.Dtos;
using Jh.Abp.Application.Contracts.Extensions;
using System;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ObjectExtending;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\t/// <summary>");
                builder.AppendLine($"\t/// {table.Comment}");
                builder.AppendLine($"\t/// </summary>");
                builder.AppendLine($"\tpublic class {FileName}: ");
                if (table.IsExtensibleObject)
                {
                    builder.AppendLine($"ExtensibleObject,");
                }
                if (table.IsConcurrencyStamp)
                { 
                    builder.AppendLine($"IHasConcurrencyStamp,");
                }
                builder.AppendLine($"IMethodDto<{table.Name}>");
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

                    if (table.IsDelete)
                    {
                        builder.AppendLine("\t\t/// <summary>");
                        builder.AppendLine("\t\t/// 是否删除");
                        builder.AppendLine("\t\t/// </summary>");
                        builder.AppendLine("\t\tpublic  bool IsDeleted { get; set; }");
                    }

                    if (table.IsConcurrencyStamp)
                    {
                        builder.AppendLine("\t\t/// <summary>");
                        builder.AppendLine("\t\t/// 并发检测字段 必须和数据库中的值一样才会允许更新");
                        builder.AppendLine("\t\t/// </summary>");
                        builder.AppendLine("\t\tpublic string ConcurrencyStamp { get; set; }");
                    }

                    builder.AppendLine("\t\t/// <summary>");
                    builder.AppendLine("\t\t/// 方法参数回调");
                    builder.AppendLine("\t\t/// </summary>");
                    builder.AppendLine($"\t\tpublic MethodDto<{table.Name}> MethodInput " + "{ get; set; }");

                    builder.AppendLine($"\t\t public virtual Guid? TenantId " + "{ get; set; }");
                }
                builder.AppendLine("\t}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
