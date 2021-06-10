using Jh.SourceGenerator.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RazorEngine;
using RazorEngine.Templating; // For extension methods.
using Jh.SourceGenerator.Common.GeneratorDtos;

namespace Jh.Abp.QuickComponents.Tests.JhSourceGeneratorCommon
{
    public class GeneratorServiceTest
    {
        [Fact]
        public void TestGetTableClass()
        {
            //模板路径为空不生成
            var basePath = @"G:\Temp\";
            var domainAssembly = typeof(MenuManagement.MenuManagementDomainModule).Assembly;
            var domain = @"\Menus";
            var options = new GeneratorOptions()
            {
                DbContext = "MenuManagementDbContext",
                Namespace = "MenuManagement",
                ControllerBase = "MenuManagementController",
                CreateContractsPermissionsPath = @$"{basePath}trunk\src\Jh.Abp.MenuManagement.Contracts\Permissions",
                CreateContractsPath = @$"{basePath}trunk\src\Jh.Abp.MenuManagement.Contracts{domain}",
                CreateApplicationPath = @$"{basePath}trunk\src\Jh.Abp.MenuManagement.Application{domain}",
                CreateDomainPath = @$"{basePath}trunk\src\Jh.Abp.MenuManagement.Domain{domain}",
                CreateEfCorePath = @$"{basePath}trunk\src\Jh.Abp.MenuManagement.EntityFrameworkCore{domain}",
                CreateHttpApiPath = @$"{basePath}trunk\src\Jh.Abp.MenuManagement.HttpApi\v1{domain}",
                //不需要domain做文件夹
                //CreateHtmlPath = @$"{basePath}trunk\host\Jh.Abp.MenuManagement.Web.Unified\wwwroot\main\view",
                //CreateHtmlTemplatePath = @"G:\github\mygithub\Jh.Abp.QuickComponents\src\GeneratorCoding\Jh.SourceGenerator.Common\CodeBuilders\Html\Layui"
            };
            var service = new GeneratorService(domainAssembly, options);
            Assert.True(service.GeneratorCode());
        }

        [Fact]
        public void TestCshtml()
        {
            string template = "Hello @Model.Name, welcome to RazorEngine!";
            var result = Engine.Razor.RunCompile(template, "templateKey", null, new { Name = "World" });
            Assert.NotNull(result);
        }
    }
}
