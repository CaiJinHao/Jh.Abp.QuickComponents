$publishApiKey="oy2i6wqus7j5ituk6clahzgduj3ek4furcdwvr6wqiqwzu"
$publishSource="https://api.nuget.org/v3/index.json"

$QuickComponents="4.4.1001"	#4.4.1001
$SourceGenerator="4.4.2001"	#4.4.2001
$AbpExtensions="4.4.3003"	#4.4.3001

# 暂时用不到不再更新
# $EntityFrameworkCoredm="0.2.0"	#4.4.4001
# $MenuManagement="0.1.106"	#4.4.5001

# AbpExtensions
dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.Application\bin\Release\Jh.Abp.Application.$AbpExtensions.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.Application.Contracts\bin\Release\Jh.Abp.Application.Contracts.$AbpExtensions.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Common\Jh.Abp.Common\bin\Release\Jh.Abp.Common.$AbpExtensions.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.Domain\bin\Release\Jh.Abp.Domain.$AbpExtensions.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.Domain.Shared\bin\Release\Jh.Abp.Domain.Shared.$AbpExtensions.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.IdentityServer\bin\Release\Jh.Abp.IdentityServer.$AbpExtensions.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.EntityFrameworkCore\bin\Release\Jh.Abp.EntityFrameworkCore.$AbpExtensions.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate


# QuickComponents
dotnet nuget push src\QuickComponents\Jh.Abp.QuickComponents\bin\Release\Jh.Abp.QuickComponents.$QuickComponents.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\QuickComponents\Jh.Abp.QuickComponents.Application\bin\Release\Jh.Abp.QuickComponents.Application.$QuickComponents.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\QuickComponents\Jh.Abp.QuickComponents.Application.Contracts\bin\Release\Jh.Abp.QuickComponents.Application.Contracts.$QuickComponents.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\QuickComponents\Jh.Abp.QuickComponents.HttpApi\bin\Release\Jh.Abp.QuickComponents.HttpApi.$QuickComponents.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate

# SourceGenerator
dotnet nuget push src\GeneratorCoding\Jh.GeneratorCoding\bin\Release\Jh.SourceGenerator.$SourceGenerator.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\GeneratorCoding\Jh.SourceGenerator.Common\bin\Release\Jh.SourceGenerator.Common.$SourceGenerator.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate

# dm
#dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.EntityFrameworkCore.Dm\bin\Release\Jh.Abp.EntityFrameworkCore.Dm.$EntityFrameworkCoredm.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
#dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.EntityFrameworkCore.DmExtensionsCore\bin\Release\Jh.Abp.EntityFrameworkCore.DmExtensionsCore.$EntityFrameworkCoredm.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate


#menu
#dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.Application\bin\Release\Jh.Abp.MenuManagement.Application.$MenuManagement.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
#dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.Application.Contracts\bin\Release\Jh.Abp.MenuManagement.Application.Contracts.$MenuManagement.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
#dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.Blazor\bin\Release\Jh.Abp.MenuManagement.Blazor.$MenuManagement.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
#dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.Domain\bin\Release\Jh.Abp.MenuManagement.Domain.$MenuManagement.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
#dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.Domain.Shared\bin\Release\Jh.Abp.MenuManagement.Domain.Shared.$MenuManagement.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
#dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.EntityFrameworkCore\bin\Release\Jh.Abp.MenuManagement.EntityFrameworkCore.$MenuManagement.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
#dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.HttpApi\bin\Release\Jh.Abp.MenuManagement.HttpApi.$MenuManagement.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
#dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.HttpApi.Client\bin\Release\Jh.Abp.MenuManagement.HttpApi.Client.$MenuManagement.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
#dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.MongoDB\bin\Release\Jh.Abp.MenuManagement.MongoDB.$MenuManagement.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
#dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.Web\bin\Release\Jh.Abp.MenuManagement.Web.$MenuManagement.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate


echo "success ok"