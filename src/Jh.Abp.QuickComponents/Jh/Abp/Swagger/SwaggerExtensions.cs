using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Volo.Abp.VirtualFileSystem;

namespace Jh.Abp.QuickComponents.Swagger
{
    public static partial class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerComponent(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = configuration["SwaggerApi:OpenApiInfo:Title"],
                        Version = configuration["SwaggerApi:OpenApiInfo:Version"],
                        Description = configuration["SwaggerApi:OpenApiInfo:Description"],
                    });
                    options.DocInclusionPredicate((docName, description) => true);
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
