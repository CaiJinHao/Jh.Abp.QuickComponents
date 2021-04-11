using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Jh.Abp.FormCustom.EntityFrameworkCore
{
    [ConnectionStringName(FormCustomDbProperties.ConnectionStringName)]
    public interface IFormCustomDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}