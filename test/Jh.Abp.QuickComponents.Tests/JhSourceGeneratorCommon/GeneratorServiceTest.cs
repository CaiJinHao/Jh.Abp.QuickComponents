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
            var domainAssembly = typeof(MenuManagement.MenuManagementDomainModule).Assembly;
            var domain = @"\AppSettings";
            var options = new GeneratorOptions()
            {
                DbContext = "EquipmentQuotationAppDbContext",
                Namespace = "EquipmentQuotationApp",
                ControllerBase = "EquipmentQuotationAppController",
                CreateContractsPath = @$"E:\TEMP\trunk\src\SupplyDemandPlatform.Application.Contracts{domain}",
                CreateApplicationPath = @$"E:\TEMP\trunk\src\SupplyDemandPlatform.Application{domain}",
                CreateDomainPath = @$"E:\TEMP\trunk\src\SupplyDemandPlatform.Domain{domain}",
                CreateEfCorePath = @$"E:\TEMP\trunk\src\SupplyDemandPlatform.EntityFrameworkCore{domain}",
                CreateHttpApiPath = @$"E:\TEMP\trunk\src\SupplyDemandPlatform.HttpApi\v1{domain}",
                //不需要domain做文件夹
                CreateHtmlPath = @$"E:\TEMP\trunk\host\SupplyDemandPlatform.Web.Unified\wwwroot\main\view",
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
