using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Jh.Abp.MenuManagement.EntityFrameworkCore
{
    public class MenuManagementHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<MenuManagementHttpApiHostMigrationsDbContext>
    {
        public MenuManagementHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var connectionString = configuration.GetConnectionString("Default");
            var builder = new DbContextOptionsBuilder<MenuManagementHttpApiHostMigrationsDbContext>()
                 //.UseSqlServer(connectionString);
                 //.UseDm(connectionString);
                 .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new MenuManagementHttpApiHostMigrationsDbContext(builder.Options);
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
