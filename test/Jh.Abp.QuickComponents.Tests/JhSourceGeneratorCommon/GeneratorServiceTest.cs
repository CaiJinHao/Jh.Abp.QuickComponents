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
            var service = new GeneratorService(domainAssembly);
            var generatorResult = service.GeneratorCode().ToList();
            Assert.True(generatorResult.Count()>0);
        }
    }
}
