using Jh.SourceGenerator.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RazorEngine;
using RazorEngine.Templating; // For extension methods.

namespace Jh.Abp.QuickComponents.Tests.JhSourceGeneratorCommon
{
    public class GeneratorServiceTest
    {
        [Fact]
        public void TestGetTableClass()
        {
            var domainAssembly = typeof(MenuManagement.MenuManagementDomainModule).Assembly;

            var options = new SourceGenerator.Common.GeneratorDtos.GeneratorOptions()
            {
                DbContext = "MenuManagementDbContext",
                Namespace = "Jh.Abp.MenuManagement",
                ControllerBase = "MenuManagementController",
                CreateContractsPath = @"E:\TEMP\Contracts",
                CreateApplicationPath = @"E:\TEMP\Application",
                CreateDomainPath = @"E:\TEMP\Domain",
                CreateEfCorePath = @"E:\TEMP\EfCore",
                CreateHttpApiPath = @"E:\TEMP\HttpApi",
                CreateHtmlPath = @"E:\TEMP\Html",
                CreateHtmlTemplatePath = @"E:\MyWork\GitHub\Jh.Abp.QuickComponents\src\GeneratorCoding\Jh.SourceGenerator.Common\CodeBuilders\Html\Layui",
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
