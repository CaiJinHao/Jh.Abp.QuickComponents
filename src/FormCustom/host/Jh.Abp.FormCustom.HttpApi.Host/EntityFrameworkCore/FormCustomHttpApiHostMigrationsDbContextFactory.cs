using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Jh.Abp.FormCustom.EntityFrameworkCore
{
    public class FormCustomHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<FormCustomHttpApiHostMigrationsDbContext>
    {
        public FormCustomHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<FormCustomHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("FormCustom"));

            return new FormCustomHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
