using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Volo.Abp.AspNetCore.Mvc;

namespace Jh.Abp.QuickComponents.Swagger
{
    public static partial class SwaggerExtensions
    {
        public static IServiceCollection AddApiVersion(this IServiceCollection services)
        {
            services.AddAbpApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                 //是否在没有填写版本号的情况下使用默认版本
                 options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);

                 //options.ApiVersionReader = new HeaderApiVersionReader("api-version"); //Supports header too
                 //options.ApiVersionReader = new MediaTypeApiVersionReader(); //Supports accept header too

                 var mvcOptions = services.ExecutePreConfiguredActions<AbpAspNetCoreMvcOptions>();
                options.ConfigureAbp(mvcOptions);
            });
            return services;
        }

        [Obsolete("Please use the AddJhAbpSwagger")]
        public static IServiceCollection AddJhSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiVersion();
            return services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = configuration["SwaggerApi:OpenApiInfo:Title"],
                        Version = configuration["SwaggerApi:OpenApiInfo:Version"],
                        Description = configuration["SwaggerApi:OpenApiInfo:Description"],
                    });
                    options.DocInclusionPredicate((docName, description) =>
                    {
                        var _nowControllerAction = (ControllerActionDescriptor)description.ActionDescriptor;
                        var _controller = _nowControllerAction.ControllerTypeInfo;
                        var _nowHttpMethodAttr = _nowControllerAction.MethodInfo.GetCustomAttribute<HttpMethodAttribute>();
                        var _nowMapToApiVersionAttr = _nowControllerAction.MethodInfo.GetCustomAttributes<MapToApiVersionAttribute>();
                        if (_nowMapToApiVersionAttr==null)
                        {
                            return true;
                        }
                        //拿到当前方法中最大的版本号
                        var _nowMethodMaxVersion = _nowMapToApiVersionAttr.Select(a => a.Versions.FirstOrDefault()).OrderByDescending(a => a.MajorVersion).ThenBy(a => a.MinorVersion).FirstOrDefault();

                        //找到与当前方法时同一个HTTP的方法只显示最小版本的或者最大版本的
                        //找到多个匹配的方法
                        var equalsMethods = _controller.DeclaredMethods
                        .Where(a => a.GetCustomAttributes<HttpMethodAttribute>().Contains(_nowHttpMethodAttr)).ToList();
                        //从多个匹配的方法中找到最大的版本号的方法与当前方法判断是否匹配
                        var _nowControllerVersions = new List<ApiVersion>();
                        foreach (var item in equalsMethods)
                        {
                            //找到每个方法的多个Version
                            var mapToApiVersions = item.GetCustomAttributes<MapToApiVersionAttribute>();
                            var actionVersions = mapToApiVersions.Select(a => a.Versions.FirstOrDefault());//测试发现Versions只有一个长度
                            _nowControllerVersions.AddRange(actionVersions);
                        }
                        var _nowControllerMaxVersion = _nowControllerVersions.OrderByDescending(a => a.MajorVersion).ThenByDescending(a => a.MinorVersion).FirstOrDefault();
                        //当前方法中最大的版本号和当前控制中最大的版本号显示最大的，整个控制器只显示一个
                        if (_nowMethodMaxVersion == _nowControllerMaxVersion)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });
                    options.CustomSchemaIds(type => type.FullName);

                    //Swagger添加授权验证服务
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Description = "授权Token：Bearer Token",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    });

                    //options.SchemaFilter<SwaggerSchemaFilter>();
                    //options.DocumentFilter<SwaggerDocumentFilter>();
                });
        }

        [Obsolete("Please use the UseJhAbpSwagger")]
        public static IApplicationBuilder UseJhSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            //StarupType用于加载Swagger文档的类的程序集
            //if (StarupType == null)
            //{
            //    throw new ArgumentNullException("Swagger Starup Type Is Null");
            //}
            app.UseSwagger();
            return app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", configuration["SwaggerApi:SwaggerEndpoint:Name"]);

                //路由地址
                options.RoutePrefix = configuration["SwaggerApi:RoutePrefix"];

                /*index.html 为嵌入的资源
                index.html页面修改需要重新生成项目*/
                // Assembly assembly = Assembly.GetExecutingAssembly();
                //string[] resNames = assembly.GetManifestResourceNames();  //列出所有资源名称
                options.IndexStream = () => Assembly.GetExecutingAssembly().GetManifestResourceStream("Jh.Abp.QuickComponents.Jh.Abp.Swagger.index.html");//这个是用点连接的途径
                // Display
                options.DefaultModelExpandDepth(2);
                options.DefaultModelRendering(ModelRendering.Model);
                options.DefaultModelsExpandDepth(-1);
                options.DisplayOperationId();
                options.DisplayRequestDuration();
                options.DocExpansion(DocExpansion.None);
                options.EnableDeepLinking();
                options.EnableFilter();
                options.ShowExtensions();
                // Network
                options.EnableValidator();
                //ui.SupportedSubmitMethods();

                // Other
                options.DocumentTitle = configuration["SwaggerApi:DocumentTitle"];
                options.InjectStylesheet("/ext/custom-stylesheet.css");
                options.InjectJavascript("/ext/custom-javascript.js");
            });
        }


        public static IServiceCollection AddJhAbpSwagger(this IServiceCollection services, IConfiguration configuration
            , Dictionary<string, string> scopes, Action<SwaggerGenOptions> steupAction = null, params NamespaceAssemblyDto[] XmlCommentsNamespaceAssemblys)
        {
            services.AddAbpSwaggerGenWithOAuth(
                configuration["AuthServer:Authority"],scopes,
                options =>
                {
                    options.SwaggerDoc("v1",
                      new OpenApiInfo
                      {
                          Title = configuration["SwaggerApi:OpenApiInfo:Title"],
                          Version = configuration["SwaggerApi:OpenApiInfo:Version"],
                          Description = configuration["SwaggerApi:OpenApiInfo:Description"],
                      });
                    options.DocInclusionPredicate((docName, description) => true);
                    //options.CustomSchemaIds(type => type.FullName);

                    //Swagger添加授权验证服务
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Description = "授权Token：Bearer Token",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    });

                    //options.SchemaFilter<SwaggerSchemaFilter>();//Schemax过滤器、修改器
                    //options.ParameterFilter<SwaggerSchemaFilter>();//Parameter过滤器、修改器
                    //options.RequestBodyFilter<>();//请求Body过滤器、修改器
                    //options.OperationFilter<>();//操作过滤器、修改器
                    //options.DocumentFilter<>();//文档过滤器、修改器
                    //options.IncludeXmlComments();
                    //var basePath = Directory.GetCurrentDirectory();
                    //var xmlPath = Path.Combine(basePath, "Swagger/YourWebApiName.ApiServices.xml");

                    if (XmlCommentsNamespaceAssemblys != null)
                    {
                        foreach (var item in XmlCommentsNamespaceAssemblys)
                        {
                            var embeddedFileProvider = new EmbeddedFileProvider(item.AssemblyXmlComments, item.BaseNamespace);//文件必须是嵌入得资源
                            var files = embeddedFileProvider.GetDirectoryContents(string.Empty).Where(a => a.Name.EndsWith(".xml"));
                            foreach (var file in files)
                            {
                                var content = file.ReadAsString();
                                options.IncludeXmlComments(() => {
                                    return new System.Xml.XPath.XPathDocument(new StringReader(content));
                                }, true);//为操作、参数和模式注入基于XML注释文件的友好描述
                            }
                        }
                    }

                    options.IgnoreObsoleteActions();//忽略任何由ObsoleteAttribute修饰的操作
                    options.DescribeAllParametersInCamelCase();//描述所有参数，不管它们在代码中是如何出现的，都使用驼峰形式
                    //options.ResolveConflictingActions((apiDescriptionList) =>
                    //{
                    //    //合并具有冲突的HTTP方法和路径的操作(Swagger 2.0必须是唯一的)
                    //    return apiDescriptionList.First();
                    //});
                    //属性
                    options.IgnoreObsoleteProperties();//忽略任何用ObsoleteAttribute装饰的属性
                    options.UseAllOfForInheritance();//启用复合模式生成,合并基类的属性
                    options.UseAllOfToExtendReferenceSchemas();//扩展引用模式(使用allOf构造)，以便上下文元数据可以应用于所有参数和属性模式
                    options.SupportNonNullableReferenceTypes();//启用对非空引用类型的检测，在模式属性上相应地设置空标志
                    steupAction?.Invoke(options);
                });
            return services;
        }

        public static IApplicationBuilder UseJhAbpSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", configuration["SwaggerApi:SwaggerEndpoint:Name"]);
                options.RoutePrefix = configuration["SwaggerApi:RoutePrefix"];
                options.IndexStream = () => Assembly.GetExecutingAssembly().GetManifestResourceStream("Jh.Abp.QuickComponents.Jh.Abp.Swagger.index.html");//这个是用点连接的途径

                options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);

                // Display
                options.DefaultModelExpandDepth(2);
                options.DefaultModelRendering(ModelRendering.Model);
                options.DefaultModelsExpandDepth(-1);
                options.DisplayOperationId();
                options.DisplayRequestDuration();
                options.DocExpansion(DocExpansion.None);
                options.EnableDeepLinking();
                options.EnableFilter();
                options.ShowExtensions();
                // Network
                options.EnableValidator();
                // Other
                options.DocumentTitle = configuration["SwaggerApi:DocumentTitle"];
                //添加自定义css和js
                //options.InjectStylesheet("/ext/custom-stylesheet.css");
                //options.InjectJavascript("/ext/custom-javascript.js");
            });
            return app;
        }
    }
}
