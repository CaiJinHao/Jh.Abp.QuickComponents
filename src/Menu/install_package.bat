dotnet add ./src/QingHaiWL.Application/QingHaiWL.Application.csproj package Jh.Abp.Application
dotnet add ./src/QingHaiWL.Application.Contracts/QingHaiWL.Application.Contracts.csproj package Jh.Abp.Application.Contracts
dotnet add ./src/QingHaiWL.Domain/QingHaiWL.Domain.csproj package Jh.SourceGenerator.Common
dotnet add ./src/QingHaiWL.EntityFrameworkCore/QingHaiWL.EntityFrameworkCore.csproj package Jh.Abp.EntityFrameworkCore
dotnet add ./host/QingHaiWL.HttpApi.Host/QingHaiWL.HttpApi.Host.csproj package Jh.Abp.QuickComponents 
dotnet add ./host/QingHaiWL.IdentityServer/QingHaiWL.IdentityServer.csproj package Jh.Abp.IdentityServer 
dotnet add ./src/QingHaiWL.HttpApi/QingHaiWL.HttpApi.csproj package Jh.Abp.MenuManagement.HttpApi
dotnet add ./src/QingHaiWL.Domain/QingHaiWL.Domain.csproj package Jh.Abp.Domain

# 注意版本
dotnet add ./src/QingHaiWL.EntityFrameworkCore/QingHaiWL.EntityFrameworkCore.csproj package Volo.Abp.Dapper

# add ref
### Application.Contracts.csproj add Domain.csproj
### EntityFrameworkCore add Application.Contracts.csproj

# Use MySql