using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;

namespace Microsoft.EntityFrameworkCore
{
    public static class AbpMySqlModelBuilderExtensions
    {
        public static void UseDm(
            this ModelBuilder modelBuilder)
        {
            modelBuilder.SetDatabaseProvider(EfCoreDatabaseProvider.SqlServer);
        }
    }
}