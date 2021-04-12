using FormCustom;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Jh.Abp.FormCustom.EntityFrameworkCore
{
    [ConnectionStringName(FormCustomDbProperties.ConnectionStringName)]
    public class FormCustomDbContext : AbpDbContext<FormCustomDbContext>, IFormCustomDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormField> FormFields { get; set; }
        public FormCustomDbContext(DbContextOptions<FormCustomDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureFormCustom();
        }
    }
}