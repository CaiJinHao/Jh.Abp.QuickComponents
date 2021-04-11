using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Jh.Abp.FormCustom.MongoDB
{
    [ConnectionStringName(FormCustomDbProperties.ConnectionStringName)]
    public interface IFormCustomMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
