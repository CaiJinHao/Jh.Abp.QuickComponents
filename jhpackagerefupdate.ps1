$basePath="G:\github\mygithub\jhabpmodule\modules\setting"
$ItemName="Jh.Abp.JhSetting"

# package“˝”√

dotnet add $basePath\src\$itemName.Application\$itemName.Application.csproj package Jh.Abp.Application
dotnet add $basePath\src\$itemName.Application.Contracts\$itemName.Application.Contracts.csproj package Jh.Abp.Application.Contracts
dotnet add $basePath\src\$itemName.Domain\$itemName.Domain.csproj package Jh.Abp.Domain
dotnet add $basePath\src\$itemName.EntityFrameworkCore\$itemName.EntityFrameworkCore.csproj package Jh.Abp.EntityFrameworkCore
dotnet add $basePath\src\$itemName.Domain\$itemName.Domain.csproj package Jh.SourceGenerator.Common
