using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using Volo.Abp.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;

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

        public static IServiceCollection AddSwaggerComponent(this IServiceCollection services, IConfiguration configuration)
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

        public static IApplicationBuilder UseSwaggerComponent(this IApplicationBuilder app, IConfiguration configuration, Type StarupType)
        {
            //StarupType用于加载Swagger文档的类的程序集
            if (StarupType == null)
            {
                throw new ArgumentNullException("Swagger Starup Type Is Null");
            }
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
    }
}
