# 介绍

该仓库的目的是为了快速开始项目的核心业务而建立，基于[Abp vNext框架](https://docs.abp.io/zh-Hans/abp/latest),会随着Abp框架的最新版本迭代。访问[Abp源码](https://github.com/abpframework);  
该仓库包含基础组件：Swagger、MiniProfiler、SameSite、JWT、OIDC、默认使用中文、CRUD基础代码生成(包括HTML,基于Layui/vue.js)  
详细使用，可参考[Menu模块](https://github.com/CaiJinHao/Jh.Abp.QuickComponents/tree/master/src/Menu)

## UI使用

复制[Menu模块下wwwroot文件夹](https://github.com/CaiJinHao/Jh.Abp.QuickComponents/tree/master/src/Menu/host/Jh.Abp.MenuManagement.HttpApi.Host/wwwroot)到你的项目wwwroot下

## 项目快速启动

## 安装依赖

启动[install_package.bat](https://github.com/CaiJinHao/Jh.Abp.QuickComponents/tree/master/src/Menu/install_package.bat)，来安装你的项目依赖项。

## 代码生成

通过单元测试来生成代码，需要在Domain Class上添加[GeneratorClass]才能被代码生成识别，生成之后记得去除不需要生的[GeneratorClass]，否则会覆盖原有的文件

```C#
            var basePath = @"G:\Temp\";
            var domainAssembly = typeof(MenuManagement.MenuManagementDomainModule).Assembly;
            var domain = @"\AppSettings";
            var options = new GeneratorOptions()
            {
                DbContext = "EquipmentQuotationAppDbContext",
                Namespace = "EquipmentQuotationApp",
                ControllerBase = "EquipmentQuotationAppController",
                CreateContractsPath = @$"{basePath}trunk\src\SupplyDemandPlatform.Application.Contracts{domain}",
                CreateApplicationPath = @$"{basePath}trunk\src\SupplyDemandPlatform.Application{domain}",
                CreateDomainPath = @$"{basePath}trunk\src\SupplyDemandPlatform.Domain{domain}",
                CreateEfCorePath = @$"{basePath}trunk\src\SupplyDemandPlatform.EntityFrameworkCore{domain}",
                CreateHttpApiPath = @$"{basePath}trunk\src\SupplyDemandPlatform.HttpApi\v1{domain}",
                //不需要domain做文件夹
                CreateHtmlPath = @$"{basePath}trunk\host\SupplyDemandPlatform.Web.Unified\wwwroot\main\view",
                CreateHtmlTemplatePath = @"G:\github\mygithub\Jh.Abp.QuickComponents\src\GeneratorCoding\Jh.SourceGenerator.Common\CodeBuilders\Html\Layui"
            };
            var service = new GeneratorService(domainAssembly, options);
            Assert.True(service.GeneratorCode());
```

## 其他使用

其他使用请参照[Abp vNext框架](https://docs.abp.io/zh-Hans/abp/latest)

## PowerDesigner 生成Class

[PowerDesigner 生成Class配置](https://github.com/CaiJinHao/Jh.Abp.QuickComponents/tree/master/powerdesigner.md)

## 版本更新

[版本更新](https://github.com/CaiJinHao/Jh.Abp.QuickComponents/tree/master/UpDateVersion.md)