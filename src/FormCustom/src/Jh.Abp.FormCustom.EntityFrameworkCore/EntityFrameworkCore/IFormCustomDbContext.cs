using FormCustom;
using Microsoft.EntityFrameworkCore;
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
        DbSet<Form> Forms { get; set; }
        DbSet<FormField> FormFields { get; set; }
    }
}