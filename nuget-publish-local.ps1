$QuickComponents="4.4.1004"	#4.4.1001
$SourceGenerator="4.4.2009"	#4.4.2001
$AbpExtensions="4.4.3005"	#4.4.3001
$Env="Debug" #Release

copy .\src\Jh.AbpExtensions\Jh.Abp.Application\bin\$Env\Jh.Abp.Application.$AbpExtensions.nupkg .\document\localnuget

# AbpExtensions
copy .\src\Jh.AbpExtensions\Jh.Abp.Application\bin\$Env\Jh.Abp.Application.$AbpExtensions.nupkg .\document\localnuget
copy .\src\Jh.AbpExtensions\Jh.Abp.Application.Contracts\bin\$Env\Jh.Abp.Application.Contracts.$AbpExtensions.nupkg .\document\localnuget
copy .\src\Common\Jh.Abp.Common\bin\$Env\Jh.Abp.Common.$AbpExtensions.nupkg .\document\localnuget
copy .\src\Jh.AbpExtensions\Jh.Abp.Domain\bin\$Env\Jh.Abp.Domain.$AbpExtensions.nupkg .\document\localnuget
copy .\src\Jh.AbpExtensions\Jh.Abp.Domain.Shared\bin\$Env\Jh.Abp.Domain.Shared.$AbpExtensions.nupkg .\document\localnuget
copy .\src\Jh.AbpExtensions\Jh.Abp.IdentityServer\bin\$Env\Jh.Abp.IdentityServer.$AbpExtensions.nupkg .\document\localnuget
copy .\src\Jh.AbpExtensions\Jh.Abp.EntityFrameworkCore\bin\$Env\Jh.Abp.EntityFrameworkCore.$AbpExtensions.nupkg .\document\localnuget


# QuickComponents
copy .\src\QuickComponents\Jh.Abp.QuickComponents\bin\$Env\Jh.Abp.QuickComponents.$QuickComponents.nupkg .\document\localnuget
copy .\src\QuickComponents\Jh.Abp.QuickComponents.Application\bin\$Env\Jh.Abp.QuickComponents.Application.$QuickComponents.nupkg .\document\localnuget
copy .\src\QuickComponents\Jh.Abp.QuickComponents.Application.Contracts\bin\$Env\Jh.Abp.QuickComponents.Application.Contracts.$QuickComponents.nupkg .\document\localnuget
copy .\src\QuickComponents\Jh.Abp.QuickComponents.HttpApi\bin\$Env\Jh.Abp.QuickComponents.HttpApi.$QuickComponents.nupkg .\document\localnuget

# SourceGenerator
copy .\src\GeneratorCoding\Jh.GeneratorCoding\bin\$Env\Jh.SourceGenerator.$SourceGenerator.nupkg .\document\localnuget
copy .\src\GeneratorCoding\Jh.SourceGenerator.Common\bin\$Env\Jh.SourceGenerator.Common.$SourceGenerator.nupkg .\document\localnuget


echo "copy success"