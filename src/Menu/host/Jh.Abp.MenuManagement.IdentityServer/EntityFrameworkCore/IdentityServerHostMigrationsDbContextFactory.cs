using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Jh.Abp.MenuManagement.EntityFrameworkCore
{
    public class IdentityServerHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<IdentityServerHostMigrationsDbContext>
    {
        public IdentityServerHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var connectionString = configuration.GetConnectionString("Default");
            var builder = new DbContextOptionsBuilder<IdentityServerHostMigrationsDbContext>()
                 //.UseSqlServer(connectionString);
                 //.UseDm(connectionString);
                 .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new IdentityServerHostMigrationsDbContext(builder.Options);
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
