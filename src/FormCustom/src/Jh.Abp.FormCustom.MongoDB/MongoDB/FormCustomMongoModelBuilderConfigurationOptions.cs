using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Jh.Abp.FormCustom.MongoDB
{
    public class FormCustomMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public FormCustomMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}