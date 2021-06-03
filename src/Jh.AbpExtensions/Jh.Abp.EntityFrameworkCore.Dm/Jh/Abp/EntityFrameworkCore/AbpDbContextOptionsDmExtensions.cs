using JetBrains.Annotations;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Volo.Abp.EntityFrameworkCore
{
    public static class AbpDbContextOptionsDmExtensions
    {
        public static void UseDm(
                [NotNull] this AbpDbContextOptions options,
                [CanBeNull] Action<DmDbContextOptionsBuilder> mySQLOptionsAction = null)
        {
            options.Configure(context =>
            {
                context.UseDm(mySQLOptionsAction);
            });
        }

        public static void UseDm<TDbContext>(
            [NotNull] this AbpDbContextOptions options,
            [CanBeNull] Action<DmDbContextOptionsBuilder> mySQLOptionsAction = null)
            where TDbContext : AbpDbContext<TDbContext>
        {
            options.Configure<TDbContext>(context =>
            {
                context.UseDm(mySQLOptionsAction);
            });
        }
    }
}
