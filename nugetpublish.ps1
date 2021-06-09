$publishApiKey="oy2cpp6nhqfc7gisjhxqtmlpx7lcylsu3f2nwicky6ripe"
$publishSource="https://api.nuget.org/v3/index.json"

dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.Application\bin\Release\Jh.Abp.Application.2.0.1.1.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.Application.Contracts\bin\Release\Jh.Abp.Application.Contracts.2.0.1.1.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Common\Jh.Abp.Common\bin\Release\Jh.Abp.Common.2.0.1.1.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.Domain\bin\Release\Jh.Abp.Domain.2.0.1.1.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.Domain.Shared\bin\Release\Jh.Abp.Domain.Shared.2.0.1.1.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.EntityFrameworkCore\bin\Release\Jh.Abp.EntityFrameworkCore.2.0.1.1.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.EntityFrameworkCore.Dm\bin\Release\Jh.Abp.EntityFrameworkCore.Dm.0.2.0.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Jh.AbpExtensions\Jh.Abp.EntityFrameworkCore.DmExtensionsCore\bin\Release\Jh.Abp.EntityFrameworkCore.DmExtensionsCore.0.2.0.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.Application\bin\Release\Jh.Abp.MenuManagement.Application.0.1.9.4.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.Application.Contracts\bin\Release\Jh.Abp.MenuManagement.Application.Contracts.0.1.9.4.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.Blazor\bin\Release\Jh.Abp.MenuManagement.Blazor.0.1.9.4.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.Domain\bin\Release\Jh.Abp.MenuManagement.Domain.0.1.9.4.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.Domain.Shared\bin\Release\Jh.Abp.MenuManagement.Domain.Shared.0.1.9.4.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.EntityFrameworkCore\bin\Release\Jh.Abp.MenuManagement.EntityFrameworkCore.0.1.9.4.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.HttpApi\bin\Release\Jh.Abp.MenuManagement.HttpApi.0.1.9.4.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.HttpApi.Client\bin\Release\Jh.Abp.MenuManagement.HttpApi.Client.0.1.9.4.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.MongoDB\bin\Release\Jh.Abp.MenuManagement.MongoDB.0.1.9.4.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\Menu\src\Jh.Abp.MenuManagement.Web\bin\Release\Jh.Abp.MenuManagement.Web.0.1.9.4.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\QuickComponents\Jh.Abp.QuickComponents\bin\Release\Jh.Abp.QuickComponents.1.2.5.5.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\GeneratorCoding\Jh.GeneratorCoding\bin\Release\Jh.SourceGenerator.3.0.1.1.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
dotnet nuget push src\GeneratorCoding\Jh.SourceGenerator.Common\bin\Release\Jh.SourceGenerator.Common.3.0.1.1.nupkg --api-key $publishApiKey --source $publishSource --skip-duplicate
echo "success ok"