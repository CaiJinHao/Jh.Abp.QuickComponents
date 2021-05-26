# PowerDesigner配置

## 注意

生成OOM的时候注意勾掉：convert names to codes  

## C# 2::Profile\Class\Templates\definition

```text
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Jh.SourceGenerator.Common.GeneratorAttributes;
using Volo.Abp;
using JetBrains.Annotations;
using System.Linq;

[%comment%\n]\
\[Description("%Name%")\]
[%visibility% ][%flags% ][%isPartialType%?partial ]class %Code%[%genericTypeParameters%][ : %inheritanceList%][ %genericTypeConstraints%]
{
[\
   %members%\n
]\
[\
   %innerClasses%\n
]\
[\
   %innerInterfaces%\n
]\
}
```

## Attribute/Templates/definition

```text
.if (%isGenerated%) and (%isValidAttribute%)
\n
    .if(%Mandatory%=="TRUE")
\[Required\]\n
    .endif
\[Description("%Name%")\]
\[CreateOrUpdateInputDto\]\n
   .if (%Multiple% == false) and (%isIndexer% == false)
[%visibility% ][%flags% ]%dataType%
        .if(%Mandatory%=="FALSE") and (%dataType% != "string")
?
        .endif
 %Code%[ = %InitialValue%] {get;set;}
   .else
[%visibility% ][%flags% ]%dataType%[%arraySize%]
        .if(%Mandatory%=="FALSE") and (%dataType% != "string")
?
        .endif
 %Code%[ = %InitialValue%]  {get;set;}
   .endif
.endif
```

## Attribute/Templates/effluntapi

```text
b.Property(p => p.[%Code%]).HasComment("%Name%");
b.Property(p => p.[%Code%]).HasMaxLength(%Length%).HasComment("%Name%");
```

## Classfier/Templates/fields

```text
.foreach_item(Attributes,,,%isField%)
   .if ((%@1?% == false) or (%@1% == all) or (%Visibility% == %@1%)) and (%Derived% == false)
      .if (%isRoleAMigrated%)
[%MigratedAssociation.roleAMigrateDefinition%\n]
      .elsif (%isRoleBMigrated%)
[%MigratedAssociation.roleBMigrateDefinition%\n]
      .else
[%definition%\n]
      .endif
   .endif
.next

.foreach_item(Attributes,,,%isField%)
   .if ((%@1?% == false) or (%@1% == all) or (%Visibility% == %@1%)) and (%Derived% == false)
      .if (%isRoleAMigrated%)
[%MigratedAssociation.roleAMigrateDefinition%\n]
      .elsif (%isRoleBMigrated%)
[%MigratedAssociation.roleBMigrateDefinition%\n]
      .else
[%effluntapi%\n]
      .endif
   .endif
.next
```

## PDM生成 MYSQL50::Script\Objects\Column\Add

```text
%20:COLUMN% [%National%?national ]%DATATYPE%[%Unsigned%? unsigned][%ZeroFill%? zerofill][ [.O:[character set][charset]] %CharSet%][.Z:[ %NOTNULL%][%R%?[%PRIMARY%]][%IDENTITY%? auto_increment:[ default %DEFAULT%]][ comment %.q:@OBJTLABL%]]
b.Property(p => p.%COLUMN%)
.if (%Length% != 0)
[.HasMaxLength(%Length%)]
.endif
[.HasComment("%Name%");]
```
