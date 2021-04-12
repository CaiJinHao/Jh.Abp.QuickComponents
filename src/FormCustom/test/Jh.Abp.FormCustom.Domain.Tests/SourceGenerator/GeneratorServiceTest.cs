using Jh.SourceGenerator.Common;
using Jh.SourceGenerator.Common.GeneratorDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jh.Abp.FormCustom.SourceGenerator
{
    public class GeneratorServiceTest
    {
        [Fact(DisplayName = "代码生成")]
        public void GeneratorTest()
        {
            var domainAssembly = typeof(FormCustomDomainModule).Assembly;
            var domain = @"\Forms";
            var options = new GeneratorOptions()
            {
                DbContext = "SupplyDemandPlatformDbContext",
                Namespace = "SupplyDemandPlatform",
                ControllerBase = "SupplyDemandPlatformController",
                CreateContractsPath = @"E:\CompanyWork\website_XinZhou\trunk\src\SupplyDemandPlatform.Application.Contracts" + domain,
                CreateApplicationPath = @"E:\CompanyWork\website_XinZhou\trunk\src\SupplyDemandPlatform.Application" + domain,
                CreateDomainPath = @"E:\CompanyWork\website_XinZhou\trunk\src\SupplyDemandPlatform.Domain" + domain,
                CreateEfCorePath = @"E:\CompanyWork\website_XinZhou\trunk\src\SupplyDemandPlatform.EntityFrameworkCore" + domain,
                //不需要表名做文件夹
                CreateHttpApiPath = @"E:\CompanyWork\website_XinZhou\trunk\src\SupplyDemandPlatform.HttpApi\v1" + domain,
                CreateHtmlPath = @"E:\CompanyWork\website_XinZhou\trunk\host\SupplyDemandPlatform.HttpApi.Host\wwwroot\main\view",
                CreateHtmlTemplatePath = @"E:\CompanyWork\website_XinZhou\documents\CodeTemplate\Layui",
            };
            /*var options = new GeneratorOptions()
            {
                DbContext = "SupplyDemandPlatformDbContext",
                Namespace = "SupplyDemandPlatform",
                ControllerBase = "SupplyDemandPlatformController",
                CreateContractsPath = @"E:\Temp\Contracts",
                CreateApplicationPath = @"E:\Temp\Application",
                CreateDomainPath = @"E:\Temp\Domain",
                CreateEfCorePath = @"E:\Temp\EntityFrameworkCore",
                //不需要表名做文件夹
                CreateHttpApiPath = @"E:\Temp\HttpApi\v1",
                CreateHtmlPath = @"E:\Temp\website_XinZhou\trunk\host\SupplyDemandPlatform.HttpApi.Host\wwwroot\main\view",
                CreateHtmlTemplatePath = @"E:\CompanyWork\website_XinZhou\documents\CodeTemplate\Layui",
            };*/
            var service = new GeneratorService(domainAssembly, options);
            Assert.True(service.GeneratorCode());
        }
    }
}
