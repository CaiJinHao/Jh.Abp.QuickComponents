using JetBrains.Annotations;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Volo.Abp.EntityFrameworkCore
{
    public static class AbpDbContextConfigurationContextDmExtensions
    {
        public static DbContextOptionsBuilder UseDm(
           [NotNull] this AbpDbContextConfigurationContext context,
           [CanBeNull] Action<DmDbContextOptionsBuilder> dmOptionsAction = null)
        {
            if (context.ExistingConnection != null)
            {
                return context.DbContextOptions.UseDm(context.ExistingConnection, optionsBuilder =>
                {
                    optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    dmOptionsAction?.Invoke(optionsBuilder);
                })
                    //.UseInternalServiceProvider(new ServiceCollection().AddEntityFrameworkDm().BuildServiceProvider())
                ;
            }
            else
            {
                return context.DbContextOptions.UseDm(context.ConnectionString, optionsBuilder =>
                {
                    optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    dmOptionsAction?.Invoke(optionsBuilder);
                })
                    //.UseInternalServiceProvider(new ServiceCollection().AddEntityFrameworkDm().BuildServiceProvider())
                ;
            }
        }
    }
}
