using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Jh.Abp.FormCustom.MongoDB
{
    [ConnectionStringName(FormCustomDbProperties.ConnectionStringName)]
    public class FormCustomMongoDbContext : AbpMongoDbContext, IFormCustomMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureFormCustom();
        }
    }
}