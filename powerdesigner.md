## 注意

生成OOM的时候注意勾掉：convert names to codes

## Attribute/Templates/definition

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
        .if(%Mandatory%=="FALSE")
?
        .endif
 %fieldCode%[ = %InitialValue%] {get;set;}
   .else
[%visibility% ][%flags% ]%dataType%[%arraySize%]
        .if(%Mandatory%=="FALSE")
?
        .endif
 %fieldCode%[ = %InitialValue%]  {get;set;}
   .endif
.endif

## Attribute/Templates/effluntapi

b.Property(p => p.[%fieldCode%]).HasComment("%Name%");

## Classfier/Templates/fields

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
