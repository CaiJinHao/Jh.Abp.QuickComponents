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
using Jh.Abp.MenuManagement;

namespace Jh.Abp.QuickComponents.Tests.JhSourceGeneratorCommon
{
    public class GeneratorServiceTest
    {
        [Fact]
        public void TestGetTableClass()
        {
            //模板路径为空不生成
            var basePathTemp = @"G:\Temp\";
            //var basePath = @"G:\github\mygithub\jhabpmodule\modules\setting";
            var basePath = basePathTemp;
            var domainAssembly = typeof(MenuManagementDomainModule).Assembly;
            var itemName = "Jh.Abp.MenuManagement";
            var domain = @"\Menus";
            var options = new GeneratorOptions()
            {
                DbContext = "MenuManagementDbContext",
                Namespace = "Jh.MenuManagement",
                ControllerBase = "MenuManagementController",
                CreateContractsPermissionsPath = @$"{basePath}\src\{itemName}.Application.Contracts\Permissions",
                CreateContractsPath = @$"{basePath}\src\{itemName}.Application.Contracts{domain}",
                CreateApplicationPath = @$"{basePath}\src\{itemName}.Application{domain}",
                CreateDomainPath = @$"{basePath}\src\{itemName}.Domain{domain}",
                CreateEfCorePath = @$"{basePath}\src\{itemName}.EntityFrameworkCore{domain}",
                CreateHttpApiPath = @$"{basePath}\src\{itemName}.HttpApi\v1{domain}",
                //不需要domain做文件夹
                //CreateHtmlPath = @$"{basePathTemp}\host\{itemName}.HttpApi.Host\wwwroot\main\view",
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
