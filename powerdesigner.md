# PowerDesigner配置

## 注意

DataBase→Edit Current DBMS  
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
using Volo.Abp.MultiTenancy;

[%comment%\n]\
\[GeneratorClass\]
\[Description("%Name%")\]
[%visibility% ][%flags% ][%isPartialType%?partial ]class %Code%[%genericTypeParameters%][ : %inheritanceList%][ %genericTypeConstraints%]: FullAuditedEntity<Guid>, IMultiTenant
{
public virtual Guid? TenantId { get; set; }
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


## C# 2::Profile\Attribute\Templates\definition

```text
.if (%isGenerated%) and (%isValidAttribute%)
\n
    .if(%Mandatory%=="TRUE")
\[Required\]\n
    .endif
    .if(%dataType% == "string")
\[MaxLength(200)\]\n
    .elsif(%dataType% == "decimal")
\[Column(TypeName = "decimal(18, 2)")\]\n
    .else
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

## MYSQL50::Script\Objects\Column\Add （使用得是PDM中得解决不能获取长度问题）

```text
b.Property(p => p.%COLUMN%)
.if (%ISPKEY%==Yes)
[.ValueGeneratedOnAdd()]
.elsif  (%Length% != 0)
[.HasMaxLength(%Length%)]
.endif
[.HasComment("%Name%");]
//
```

## MYSQL50::Script\Objects\Table\Create

```text
builder.Entity<%TABLE%>(b=> {
b.HasComment("%COMMENT%");
b.ToTable(options.TablePrefix + "%TABLE%", options.Schema);
b.ConfigureByConvention();
b.Property(p => p.Id).ValueGeneratedOnAdd();

%TABLDEFN%
});
```

## MYSQL50::Script\Objects\PKey\Add  去除所有内容

