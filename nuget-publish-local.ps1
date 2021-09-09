$QuickComponents="4.4.1004"	#4.4.1001
$SourceGenerator="4.4.2011"	#4.4.2001
$AbpExtensions="4.4.3007"	#4.4.3001
$Env="Debug" #Release

cd G:\github\mygithub\Jh.Abp.QuickComponents

# AbpExtensions
rm .\document\localnuget\Jh.Abp.Application.*.nupkg
rm .\document\localnuget\Jh.Abp.Application.Contracts.*.nupkg
rm .\document\localnuget\Jh.Abp.Common.*.nupkg
rm .\document\localnuget\Jh.Abp.Domain.*.nupkg
rm .\document\localnuget\Jh.Abp.Domain.Shared.*.nupkg
rm .\document\localnuget\Jh.Abp.IdentityServer.*.nupkg
rm .\document\localnuget\Jh.Abp.EntityFrameworkCore.*.nupkg

# QuickComponents
rm .\document\localnuget\Jh.Abp.QuickComponents.*.nupkg
rm .\document\localnuget\Jh.Abp.QuickComponents.Application.*.nupkg
rm .\document\localnuget\Jh.Abp.QuickComponents.Application.Contracts.*.nupkg
rm .\document\localnuget\Jh.Abp.QuickComponents.HttpApi.*.nupkg

# SourceGenerator
rm .\document\localnuget\Jh.SourceGenerator.*.nupkg
rm .\document\localnuget\Jh.SourceGenerator.Common.*.nupkg


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