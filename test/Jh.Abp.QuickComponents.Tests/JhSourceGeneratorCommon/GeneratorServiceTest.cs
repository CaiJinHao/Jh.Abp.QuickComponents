using Jh.SourceGenerator.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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
            };
            var service = new GeneratorService(domainAssembly, options);
            var generatorResult = service.GeneratorCode().ToList();
            Assert.True(generatorResult.Count()>0);
        }
    }
}
