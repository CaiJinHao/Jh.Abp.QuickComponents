using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Jh.SourceGenerator.Common.CodeBuilders
{
    public class PermissionsDefinitionProviderCodeBuilder : CodeBuilderAbs
    {
        public PermissionsDefinitionProviderCodeBuilder(IEnumerable<TableDto> tableDto, string filePath) : base(tableDto, filePath)
        {
            this.FileName = $"{table.Namespace}PermissionDefinitionProvider";
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"
using System;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;");
            builder.AppendLine($"namespace {table.Namespace}");
            builder.AppendLine("{");
            {
                builder.AppendLine($"\tpublic class {table.Namespace}PermissionDefinitionProvider: PermissionDefinitionProvider");
                builder.AppendLine("\t{");
                {
                    var groupName = $"{table.Namespace}";
                    var permissions = $"{groupName}Permissions";
                    builder.AppendLine($"\tpublic override void Define(IPermissionDefinitionContext context)");
                    builder.AppendLine("\t{");
                    {
                        builder.AppendLine($"\t\tvar {groupName}Group = context.AddGroup({groupName}Permissions.GroupName, L(\"Permission:{table.Namespace}\"));");
                        foreach (var item in tables)
                        {
                            var moduleName = $"{item.Name}s";
                            builder.AppendLine($"\t\t  var {moduleName}Permission = {groupName}Group.AddPermission({permissions}.{moduleName}.Default, L(\"Permission:{table.Name}\"));");
                            builder.AppendLine($"\t\t {moduleName}Permission.AddChild({permissions}.{moduleName}.Create, L(\"Permission:Create\"));");
                            builder.AppendLine($"\t\t {moduleName}Permission.AddChild({permissions}.{moduleName}.Update, L(\"Permission:Edit\"));");
                            builder.AppendLine($"\t\t {moduleName}Permission.AddChild({permissions}.{moduleName}.Delete, L(\"Permission:Delete\"));");
                            builder.AppendLine($"\t\t {moduleName}Permission.AddChild({permissions}.{moduleName}.ManagePermissions, L(\"Permission:ManagePermissions\"));");
                        }
                        builder.AppendLine("\t\t //Write additional permission definitions");
                    }
                    builder.AppendLine("\t}");

                    builder.AppendLine($"\t private static LocalizableString L(string name)");
                    builder.AppendLine("\t{");
                    {
                        builder.AppendLine($"\t\t return LocalizableString.Create<{table.Namespace}Resource>(name);");
                    }
                    builder.AppendLine("\t}");

                }
                builder.AppendLine("\t}");
            }
            builder.AppendLine("}");
            return builder.ToString();
        }
    }
}
