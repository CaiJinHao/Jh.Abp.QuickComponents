using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Jh.Abp.FormCustom.EntityFrameworkCore
{
    public class FormCustomHttpApiHostMigrationsDbContext : AbpDbContext<FormCustomHttpApiHostMigrationsDbContext>
    {
        public FormCustomHttpApiHostMigrationsDbContext(DbContextOptions<FormCustomHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureFormCustom();
        }
    }
}
