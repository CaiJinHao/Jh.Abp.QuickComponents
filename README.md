# Jh.Abp.QuickComponents

Abp webapi项目需要使用的基础组件。Swagger、MiniProfiler、IdentityServer.
提供AccessToken自动验证控制器，请求地址：api/v1/AccessToken

## 修改IdentityServer & Host

修改install_package.bat中的项目名称为你的项目名称，之后执行install_package.bat
Program
hostsettings.json
UseMySQL
	删除掉SQLServer依赖，安装Install-Package Volo.Abp.EntityFrameworkCore.MySQL，更改为依赖AbpEntityFrameworkCoreMySQLModule
	修改IdentityServerHostMigrationsDbContextFactory中的CreateDbContext
            var connectionString = configuration.GetConnectionString("Default");
            var builder = new DbContextOptionsBuilder<IdentityServerHostMigrationsDbContext>()
                  .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), optionsBuilder =>
                  {
                      optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                      //mySQLOptionsAction?.Invoke(optionsBuilder);
                  })
                  .EnableSensitiveDataLogging();
            return new IdentityServerHostMigrationsDbContext(builder.Options);
			
## IdentityServer 

	typeof(JhAbpIdentityServerModule)
	SeedData
	IdentityServerDataSeedContributor
	1q2w3e*(批量替换)
	add-migration(先删掉原有的)
	update-database
	执行程序开始数据迁移，完成之后复制RoleId到Host
	IdentityServer启动报缺少js文件问题,使用命令行在IdentityServer文件夹下依次运行 yarn 、gulp。执行完成之后即可。如果还不行， 有可能是cli版本更新的问题，需要替换最新的js文件
	
## HttpApi.Host

	Module
		typeof(AbpQuickComponentsModule),
		注释掉AddAbpSwaggerGenWithOAuth,AddAuthentication,AddCors
		底部添加：
		context.Services.Configure<AbpExceptionHandlingOptions>(options =>
            {
                options.SendExceptionsDetailsToClients = configuration.GetValue<bool>("AppSettings:SendExceptionsDetailsToClients");
            });

            context.Services.AddAuthorizeFilter();
		注释掉UseCors，UseSwagger，UseAbpSwaggerUI
		添加app.UseJhSwagger(context.GetConfiguration(), this.GetType());
	appsettings.json配置
	添加：
  "App": {
    "CorsOrigins": "https://*.MenuManagement.com,http://localhost:4200,http://localhost:66001,https://localhost:6101",
    "AllowAnonymousArea": "abp",
    "AllowAnonymousController": "AbpApiDefinition"
  },
  "DistributedCache": {
    "KeyPrefix": "SupplyDemandPlatform:"
  },
  "SwaggerApi": {
    "User": {
      "UserNameOrEmailAddress": "admin",
      "Password": "123456"
    },
    "OpenApiInfo": {
      "Title": "你的项目名称",
      "Version": "v1",
      "Description": "你的项目名称"
    },
    "DocumentTitle": "你的项目名称 RESTfull Api",
    "RoutePrefix": "swagger",
    "SwaggerEndpoint": {
      "Name": "Support APP API"
    }
  },
  "IdentityServer": {
    "Clients": {
      "WebApi": {
        "Authority": "https://localhost:44361/",
        "ClientId": "SupplyDemandPlatform_App",
        "ClientSecret": "Cngrain@123",
        "Scope": "role email SupplyDemandPlatform offline_access",
        "RequireHttps": false
      }
    }
  }
  

## Use

最后打开包所在路径将C:\Users\Administrator\.nuget\packages\jh.abp.quickcomponents.httpapi\x.x.x\content\wwwroot文件夹copy到项目根路径即可

## Use Demo 

具体使用可参考Menu模块

## version

Jh.Abp.Application 2.0.0.5 Jh.Abp.QuickComponents 1.2.4.8 Jh.Abp.MenuManagement.Application 0.1.8.8 Jh.SourceGenerator.Common 3.0.0.3