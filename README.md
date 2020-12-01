# Jh.Abp.QuickComponents

Abp webapi项目需要使用的基础组件。Swagger、MiniProfiler、IdentityServer.
提供AccessToken自动验证控制器，请求地址：api/v1/AccessToken

## Swagger

> 在appsetings.json添加Swagger配置
"SwaggerApi": {
    "User": {
      "UserNameOrEmailAddress": "admin",
      "Password": "123456"
    },
    "OpenApiInfo": {
      "Title": "YourProjectName Title",
      "Version": "v1",
      "Description": "YourProjectName Description"
    },
    "DocumentTitle": "XXX平台 RESTfull Api",
    "RoutePrefix": "swagger",
    "SwaggerEndpoint": {
      "Name": "Support APP API"
    }
 }
> 在ConfigureServices中删除掉或者注释掉以下代码段
/*
 context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "YourProjectName API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                });
*/
>在OnApplicationInitialization中这么用
//app.UseAbpRequestLocalization();
app.UseJhRequestLocalization();
/*
app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");
                });
*/
app.UseSwaggerComponent(configuration,this.GetType());

## IdentityServer

> 在appsetings.json添加IdentityServer配置
"AuthServer": {
    "Authority": "https://localhost:6002/",
    "ApiName": "YourProjectName",
    "RequireHttps": false
},
"IdentityServer": {
    "Clients": {
      "Web": {
        "Authority": "https://localhost:6002/",
        "ClientId": "YourProjectName_Web",
        "ClientSecret": "1q2w3e*",
        "Scope": "role email YourProjectName offline_access",
        "RequireHttps": false
      },
      "WebApi": {
        "Authority": "https://localhost:6002/",
        "ClientId": "YourProjectName_ConsoleTestApp",
        "ClientSecret": "1q2w3e*",
        "Scope": "role email YourProjectName offline_access",
        "RequireHttps": false
      }
    }
  }
> 在ConfigureServices中删除掉或者注释掉以下代码段
/*
context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.Audience = "YourProjectName";
                });
*/
/*
context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
*/

## 引用模块

typeof(AbpQuickComponentsModule),
typeof(JhAbpQuickComponentsHttpApiModule)

## 完整配置

"Redis": {
    "Configuration": "127.0.0.1"
  },
  "AuthServer": {
    "Authority": "https://localhost:6002/",
    "ApiName": "YourProjectName",
    "RequireHttps": false
  },
  "DistributedCache": {
    "KeyPrefix": "YourProjectName:"
  },
  "SwaggerApi": {
    "User": {
      "UserNameOrEmailAddress": "admin",
      "Password": "123456"
    },
    "OpenApiInfo": {
      "Title": "YourProjectName Title",
      "Version": "v1",
      "Description": "YourProjectName Description"
    },
    "DocumentTitle": "XXX平台 RESTfull Api",
    "RoutePrefix": "swagger",
    "SwaggerEndpoint": {
      "Name": "Support APP API"
    }
  },
  "IdentityServer": {
    "Clients": {
      "Web": {
        "Authority": "https://localhost:6002/",
        "ClientId": "YourProjectName_Web",
        "ClientSecret": "1q2w3e*",
        "Scope": "role email YourProjectName offline_access",
        "RequireHttps": false
      },
      "WebApi": {
        "Authority": "https://localhost:6002/",
        "ClientId": "YourProjectName_ConsoleTestApp",
        "ClientSecret": "1q2w3e*",
        "Scope": "role email YourProjectName offline_access",
        "RequireHttps": false
      }
    }
  }

## Use

最后打开包所在路径将C:\Users\Administrator\.nuget\packages\jh.abp.quickcomponents.httpapi\x.x.x\content\wwwroot文件夹copy到项目根路径即可
